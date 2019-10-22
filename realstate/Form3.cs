using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace realstate
{
    public partial class Form3 : Form
    {
        BackgroundWorker ConfirmBG = new BackgroundWorker();
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection();

        Font myFont;
        private void fontload()
        {
            Font lableFont;
            lableFont = new Font(fonts.Families[0], 9.0F);
            label1.Font = lableFont;
            label2.Font = lableFont;
            label3.Font = lableFont;
            label4.Font = lableFont;
            checkBox1.Font = lableFont;
            Font textfont;
            textfont = new Font(fonts.Families[0], 11.0F, System.Drawing.FontStyle.Bold);
            username.Font = textfont;
            password.Font = textfont;
           // ip.Font = textfont;

        }
        public Form3()
        {
            InitializeComponent();

            byte[] fontData = Properties.Resources.IRANSans_FaNum_;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.IRANSans_FaNum_.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.IRANSans_FaNum_.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            fontload();
            this.MinimizeBox = true;
            this.MaximizeBox = false;
            username.Text = Settings1.Default.username;
            password.Text = Settings1.Default.password;
            ip.Text = Settings1.Default.ip;


            this.Location = new Point(400, 150);
            //if (Settings1.Default.IsLogedIn == "1")
            //{
            //    Form1 form = new Form1();
            //    form.Show();

            //}

            // form.createBackgroundWorker(2);
        }


        private void confirm_Click(object sender, EventArgs e)
        {
            loadingIMG.Visible = true;
            
            if (checkBox1.Checked == true)
            {

                if (username.Text == "" || password.Text == "")
                {

                }
                else
                {
                    
                    Settings1.Default.username = username.Text;
                    Settings1.Default.password = password.Text;
                    string varsion = Application.ProductVersion;
                    string platform = System.Environment.OSVersion.Platform.ToString();
                    string result = "";
                    try
                    {
                        using (WebClient client = new WebClient())
                        {
                            var collection = new System.Collections.Specialized.NameValueCollection();
                            collection.Add("version", varsion);
                            collection.Add("os", platform);
                            collection.Add("username", username.Text);
                            collection.Add("password", password.Text);
                            byte[] response =
                            client.UploadValues("http://arvandfile.com/api/v1/authenticateUser.php", collection);

                            result = System.Text.Encoding.UTF8.GetString(response);
                        }
                    }
                    catch (Exception error)
                    {
                        loadingIMG.Visible = false;
                        MessageBox.Show("خطا در ارتباط با سرور، لطفاً مجدداً تلاش کنید");
                        return;
                    }
                  
                   
                    responseModel log = JsonConvert.DeserializeObject<responseModel>(result);
                    errorResponse log2 = JsonConvert.DeserializeObject<errorResponse>(result);
                    if (log.dialog != null)
                    {
                        if (log.dialog.status == 200)
                        {
                            loadingIMG.Visible = false;
                            // GlobalVariable.activezonse = log...result2.active_regions;
                            GlobalVariable.portlimit = log.loginResult.users_limit;
                            GlobalVariable.expirationdate = log.loginResult.exp_date;
                            GlobalVariable.token = log.loginResult.token;
                            try
                            {
                                using (WebClient client = new WebClient())
                                {
                                    var collection = new System.Collections.Specialized.NameValueCollection();
                                    collection.Add("token", log.loginResult.token);
                                    byte[] response =
                                    client.UploadValues("http://arvandfile.com/api/v1/getCats&Areas.php", collection);

                                    result = System.Text.Encoding.UTF8.GetString(response);
                                    GlobalVariable.newCatsAndAreas = result;
                                    GlobalVariable.port = 8001;
                                }
                              
                                GlobalVariable.isadmin = "1";
                                Form1 form1 = new Form1();
                                form1.Show();
                                //this.Close();
                            }
                            catch (Exception)
                            {
                                loadingIMG.Visible = false;
                                MessageBox.Show("خطا در ارتباط با سرور، لطفاً مجدداً تلاش کنید");
                                
                                return;
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show(log.dialog.dialogMessage);
                            loadingIMG.Visible = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show(log2.response.dialogMessage);
                        loadingIMG.Visible = false;

                    }


                }




            }
            else
            {
                doConfirmProcess();
            }


            //ConfirmBG.DoWork += new DoWorkEventHandler(doConfirmProcess);
            // ConfirmBG.WorckerSupportsCancellation = true;
            // ConfirmBG.RunWorkerAsync(argument: count);
            // ConfirmBG.RunWorkerAsync();


        }
        //void doConfirmProcess(object sender, DoWorkEventArgs e)
        //{


        //}
        public void doConfirmProcess()
        {

            username.Enabled = false;
            password.Enabled = false;
            ip.Enabled = false;

            loadingIMG.Visible = true;
            BackgroundWorker checkPassBackGroundWorker = new BackgroundWorker();
            checkPassBackGroundWorker.WorkerSupportsCancellation = true;
            checkPassBackGroundWorker.DoWork += new DoWorkEventHandler(checkPassBackGroundWorker_doWork);
            checkPassBackGroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(checkPassBackGroundWorker_done);

            login model = new login();
            if (password.Text == "" || username.Text == "" || ip.Text == "")
            {
                MessageBox.Show("لطفاً همه موارد را تکمیل کنید");
                loadingIMG.Visible = false;
                username.Enabled = true;
                password.Enabled = true;
                ip.Enabled = true;
            }
            else
            {
                string ipsrt = ip.Text;
                ipsrt = ipsrt.Replace("/", ".");
                if (!ipsrt.Contains(":"))
                {
                    MessageBox.Show("آی پی وارد شده صحیح نیست");
                    loadingIMG.Visible = false;
                    username.Enabled = true;
                    password.Enabled = true;
                    ip.Enabled = true;
                }
                else
                {
                    Settings1.Default.username = username.Text;
                    Settings1.Default.password = password.Text;
                    Settings1.Default.ip = ipsrt;

                    model.password = password.Text;
                    model.username = username.Text;
                    model.port = ipsrt;

                    string str = JsonConvert.SerializeObject(model);

                    checkPassBackGroundWorker.RunWorkerAsync(argument: str);
                }

            }



        }

        void checkPassBackGroundWorker_doWork(object sender, DoWorkEventArgs e)
        {


            string query = (string)e.Argument;
            login mymodel = JsonConvert.DeserializeObject<login>(query);

            string myip = mymodel.port;
            string myusername = mymodel.username;
            string mypassword = mymodel.password;

            int index = myip.IndexOf(":");
            string portstr = myip.Substring(index + 1, myip.Length - index - 1);
            string ipstr = myip.Substring(0, index);
            int port = Convert.ToInt32(portstr);

            GlobalVariable.port = port;
            GlobalVariable.serverIP = ipstr;
            string negip = myip.Substring(0, index);


            TcpClient tcpclnt = new TcpClient();
            try
            {
                tcpclnt.Connect(negip, port);


            }

            catch (Exception)
            {
                MessageBox.Show("پاسخی از سرور دریافت نشد");
                tcpclnt.Close();
                return;

            }



            try
            {
                login login = new realstate.login()
                {
                    username = myusername,
                    password = mypassword
                };
                //List<login> list = new List<realstate.login>();
                //list.Add(login);
                //list.Add(login);

                string str = JsonConvert.SerializeObject(login);
                // use the ipaddress as in the server program
                Console.WriteLine("Connected with port" + port.ToString());
                str = "4" + str;
                //Console.Write("Enter the string to be transmitted : ");

                //String str = Console.ReadLine();

                Stream stm = tcpclnt.GetStream();
                Encoding utf8 = Encoding.UTF8;
                ASCIIEncoding asen = new ASCIIEncoding();
                //  byte[] ba = utf8.GetBytes(str);
                byte[] ba = asen.GetBytes(str);
                Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length);
                string json = "";

                const int blockSize = 100000;
                byte[] buffer = new byte[blockSize];
                int bytesRead;

                while ((bytesRead = stm.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < bytesRead; i++)
                    {
                        json = json + Convert.ToChar(buffer[i]);
                    }
                }
                string json2 = utf8.GetString(buffer);
                loginback response = JsonConvert.DeserializeObject<loginback>(json2);
                if (response.status == "Error")
                {
                    loadingIMG.Visible = false;
                    MessageBox.Show("خطای سرور لطفاً مجدداً تلاش کنید");
                    tcpclnt.Close();
                    //"خطای سرور لطفاً مجدداً تلاش کنید";
                }
                else if (response.status == "error2")
                {
                    loadingIMG.Visible = false;
                    MessageBox.Show("رمز عبور یا نام کاربری صحیح نیست");
                    tcpclnt.Close();
                }
                else
                {
                    loginback log = JsonConvert.DeserializeObject<loginback>(json2);

                    if (log.status == "success")
                    {
                        Settings1.Default.IsLogedIn = "1";
                        GlobalVariable.catsAndAreas = log.autocompleteObject;
                        GlobalVariable.token = log.token;
                        tcpclnt.Close();
                        //ConfirmBG.CancelAsync();
                        e.Result = "change";


                    }
                    else
                    {
                        tcpclnt.Close();

                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("خطا در برقراری ارتباط با سرور");
                tcpclnt.Close();
                return;
            }


        }
        private void checkPassBackGroundWorker_done(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Result == "change")
            {
                loadingIMG.Visible = false;
                username.Enabled = true;
                password.Enabled = true;
                ip.Enabled = true;
                GlobalVariable.isadmin = "0";

                Form1 form1 = new Form1();
                Control clt = form1.Controls.Find("connect", true).First();
                clt.Visible = true;

                Control clt2 = form1.Controls.Find("searchpanel", true).First();
                clt2.Visible = true;

                Control clt3 = form1.Controls.Find("pictureBox9", true).First();
                clt3.Visible = false;
                Control clt4 = form1.Controls.Find("dissconnect", true).First();
                clt4.Visible = false;

                //form1.Controls["dissconnect"].Visible = false;
                //form1.Controls["pictureBox9"].Visible = false;
                //form1.Controls["searchpanel"].Visible = false;
                //form1.Controls["connect"].Visible = true;

                form1.Show();
             //   this.Close();
                loadingIMG.Visible = false;
                this.Visible = false;

            }
            else
            {
                loadingIMG.Visible = false;
                username.Enabled = true;
                password.Enabled = true;
                ip.Enabled = true;
            }



        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                ip.Enabled = false;
                ip.BackColor = System.Drawing.SystemColors.ControlLight;
                radPanel5.BackColor = System.Drawing.SystemColors.ControlLight;
            }
            else
            {
                ip.Enabled = true;
                ip.BackColor = System.Drawing.SystemColors.ScrollBar;
                radPanel5.BackColor = System.Drawing.SystemColors.ScrollBar;
            }

        }











    }
}
