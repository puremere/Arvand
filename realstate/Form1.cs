using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;

using System.Runtime.InteropServices;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
using System.Data.SqlClient;
using System.Security.AccessControl;
using System.Management;

using System.Net.Sockets;
using System.Security.Cryptography;
using System.Drawing.Text;
using System.Globalization;
using Telerik.WinControls.Layouts;

using Classes;
 




namespace realstate
{
    public partial class Form1 : Form
    {

       
        private List<Control> GetAllControls(Control container, List<Control> list)
        {
            foreach (Control c in container.Controls)
            {

                if (c.Controls.Count > 0)
                    list = GetAllControls(c, list);
                else
                    list.Add(c);
            }

            return list;
        }
        private List<Control> GetAllControls(Control container)
        {
            return GetAllControls(container, new List<Control>());
        }
        public void initFont()
        {
            byte[] fontData = Properties.Resources.IRANSans_FaNum_;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.IRANSans_FaNum_.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.IRANSans_FaNum_.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            GlobalVariable.headerlistFONT = new Font(fonts.Families[0], 9.0F, System.Drawing.FontStyle.Regular);
            GlobalVariable.headerlistFONTsmall = new Font(fonts.Families[0], 8.0F, System.Drawing.FontStyle.Regular);
            GlobalVariable.headerlistFONTBold = new Font(fonts.Families[0], 11.0F, System.Drawing.FontStyle.Bold);

            
            List<Control> allControls = GetAllControls(this);
            allControls.ForEach(k => k.Font = GlobalVariable.headerlistFONT);

        }
        
        public static float width = 0;
        public static string server_id = "";
        int i = 0;
        static bool ContinuAllListener = true;
        static List<TcpListener> listenerlist = new List<TcpListener>();
        static List<lisener> lisenerdetaillist = new List<lisener>();
        List<BackgroundWorker> workers = new List<BackgroundWorker>();

        #region .. Double Buffered function ..
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }

        #endregion


        #region .. code for Flucuring ..
        private const int CP_NOCLOSE_BUTTON = 0x200;
        // code to desable close butt 
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                cp.ClassStyle = cp.ClassStyle | CP_NOCLOSE_BUTTON;
                return cp ;
               

               
            }

           
        }
       
        
        #endregion



        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;



        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        public Form1()
        {
           

            //string newpath = @"\\MERIOLI-PC\Users\merioli\Desktop\Datafolder";
            ////string newpath = AppDomain.CurrentDomain.BaseDirectory;
            //AppDomain.CurrentDomain.SetData("DataDirectory", newpath);

            InitializeComponent();
           
            this.WindowState = FormWindowState.Maximized;


            // To report progress from the background worker we need to set this property
            backgroundWorker1.WorkerReportsProgress = true;
            // This event will be raised on the worker thread when the worker starts
            // backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            // This event will be raised when we call ReportProgress
            // backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                                                 backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.WorkerSupportsCancellation = true;


            //backgroundworker2
            backgroundWorker2.WorkerReportsProgress = true;
            // This event will be raised on the worker thread when the worker starts
            //backgroundWorker2.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
            // This event will be raised when we call ReportProgress
            backgroundWorker2.WorkerSupportsCancellation = true;



            byte[] fontData = Properties.Resources.IRANSans_FaNum_;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.IRANSans_FaNum_.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.IRANSans_FaNum_.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);



        }

        //void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    MyContext mycontext = new MyContext();
        //    string json2;
        //    using (var client = new WebClient())
        //    {
        //        json2 = client.DownloadString("http://api.backino.net/melkagahi/syncEngine.php");
        //    }
        //    RootObject log = JsonConvert.DeserializeObject<RootObject>(json2);
        //    float count = log.result.data.Count();
        //    float amount = 0;
        //    if (count >= 100)
        //    {
        //        amount = count / 100;

        //    }
        //    else
        //    {
        //        amount = 100 / count;
        //    }


        //    foreach (var item in log.result.data)
        //    {


        //        finalamount = finalamount + amount;
        //        try
        //        {
        //            Datum newdata = new Datum
        //            {
        //                area = item.area,
        //                build_year = item.build_year,
        //                canbeAgent = item.canbeAgent,
        //                cat = item.cat,
        //                countryside = item.countryside,
        //                desc = item.desc,
        //                ejare = item.ejare,
        //                email = item.email,
        //                ugent = item.isAgent,
        //                lat = item.lat,
        //                lng = item.lng,
        //                metraj = item.metraj,
        //                phone = item.phone,
        //                room = item.room,
        //                server_id = item.server_id,
        //                source = item.source,
        //                tabdil = item.tabdil,
        //                title = item.title,
        //                vadie = item.vadie
        //            };
        //            miners = finalamount - Math.Truncate(finalamount);
        //            if (miners > 0.99)
        //            {

        //                backgroundWorker1.ReportProgress(Convert.ToInt32(Math.Truncate(finalamount)) + 1);
        //                miners = miners - 0.99;
        //            }
        //            else
        //            {
        //                backgroundWorker1.ReportProgress(Convert.ToInt32(finalamount));
        //            }

        //            mycontext.Data.Add(newdata);
        //            mycontext.SaveChanges();
        //            foreach (var image in item.images)
        //            {


        //                bool exists = System.IO.Directory.Exists(Path.Combine(Application.StartupPath, "Images"));

        //                if (!exists)
        //                    System.IO.Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Images"));

        //                WebClient myWebClient = new WebClient();
        //                string myStringWebResource = image;
        //                string name = RandomString(7);
        //                string fileName = Path.Combine(Application.StartupPath, "Images", name + ".jpg");
        //                myWebClient.DownloadFile(myStringWebResource, fileName);

        //                image newimage = new image();
        //                newimage.name = name;
        //                newimage.ProductID = item.server_id;
        //                mycontext.images.Add(newimage);

        //                mycontext.SaveChanges();

        //            }


        //        }
        //        catch (Exception myerror)
        //        {


        //        }




        //    }




        //}

        //void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    // The progress percentage is a property of e
        //    progressBar1.Value = e.ProgressPercentage;
        //    label3.Text = e.ProgressPercentage.ToString() + "%";

        //}
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            //MyContext mycontext = new MyContext();
            //List<ListOfAdds.Datum> songsDataTableBindingSource = (from p in mycontext.Data
            //                                           select p).ToList();
            //this.radListView1.DataSource = songsDataTableBindingSource;
            //this.radListView1.ColumnCreating += new ListViewColumnCreatingEventHandler(radListView1_ColumnCreating);
            //this.radListView1.ViewType = ListViewType.DetailsView;
        }



        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //  public ChromiumWebBrowser chromeBrowser;

        //public void InitializeChromium()
        //{
        //    CefSettings settings = new CefSettings();
        //    // Initialize cef with the provided settings
        //    Cef.Initialize(settings);
        //    // Create a browser component
        //    chromeBrowser = new ChromiumWebBrowser("http://ourcodeworld.com");
        //    // Add it to the form and fill it to the form window.
        //    this.Controls.Add(chromeBrowser);
        //    chromeBrowser.Dock = DockStyle.Fill;
        //}


        //private void MenuIcon_Click(object sender, EventArgs e)
        //{
        //    Util.Animate(menuPanel, Util.Effect.Slide, 150, 360);
        //}


        //private void fontload()
        //{

        //    Font headerlistFONT;
        //    headerlistFONT = new Font(fonts.Families[0], 11.0F, System.Drawing.FontStyle.Regular);



        //    label2.Font = headerlistFONT;
        //    label28.Font = headerlistFONT;
        //    label27.Font = headerlistFONT;
        //    label26.Font = headerlistFONT;
        //    label5.Font = headerlistFONT;
        //    //listheader


        //    Font lableFont;
        //    lableFont = new Font(fonts.Families[0], 12.0F);
        //  //  label29.Font = lableFont;
        //    //titlelogo

        //   // seachButt.Font = lableFont;
        //    //confirmlable






        //    Font smallfont;
        //    smallfont = new Font(fonts.Families[0], 10.0F);
        //    radListView1.Font = smallfont;
        //    Font smallfontmedium;
        //    smallfontmedium = new Font(fonts.Families[0], 9.0F, System.Drawing.FontStyle.Bold);


        //    label17.Font = smallfontmedium;
        //    lastupdataLable.Font = smallfontmedium;
        //    label21.Font = smallfontmedium;
        //    label22.Font = smallfontmedium;
        //    label23.Font = smallfontmedium;
        //    label24.Font = smallfontmedium;
        //    label25.Font = smallfontmedium;
        //    wishListLable.Font = smallfontmedium;
        //    //headerbar
        //   // label30.Font = smallfontmedium;
        //    //versionlogo



        //}
        
       

        
        private void Form1_Load(object sender, EventArgs e)
        {


          //  Settings1.Default.ides = "";
           // Settings1.Default.Save();


              GlobalVariable.relatedAddress = "";
             // width = tableLayoutPanel6.Width;
            width = ClientRectangle.Width;
            radListView1.Height = flowLayoutPanel1.Height - 90;
            flowLayoutPanel1.Width = (int)width;
            tableLayoutPanel6.Width = (int)width - (Width*3)/100;
            radListView1.Width =(int) width - (Width * 3) / 100;


            //listviewpanel.Height = (flowLayoutPanel1.Height * 80) / 100;
            radListView1.ListViewElement.HorizontalScrollState = ScrollState.AlwaysHide;
            GlobalVariable.searchPicPath = Path.Combine(Application.StartupPath, "Resources", "search2.jpg");
            initFont();



            // this.MaximizeBox = true;
            this.MinimizeBox = true;
            GlobalVariable.page = "1";
            GlobalVariable.startID = "";
           


            if (Settings1.Default.lastTimeStamp != DateTime.MinValue)
            {
                TimeSpan duration = DateTime.Now - Settings1.Default.lastTimeStamp;
                double minute = duration.TotalMinutes;
                if (minute < 1)
                {
                    lastupdataLable.Text = "لحظاتی پیش";
                }
                else if (minute > 1 && minute < 60)
                {
                    lastupdataLable.Text = Convert.ToInt32(minute).ToString() + " دقیقه پیش ";
                }
                else
                {
                    lastupdataLable.Text = "+ یکساعت پیش";
                }
            }


            // fontload();






            //searchTextBox.Text = "جستجو در فایل ها";


            //SearchPanel.PanelElement.Shape = new RoundRectShape();
            //SearchPanel.PanelElement.PanelFill.GradientStyle = GradientStyles.Solid;
            //SearchPanel.PanelElement.PanelFill.BackColor = Color.White;
          

            mainPanelOfList.PanelElement.Shape = new RoundRectShape();
            mainPanelOfList.PanelElement.PanelFill.GradientStyle = GradientStyles.Solid;
            mainPanelOfList.PanelElement.PanelFill.BackColor = Color.White;

            //if (GlobalVariable.isadmin == "0")
            //{
            //    connect.Visible = true;
            //    dissconnect.Visible = false;
            //    searchpanel.Visible = true;
            //    pictureBox9.Visible = false;
            //}
            //else
            //{
            //    connect.Visible = true;
            //    dissconnect.Visible = true;
            //    searchpanel.Visible = false;
            //    pictureBox9.Visible = true;
            //}
           

            







            this.radListView1.VisualItemFormatting += new Telerik.WinControls.UI.ListViewVisualItemEventHandler(radListView1_VisualItemFormatting);
            this.radListView1.CellCreating += new Telerik.WinControls.UI.ListViewCellElementCreatingEventHandler(radListView1_CellCreating);
            this.radListView1.ColumnCreating += new ListViewColumnCreatingEventHandler(radListView1_ColumnCreating);

            this.radListView1.CellFormatting += new Telerik.WinControls.UI.ListViewCellFormattingEventHandler(radListView1_CellFormatting);
            // this.radListView1.SelectedIndexChanged += new Telerik.WinControls.UI.
            //this.radListView1.DisplayMember = "title";

            //  radListView1.Columns[0].Width = 100;
            //  radListView1.Columns[1].Width = 100;
            //  radListView1.Columns[2].Width = 100;
            //  radListView1.Columns[3].Width = 100;
            ////  radListView1.Columns[4].Width = 200;


            //   getDataFromServer();
            if (GlobalVariable.result != null)
            {
                ListOfAdds.RootObject log = JsonConvert.DeserializeObject<ListOfAdds.RootObject>(GlobalVariable.result);
                foreach (var item in log.result.data)
                {
                    GlobalVariable.RowIDListForPhone.Add(item.ID);

                    if (item.phones != null)
                    {
                        if(item.phones.Count() > 0)
                        {
                            GlobalVariable.RowPhoneList.Add(item.phones.FirstOrDefault());
                        }
                        else
                        {
                            GlobalVariable.RowPhoneList.Add("-----");
                        }
                    }
                    else
                    {
                        GlobalVariable.RowPhoneList.Add("-----");
                    }
                  
                   

                }
                if (log != null)
                {
                    RefreshListhWithNewData(log);
                }
            }






        }

        private void ListViewElement_SelectedItemChanging(object sender, Telerik.WinControls.UI.ListViewItemCancelEventArgs e)
        {

            if (radListView1.Items.IndexOf(e.Item) < 10)
            {
                e.Cancel = true;

                RadMessageBox.Show("test");
            }
        }

        void radListView1_VisualItemFormatting(object sender, ListViewVisualItemEventArgs e)
        {

            e.VisualItem.SetThemeValueOverride(LightVisualElement.NumberOfColorsProperty, 1, "RadListVisualItem.ContainsMouse.MouseOver");
            e.VisualItem.SetThemeValueOverride(LightVisualElement.BackColorProperty, Color.LightBlue, "RadListVisualItem.ContainsMouse.MouseOver");
            if (e.VisualItem.Selected == true)
            {
                e.VisualItem.BackColor = Color.Transparent;
                e.VisualItem.NumberOfColors = 1;
                e.VisualItem.GradientStyle = Telerik.WinControls.GradientStyles.Solid;
                e.VisualItem.BorderColor = Color.White;
            }

            else
            {
                e.VisualItem.BackColor = Color.Transparent;
                //    e.VisualItem.ResetValue(LightVisualElement.BackColorProperty, Telerik.WinControls.ValueResetFlags.Local);
                //    e.VisualItem.ResetValue(LightVisualElement.NumberOfColorsProperty, Telerik.WinControls.ValueResetFlags.Local);
                //    e.VisualItem.ResetValue(LightVisualElement.GradientStyleProperty, Telerik.WinControls.ValueResetFlags.Local);
                //
            }

        }


        private void radListView1_CellFormatting(object sender, ListViewCellFormattingEventArgs e)
        {
            DetailListViewDataCellElement cell = e.CellElement as DetailListViewDataCellElement;
            if (cell != null)
            {
                cell.BackColor = Color.White;
                cell.GradientStyle = Telerik.WinControls.GradientStyles.Solid;
                //  cell.Margin = new Padding(0, 10, 0, 10);
                cell.BorderColor = Color.White;

                // DataRowView productRowView = cell.Row.DataBoundItem as DataRowView;
                //if (productRowView != null && (bool)productRowView.Row["title"] == true)
                //{
                //    e.CellElement.BackColor = Color.Yellow;
                //    e.CellElement.ForeColor = Color.Red;
                //    e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid;
                //    //e.CellElement.Font = newFont;
                //}
                //else
                //{
                //    e.CellElement.ResetValue(LightVisualElement.BackColorProperty, Telerik.WinControls.ValueResetFlags.Local);
                //    e.CellElement.ResetValue(LightVisualElement.ForeColorProperty, Telerik.WinControls.ValueResetFlags.Local);
                //    e.CellElement.ResetValue(LightVisualElement.GradientStyleProperty, Telerik.WinControls.ValueResetFlags.Local);
                //    e.CellElement.ResetValue(LightVisualElement.FontProperty, Telerik.WinControls.ValueResetFlags.Local);
                //}
            }
        }
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }






        private void radListView1_CellCreating(object sender, ListViewCellElementCreatingEventArgs e)
        {
            DetailListViewDataCellElement cell = e.CellElement as DetailListViewDataCellElement;

            if (cell != null && cell.Data.Name == "title")
            {
                // cell.Padding = new Padding(50, 50, 50, 50);
                cell.BackColor = Color.Red;
                e.CellElement = new CustomDetailListViewDataCellElement(cell.RowElement, e.CellElement.Data);

            }
        }

        //void radListView1_ItemDataBound(object sender, Telerik.WinControls.UI.ListViewItemEventArgs e)
        //{
        //    if (radListView1.ViewType == Telerik.WinControls.UI.ListViewType.ListView)
        //    {
        //        string serverid = ((Datum)e.Item.DataBoundItem).server_id;
        //        string title = ((Datum)e.Item.DataBoundItem).title;
        //        string phone = ((Datum)e.Item.DataBoundItem).phone;
        //        string image = ((Datum)e.Item.DataBoundItem).images.First();

        //       // e.Item.Text = "<html> " + serverid + "<br><span style=\"color:#999999\"> " + title + "<br> " + phone + "</span>";

        //    }
        //}
        //void radListView1_VisualItemFormatting(object sender, Telerik.WinControls.UI.ListViewVisualItemEventArgs e)
        //{
        //    if (e.VisualItem.Data.Image != null)
        //    {
        //        e.VisualItem.Image = e.VisualItem.Data.Image.GetThumbnailImage(32, 32, null, IntPtr.Zero);
        //        e.VisualItem.Layout.RightPart.Margin = new Padding(2, 0, 0, 0);
        //    }
        //    if (this.radListView1.ViewType == Telerik.WinControls.UI.ListViewType.DetailsView && e.VisualItem.Data.DataBoundItem != null)
        //    {

        //        e.VisualItem.Size = new System.Drawing.Size(180, 0);
        //    }
        //}




        void radListView1_ColumnCreating(object sender, ListViewColumnCreatingEventArgs e)
        {
            //|| e.Column.FieldName == "area"
            if (e.Column.FieldName == "title")
            {
                e.Column.Visible = true;
            }

            else
            {
                e.Column.Visible = false;
            }



            //if (e.Column.FieldName == "countryside")
            //{
            //    e.Column.HeaderText = "حومه شهر";
            //}
            //if (e.Column.FieldName == "metraj")
            //{
            //    e.Column.HeaderText = "متراژ";
            //}
            //if (e.Column.FieldName == "vadie")
            //{
            //    e.Column.HeaderText = "رهن";
            //}
            //if (e.Column.FieldName == "ejare")
            //{
            //    e.Column.HeaderText = "اجاره";
            //}
            //if (e.Column.FieldName == "room")
            //{
            //    e.Column.HeaderText = "تعداد اتاق";
            //}
            //if (e.Column.FieldName == "cat")
            //{
            //    e.Column.HeaderText = "دسته بندی";
            //}

        }



        //private void menuPanel_Click(object sender, EventArgs e)
        //{
        //    Util.Animate(menuPanel, Util.Effect.Slide, 150, 360);
        //}

        //private void downPic_Click(object sender, EventArgs e)
        //{
        //    Util.Animate(FilterDetailPanel, Util.Effect.Slide, 150, 90);
        //    //downPic.Hide();
        //    //upPic.Show();

        //}

        //private void upPic_Click(object sender, EventArgs e)
        //{
        //    Util.Animate(FilterDetailPanel, Util.Effect.Slide, 150, 90);
        //    //upPic.Hide();
        //    //downPic.Show();
        //}


        //private void radButton1_Click(object sender, EventArgs e)
        //{
        //    progressBar1.Maximum = 100;
        //    progressBar1.Step = 1;
        //    progressBar1.Value = 0;
        //    backgroundWorker1.RunWorkerAsync();
        //    progressBar1.Show();
        //    radButton1.Hide();
        //    // MyContext mycontext = new MyContext(GlobalVariable.ConnectionString_IP);



        //    //List<Datum> songsDataTableBindingSource = (from p in mycontext.Data
        //    //                                           select p).ToList();
        //    //this.radListView1.DataSource = songsDataTableBindingSource;
        //    //this.radListView1.ColumnCreating += new ListViewColumnCreatingEventHandler(radListView1_ColumnCreating);
        //    //this.radListView1.ViewType = ListViewType.DetailsView;




        //}

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalVariable.port = 8002;
            clientsector(GlobalVariable.port);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GlobalVariable.port = 8002;
            clientsector(8002);
        }
        public void clientsector(int port)
        {

            try
            {

                TcpClient tcpclnt = new TcpClient();
                bool mybo = true;
                while (mybo)
                {
                    try
                    {
                        tcpclnt.Connect("192.168.0.1", port);
                        mybo = false;
                    }
                    catch (Exception)
                    {


                    }
                }

                // use the ipaddress as in the server program
                Console.WriteLine("Connected with port" + port.ToString());
                //Console.Write("Enter the string to be transmitted : ");

                //String str = Console.ReadLine();
                string str = "aaaaa";
                Stream stm = tcpclnt.GetStream();

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length);
                string json = "";

                const int blockSize = 1024;
                byte[] buffer = new byte[blockSize];
                int bytesRead;

                while ((bytesRead = stm.Read(buffer, 0, buffer.Length)) > 0)
                {
                    for (int i = 0; i < bytesRead; i++)
                    {
                        json = json + Convert.ToChar(buffer[i]);
                    }
                }

                ListOfAdds.RootObject log = JsonConvert.DeserializeObject<ListOfAdds.RootObject>(json);
                List<ListOfAdds.Datum> songsDataTableBindingSource = (from p in log.result.data
                                                           select p).ToList();
                //this.radListView1.DataSource = songsDataTableBindingSource;
                //this.radListView1.ColumnCreating += new ListViewColumnCreatingEventHandler(radListView1_ColumnCreating);
                //this.radListView1.ViewType = ListViewType.DetailsView;

                tcpclnt.Close();

            }

            catch (Exception f)
            {
                Console.Write("Error..... " + f.StackTrace);
            }
        }
        private void listen_Click(object sender, EventArgs e)
        {
            GlobalVariable.port = 8001;
            Form3 form3 = new Form3();
            form3.Show();
            //createBackgroundWorker(2);
        }

     
        //static void donegociate(int port)
        //{
        //    //backgroundWorker2.ReportProgress(Convert.ToInt32(Math.Truncate(finalamount)) + 1);
        //    // IPAddress ipAd = IPAddress.Parse("192.168.0.1");
        //    // use local m/c IP address, and 
        //    // use the same in the client

        //    /* Initializes the Listener */
        //    TcpListener myList = listenerlist[port];

        //    /* Start Listeneting at the specified port */

        //    bool mybool = true;
        //    string id = "";
        //    while (mybool)
        //    {
        //        try
        //        {

        //            myList.Start();
        //            Console.WriteLine("The server is running at port " + port + "...");
        //            Console.WriteLine("The local End point is  :" + myList.LocalEndpoint);
        //            Console.WriteLine("Waiting for a connection.....");
        //            Socket s = myList.AcceptSocket();
        //            Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

        //            byte[] b = new byte[100];
        //            int k = s.Receive(b);
        //            Console.WriteLine("Recieved...");
        //            string mes = "";
        //            for (int i = 0; i < k; i++)
        //            {
        //                Console.Write(Convert.ToChar(b[i]));
        //                mes = mes + Convert.ToChar(b[i]);
        //            }
        //            string json2;
        //            using (var client = new WebClient())
        //            {
        //                json2 = client.DownloadString("http://api.backino.net/melkagahi/syncEngine.php");
        //            }
        //            RootObject log = JsonConvert.DeserializeObject<RootObject>(json2);

        //            if (Convert.ToChar(b[0]).ToString() == "1")
        //            {
        //                string income = mes.Substring(1, mes.Length - 1);
        //                id = income;


        //                Datum dataselected = (from p in log.result.data
        //                                      where p.server_id == income
        //                                      select p).SingleOrDefault();

        //                byte[] imagenum = new Byte[1];


        //                ASCIIEncoding asencoding = new ASCIIEncoding();
        //                switch (dataselected.images.Count())
        //                {
        //                    case 1:
        //                        imagenum[0] = 1;
        //                        break;
        //                    case 2:
        //                        imagenum[0] = 2;
        //                        break;
        //                    case 3:
        //                        imagenum[0] = 3;
        //                        break;
        //                    case 4:
        //                        imagenum[0] = 4;
        //                        break;
        //                    case 5:
        //                        imagenum[0] = 5;
        //                        break;
        //                    case 6:
        //                        imagenum[0] = 6;
        //                        break;
        //                    case 7:
        //                        imagenum[0] = 7;
        //                        break;
        //                    case 8:
        //                        imagenum[0] = 8;
        //                        break;
        //                    case 9:
        //                        imagenum[0] = 9;
        //                        break;
        //                    default:
        //                        imagenum[0] = 9;
        //                        break;


        //                }
        //                s.Send(imagenum);


        //            }
        //            else if (Convert.ToChar(b[0]).ToString() == "2")
        //            {
        //                int imagenum = Convert.ToInt32(Convert.ToChar(b[1]).ToString()) - 1;
        //                Datum dataselected = (from p in log.result.data
        //                                      where p.server_id == id
        //                                      select p).SingleOrDefault();
        //                string item = dataselected.images[imagenum];
        //                var webClient = new WebClient();

        //                byte[] imageBytes = webClient.DownloadData(item);
        //                s.Send(imageBytes);

        //                //Console.WriteLine("\nSent Acknowledgement");
        //            }
        //            else if (Convert.ToChar(b[0]).ToString() == "3")
        //            {


        //                ASCIIEncoding asen = new ASCIIEncoding();
        //                s.Send(asen.GetBytes(json2));

        //            }
        //            else
        //            {
        //                string json3;
        //                using (var client = new WebClient())
        //                {
        //                    json3 = client.DownloadString("http://api.backino.net/melkagahi/syncEngine.php");
        //                }
        //                ASCIIEncoding asen = new ASCIIEncoding();
        //                s.Send(asen.GetBytes(json2));
        //                Console.WriteLine("\nSent Acknowledgement");
        //            }



        //            /* clean up */
        //            s.Close();

        //            myList.Stop();
        //        }
        //        catch (Exception h)
        //        {
        //            Console.WriteLine("Error..... " + h.StackTrace);
        //        }
        //        // mybool = false;
        //    }
        //}
        static void donegociate(int port)
        {

            //backgroundWorker2.ReportProgress(Convert.ToInt32(Math.Truncate(finalamount)) + 1);
            // IPAddress ipAd = IPAddress.Parse("192.168.0.1");
            // use local m/c IP address, and 
            // use the same in the client

            /* Initializes the Listener */


            /* Start Listeneting at the specified port */



        }


        public void createBackgroundWorker(int num)
        {
            listenerlist.Clear();
            if (Settings1.Default.ServerIP != "")
            {
                IPAddress ipAd = IPAddress.Parse(Settings1.Default.ServerIP);
                int numberOfWorkersNeeded = num + 1;

                for (i = 1; i <= numberOfWorkersNeeded; i++)
                {
                    int count = listenerlist.Count();
                    BackgroundWorker bg = new BackgroundWorker();
                    bg.DoWork += new DoWorkEventHandler(bg_DoWork);
                    bg.WorkerSupportsCancellation = true;
                    bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_MyWorkFinishedHandler);

                    bg.WorkerSupportsCancellation = true;
                    int port = 8000 + i;

                    TcpListener myList = new TcpListener(ipAd, port);
                    lisener newlistener = new lisener()
                    {
                        newsocket = null,
                        b = new byte[4000],
                        datumselected = new ListOfAdds.Datum(),
                        id = "",
                        imagenum = new byte[1],
                        imageNumberSent = 0,
                        imageurlForDownload = "",
                        message = "",
                        RecievedByteCount = 0,
                        counter = 0,
                        workercontinu = true


                    };

                    lisenerdetaillist.Add(newlistener);
                    listenerlist.Add(myList);
                    workers.Add(bg);
                    bg.RunWorkerAsync(argument: count);
                }

               // searchpanel.Visible = true;
            }
            else
            {
                dissconnect.Visible = false;
                connect.Visible = true;
                //searchpanel.Visible = false;
                //MessageBox.Show("آی پی تنظیم شده برای سرور یافت نشد");
            }



        }
        private void bg_MyWorkFinishedHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                dissconnect.Visible = true;
                connect.Visible = false;
                searchpanel.Visible = false;
                MessageBox.Show("آی پی تنظیم شده و یا ارتباط با سرور با مشکل مواجه است، لطفاً دوباره تلاش کنید ");
                return;
            }
            BackgroundWorker mysender = sender as BackgroundWorker;
            if (mysender.CancellationPending == true)
            {

            }
            if (listenerlist.Count > 0)
            {
                foreach (var item in listenerlist)
                {
                    item.Stop();
                }
            }

        }

        //private void radListView1_ItemMouseClick(object sender, ListViewItemEventArgs e)
        //{

        //    Datum myCustomId = (Datum)radListView1.SelectedItem.Value;
        //    server_id = myCustomId.server_id;

        //    Form2 form2 = new Form2(server_id);

        //    form2.Show();

        //}

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            //if (label6.Text == "ایجاد ارتباط")
            //{

            //    label25.Text = "قطع ارتباط";

            //}
            //else
            //{

            //    foreach (var item in workers)
            //    {
            //        item.CancelAsync();


            //    }

            //    //foreach (var item in listenerlist)
            //    //{
            //    //    item.Stop();
            //    //}
            //    ContinuAllListener = false;
            //    connect.Visible = false;
            //    dissconnect.Visible = true;
            //    label6.Text = "ایجاد ارتباط";
            //}


        }

        //private void label6_MouseEnter(object sender, EventArgs e)
        //{
        //    listenButtonHolderPanel.BackColor = Color.LightGray;
        //}

        //private void label6_MouseLeave(object sender, EventArgs e)
        //{
        //    listenButtonHolderPanel.BackColor = Color.Transparent;
        //}

        //private void label7_MouseEnter(object sender, EventArgs e)
        //{
        //    UserAccessPanel.BackColor = Color.LightGray;
        //}

        //private void label7_MouseLeave(object sender, EventArgs e)
        //{
        //    UserAccessPanel.BackColor = Color.Transparent;
        //}

        //private void label8_MouseEnter(object sender, EventArgs e)
        //{
        //    CreateFilePanel.BackColor = Color.LightGray;
        //}

        //private void label8_MouseLeave(object sender, EventArgs e)
        //{
        //    CreateFilePanel.BackColor = Color.Transparent;
        //}

        //private void label8_MouseEnter_1(object sender, EventArgs e)
        //{
        //    CreateFilePanel.BackColor = Color.LightGray;
        //}

        //private void label8_MouseLeave_1(object sender, EventArgs e)
        //{
        //    CreateFilePanel.BackColor = Color.Transparent;
        //}



        private void label7_Click(object sender, EventArgs e)
        {

        }


       
      
        void RefreshListhWithNewData(ListOfAdds.RootObject log)
        {

            Settings1.Default.lastTimeStamp = DateTime.Now;
            Settings1.Default.Save();
            if(log.result.data != null)
            {
                label22.Text = log.result.data.Count().ToString();
            }
           
           
            lastupdataLable.Text = "لحظاتی پیش";
            try
            {


                if (log.result.data != null)
                {
                    totalNumber.Text = log.result.data.Count().ToString();
                   

                    List<ListOfAdds.Datum> songsDataTableBindingSource = (from p in log.result.data
                                                               orderby p.ID ascending
                                                               select p).ToList();
                 
                    radListView1.DataSource = ConvertToDataTable(log.result.data);
                    this.radListView1.ValueMember = "ID";
                    radListView1.ViewType = ListViewType.DetailsView;
                    radListView1.ShowColumnHeaders = false;
                    radListView1.Columns["title"].Width = this.radListView1.Size.Width - this.radListView1.ListViewElement.BorderWidth * 2 - 30;
                    
                    radListView1.ItemSize = new Size(0, 33);
                    


                }
                else
                {
                    MessageBox.Show("موردی وجود  ندارد");
                }



            }
            catch (Exception)
            {


            }
            refreshPic.Visible = true;
            refresh.Visible = false;
        }
        private void detailpic_click(object sender, EventArgs e)
        {
            var pic = (PictureBox)sender;
            string server_id = pic.Name.Substring(2, pic.Name.Length - 2);
            //GlobalVariable.selectedIDofList = server_id;
            //Form2 form2 = new Form2(server_id);
            //form2.Text = "جزئیات محصول";
            //form2.Font = GlobalVariable.headerlistFONT;
            //Application.Run(form2);


        }
        private void PageNum_Click(object sender, EventArgs e)
        {
            refresh.Visible = true;
            Label pageholder = sender as Label;
            string page = pageholder.Name;
            GlobalVariable.page = page;
            int skip = 20 * (Convert.ToInt32(page) - 1);
            ListOfAdds.RootObject log = JsonConvert.DeserializeObject<ListOfAdds.RootObject>(GlobalVariable.result);
            List<ListOfAdds.Datum> newdata = log.result.data.Select(p => p).Skip(skip).Take(20).ToList();
            log.result.data = newdata;

            RefreshListhWithNewData(log);
        }

        
        private void starPic_Click(object sender, EventArgs e)
        {
            var pic = (PictureBox)sender;
            string name = pic.Name;
            string name2 = "1-" + name.Substring(2, name.Length - 2);
            string id = name.Substring(2, name.Length - 2);
            var control = this.Controls.Find(name, true).First();
            var control2 = this.Controls.Find(name2, true).First();
            control.Visible = false;
            control2.Visible = true;
            string ideas = Settings1.Default.ides;
            Settings1.Default.ides = ideas + "," + id;
            Settings1.Default.Save();



        }
        private void starPic_Click2(object sender, EventArgs e)
        {
            var pic = (PictureBox)sender;
            string name = pic.Name;
            string name2 = "0-" + name.Substring(2, name.Length - 2);
            string id = name.Substring(2, name.Length - 2);
            int index = Settings1.Default.ides.LastIndexOf(id);
            Settings1.Default.ides.Remove(index, 6);
            var control = this.Controls.Find(name, true).First();
            var control2 = this.Controls.Find(name2, true).First();
            control.Visible = false;
            control2.Visible = true;


        }


        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }


        private void refreshPic_Click(object sender, EventArgs e)
        {
            GlobalVariable.fromwhere = "main";
            getDataFromServer(null);
            refreshPic.Visible = false;
            //ListOfAdds.RootObject log = JsonConvert.DeserializeObject<ListOfAdds.RootObject>(GlobalVariable.result);
            //RefreshListhWithNewData(log);
        }

        private void dissconnect_Click(object sender, EventArgs e)
        {



            if (Settings1.Default.ServerIP != "")
            {
                ContinuAllListener = true;
                createBackgroundWorker(GlobalVariable.portlimit);
                connect.Visible = true;
                dissconnect.Visible = false;
                searchpanel.Visible = true;
            }
            else
            {
                MessageBox.Show("آی پی سرور تنظیم نشده است");
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

       

        private void searchTextBox_Enter(object sender, EventArgs e)
        {
           // searchTextBox.Text = "";
        }

        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            //if (searchTextBox.Text == "")
            //{
            //    searchTextBox.Text = "جستجو در فایل ها";
            //}

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //TextBox control = searchTextBox;

            GlobalVariable.mtjfrom = "";
            GlobalVariable.mtjto = "";
            GlobalVariable.rom = "";
            GlobalVariable.vadfrom = "";
            GlobalVariable.vadto = "";
            GlobalVariable.ejfrom = "";
            GlobalVariable.ejto = "";
            GlobalVariable.Gtotal_from = "";
            GlobalVariable.Gtotal_to = "";
            GlobalVariable.Gtabdil = "";

            GlobalVariable.Gprice_per_meter_from = "";
            GlobalVariable.Gprice_per_meter_to = "";

            GlobalVariable.Gowner = "";
            GlobalVariable.Gmain_street = "";
            GlobalVariable.Gsubsidiary_street = "";
            GlobalVariable.Galley = "";
            GlobalVariable.Gunit = "";
            GlobalVariable.Gvahed = "";
            GlobalVariable.Gfloor = "";
            GlobalVariable.Gyear = "";
          //  GlobalVariable.query = control.Text;
            GlobalVariable.page = "1";

          //  getDataFromServer();
        }



        //private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        T//extBox control = searchTextBox;

        //        GlobalVariable.mtjfrom = "";
        //        GlobalVariable.mtjto = "";
        //        GlobalVariable.rom = "";
        //        GlobalVariable.vadfrom = "";
        //        GlobalVariable.vadto = "";
        //        GlobalVariable.ejfrom = "";
        //        GlobalVariable.ejto = "";
        //        GlobalVariable.Gtotal_from = "";
        //        GlobalVariable.Gtotal_to = "";
        //        GlobalVariable.Gtabdil = "";

        //        GlobalVariable.Gprice_per_meter_from = "";
        //        GlobalVariable.Gprice_per_meter_to = "";

        //        GlobalVariable.Gowner = "";
        //        GlobalVariable.Gmain_street = "";
        //        GlobalVariable.Gsubsidiary_street = "";
        //        GlobalVariable.Galley = "";
        //        GlobalVariable.Gunit = "";
        //        GlobalVariable.Gvahed = "";
        //        GlobalVariable.Gfloor = "";
        //        GlobalVariable.Gyear = "";
        //        GlobalVariable.query = control.Text;
        //        GlobalVariable.page = "1";

        //      //  getDataFromServer();
        //    }
        //}






      

        private void label23_Click(object sender, EventArgs e)
        {
            GlobalVariable.fromwhere6 = "main"; 
            GlobalVariable.selectedOwnObject = null;
            Form6 form6 = new Form6();
            form6.Show();

        }

        private void label24_Click(object sender, EventArgs e)
        {
            GlobalVariable.fromwhere6 = "sub";
            Form7 form7 = new Form7();
            form7.Show();
        }

        private void wishListLable_Click(object sender, EventArgs e)
        {
            string filepath = "";
            string allobject = "";
            try
            {
                GlobalVariable.fromwhere = "sub";
               
                var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string path = Path.Combine(directory, "Arvand", "wishList.txt");
                if (System.IO.File.Exists(path) == false)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {

                    }
                }
                using (StreamReader sr = new StreamReader(path, true))
                {

                    allobject = sr.ReadToEnd();

                }
            }
            catch 
            {
                MessageBox.Show("error");
            }
          
            List<ListOfAdds.Datum> list = new List<ListOfAdds.Datum>();
            List<ListOfAdds.Datum> list2 = new List<ListOfAdds.Datum>();
            try
            {
                list2 = JsonConvert.DeserializeObject<List<ListOfAdds.Datum>>(allobject);

            }
            catch(Exception f)
            {

            }
            if (list2 != null)
            {
                foreach (var item in list2)
                {
                    list.Add(item);
                }
            }

            if (list.Count > 0)
            {
                ListOfAdds.RootObject log = new ListOfAdds.RootObject();
                ListOfAdds.Result result = new ListOfAdds.Result();
                List<ListOfAdds.Datum> data = new List<ListOfAdds.Datum>();
                data = list;
                result.data = data;
                result.today_files = 0;
                log.result = result;
                log.result.today_files = list.Count();


                string final = JsonConvert.SerializeObject(log);
                //GlobalVariable.result = final;
                RefreshListhWithNewData(log);
            }
            else
            {
                ListOfAdds.RootObject log = new ListOfAdds.RootObject();
                ListOfAdds.Result result = new ListOfAdds.Result();
                List<ListOfAdds.Datum> data = new List<ListOfAdds.Datum>();

                result.data = data;
                result.today_files = 0;
                log.result = result;
                log.result.today_files = 0;


                string final = JsonConvert.SerializeObject(log);
               // GlobalVariable.result = final;
                RefreshListhWithNewData(log);

            }





        }
        public void wishfromsearch()
        {
            GlobalVariable.fromwhere = "sub";
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = Path.Combine(directory, "Arvand", "wishList.txt");
            string allobject = System.IO.File.ReadAllText(path);
            List<ListOfAdds.Datum> list = new List<ListOfAdds.Datum>();
            List<ListOfAdds.Datum> list2 = new List<ListOfAdds.Datum>();
            try
            {
                list2 = JsonConvert.DeserializeObject<List<ListOfAdds.Datum>>(allobject);

            }
            catch
            {
            }
            if (list2 != null)
            {
                foreach (var item in list2)
                {
                    list.Add(item);
                }
            }

            if (list.Count > 0)
            {
                ListOfAdds.RootObject log = new ListOfAdds.RootObject();
                ListOfAdds.Result result = new ListOfAdds.Result();
                List<ListOfAdds.Datum> data = new List<ListOfAdds.Datum>();
                data = list;
                result.data = data;
                result.today_files = 0;
                log.result = result;
                log.result.today_files = list.Count();


                string final = JsonConvert.SerializeObject(log);
                GlobalVariable.result = final;
                RefreshListhWithNewData(log);
            }
            else
            {
                ListOfAdds.RootObject log = new ListOfAdds.RootObject();
                ListOfAdds.Result result = new ListOfAdds.Result();
                List<ListOfAdds.Datum> data = new List<ListOfAdds.Datum>();

                result.data = data;
                result.today_files = 0;
                log.result = result;
                log.result.today_files = 0;


                string final = JsonConvert.SerializeObject(log);
                GlobalVariable.result = final;
                RefreshListhWithNewData(log);

            }
        }
        private void refresh_Click(object sender, EventArgs e)
        {

        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.Show();
        }


        private void label64_Click(object sender, EventArgs e)
        {
           // getDataFromServer();
        }

    

        private void radPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radScrollablePanel2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            bool searchEX = false;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "search")
                {
                    searchEX = true;
                    break;
                }
            }
            if (searchEX)
            {
                this.Close();
            }
            else
            {
                GlobalVariable.relatedAddress = "";
                search search = new search();

                search.Show();
            }
                
    
           
           // this.Close();
        }

        private void allpageholder_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void label56_Click(object sender, EventArgs e)
        {
            try
            {
                gifholder.Visible = true;
                string finalstring = "";
                List<string> listStrLineElements = new List<string>();
                string mysrt = GlobalVariable.temporaryOwnList;
                mysrt = mysrt.Remove(mysrt.Length - 1);

                listStrLineElements = mysrt.Split(',').ToList();
                if (listStrLineElements.Count() > 0)
                {
                    foreach (var item in listStrLineElements)
                    {

                        //foreach (RadElement element in this.radListView1.ListViewElement.ViewElement.ViewElement.Children)
                        //{
                        //   // CustomListViewVisualItem visualItem = element as CustomListViewVisualItem;
                        //}


                        //ListOfAdds.RootObject log = JsonConvert.DeserializeObject<ListOfAdds.RootObject>(GlobalVariable.result);
                        //string ADDRESS = log.result.data.Where(x => x.ID == item).ToList().First().address;
                        finalstring = finalstring + item + "+++";
                    }
                }
                GlobalVariable.relatedAddress = finalstring;
                GlobalVariable.temporaryOwnList = "";
                getDataFromServer(null);
            }
            catch 
            {
                gifholder.Visible = false;
            }
            
        }

       

      

       

        private void label4_Click(object sender, EventArgs e)
        {
            string allobject = "";
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = Path.Combine(directory, "Arvand", "objects.txt");
            if (System.IO.File.Exists(path) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {

                }
            }
            using (StreamReader sr = new StreamReader(path, true))
            {

                allobject = sr.ReadToEnd();

            }
            

            List<ListOfAdds.Datum> list = new List<ListOfAdds.Datum>();
            List<ListOfAdds.Datum> list2 = new List<ListOfAdds.Datum>();
            try
            {
                list2 = JsonConvert.DeserializeObject<List<ListOfAdds.Datum>>(allobject);

            }
            catch
            {
            }
            if (list2 != null)
            {
                foreach (var item in list2)
                {
                    list.Add(item);
                }
            }



           
            
           


            string finalstring = "";
            List<string> listStrLineElements = new List<string>();
            string mysrt = GlobalVariable.temporaryOwnList;
            mysrt = mysrt.Remove(mysrt.Length - 1);

            listStrLineElements = mysrt.Split(',').ToList();
            if (listStrLineElements.Count() > 0)
            {
                ListOfAdds.Datum newobj = new ListOfAdds.Datum();
                foreach (var item in listStrLineElements)
                {

                    ListOfAdds.RootObject log = JsonConvert.DeserializeObject<ListOfAdds.RootObject>(GlobalVariable.result);
                    newobj = log.result.data.Where(x => x.ID == item).First();
                    list.Add(newobj);
                }
            }
            string jsonmodel = JsonConvert.SerializeObject(list);
            try
            {
                System.IO.File.WriteAllText(path, string.Empty);
                System.IO.File.WriteAllText(path, jsonmodel);
                MessageBox.Show("فایل های مورد نظر با موفقیت به فایل های اختصاصی شما اضافه شد");

            }
            catch (Exception)
            {

            }

            GlobalVariable.temporaryOwnList = "";
           




        }


        private void radListView1_Click(object sender, EventArgs e)
        {

            string selectedindex = radListView1.SelectedItem.Value.ToString();
            int index = GlobalVariable.RowIDListForPhone.IndexOf(selectedindex);
            ownerPhone.Text = GlobalVariable.RowPhoneList[index];
           
        }
        private void radListView1_DoubleClick(object sender, EventArgs e)
        {
            GlobalVariable.fromwhere = "main";
            string selectedindex = radListView1.SelectedItem.Value.ToString();
            GlobalVariable.selectedIDofList = selectedindex;
            gifholder.Visible = true;
            // refreshPic.Visible = false;
            List<string> lst = GlobalVariable.RowIDList;
            getDataFromServer( int.Parse(selectedindex));
          
        }


        public void getDataFromServer(int? selectedID)
        {

            BackgroundWorker getDataBackGroundWorker = new BackgroundWorker();
            getDataBackGroundWorker.WorkerSupportsCancellation = true;
            getDataBackGroundWorker.DoWork += new DoWorkEventHandler(getDataBackGroundWorker_do);
            getDataBackGroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(getDataBackGroundWorker_done);

            try
            {
                queryModel model = new queryModel()
                {

                };
                if(selectedID != null)
                {
                    model.ID = selectedID.ToString();
                    GlobalVariable.form2MustWait = true;

                }
                model.relatedAddress = GlobalVariable.relatedAddress;

                string str = JsonConvert.SerializeObject(model);

                getDataBackGroundWorker.RunWorkerAsync(argument: str);
                refresh.Visible = true;
            }
            catch (Exception)
            {

                MessageBox.Show("فرم جستجو را کامل کنید ");
                return;
            }


        }
        void bg_DoWork(object sender, DoWorkEventArgs e)
        {

            int value = (int)e.Argument;



            int port = value;
            while (ContinuAllListener)
            {
                if (workers[value].CancellationPending == true)
                {
                    e.Cancel = true;
                    ContinuAllListener = false;
                }
                else
                {
                    try
                    {

                        listenerlist[port].Start();
                        Console.WriteLine("The server is running at port " + port + "...");
                        Console.WriteLine("The local End point is  :" + listenerlist[port].LocalEndpoint);
                        Console.WriteLine("Waiting for a connection.....");

                        lisenerdetaillist[port].newsocket = listenerlist[port].AcceptSocket();
                        Console.WriteLine("Connection accepted from " + lisenerdetaillist[port].newsocket.RemoteEndPoint);

                        Encoding utf8 = Encoding.UTF8;

                        lisenerdetaillist[port].RecievedByteCount = lisenerdetaillist[port].newsocket.Receive(lisenerdetaillist[port].b);
                        Console.WriteLine("Recieved...");




                        lisenerdetaillist[port].message = utf8.GetString(lisenerdetaillist[port].b);

                        string mes = "";
                        //while (lisenerdetaillist[port].counter < lisenerdetaillist[port].RecievedByteCount)
                        //{
                        //    Console.Write(Convert.ToChar(lisenerdetaillist[port].b[lisenerdetaillist[port].counter]));
                        //    lisenerdetaillist[port].message = lisenerdetaillist[port].message + Convert.ToChar(lisenerdetaillist[port].b[lisenerdetaillist[port].counter]);
                        //    lisenerdetaillist[port].counter += 1;
                        //}

                        //string json2;
                        //using (var client = new WebClient())
                        //{
                        //    json2 = client.DownloadString("http://api.backino.net/melkagahi/syncEngine.php");
                        //}


                        //RootObject log = JsonConvert.DeserializeObject<RootObject>(json2);





                        // یک ینی کت اند اریا

                        // دو یعنی درخواست خود عکس که عدد عکس هم تو درخواست می آد

                        //if (Convert.ToChar(lisenerdetaillist[port].b[0]).ToString() == "2")
                        //{
                        //    try
                        //    {
                        //        //lisenerdetaillist[port].imageNumberSent = Convert.ToInt32(Convert.ToChar(lisenerdetaillist[port].b[1]).ToString()) - 1;
                        //        //lisenerdetaillist[port].datumselected = (from p in log.result.data
                        //        //                                         where p.server_id == GlobalVariable.selectedIDofList
                        //        //                                         select p).SingleOrDefault();


                        //        //lisenerdetaillist[port].imageurlForDownload = lisenerdetaillist[port].datumselected.images[lisenerdetaillist[port].imageNumberSent];
                        //        lisenerdetaillist[port].imageurlForDownload = lisenerdetaillist[port].message.Substring(1, lisenerdetaillist[port].message.Length - 1);
                        //        int index = lisenerdetaillist[port].imageurlForDownload.IndexOf("\0");
                        //        string address = lisenerdetaillist[port].imageurlForDownload.Substring(0, index);

                        //        lisenerdetaillist[port].imageurlForDownload = address;
                        //        if (lisenerdetaillist[port].imageurlForDownload != "")
                        //        {
                        //            MD5 md5Hash = MD5.Create();
                        //            string hash = GetMd5Hash(md5Hash, lisenerdetaillist[port].imageurlForDownload) + ".jpg";
                        //            string filepath = Path.Combine(Application.StartupPath, "Images", hash);
                        //            var webClient = new WebClient();
                        //            if ( File.Exists(filepath))
                        //            {
                        //                while (true)
                        //                {
                        //                    try
                        //                    {

                        //                        lisenerdetaillist[port].newsocket.Send(webClient.DownloadData(filepath));
                        //                        break;
                        //                    }
                        //                    catch (Exception)
                        //                    {

                        //                    }
                        //                }
                        //            }


                        //            else
                        //            {
                        //                webClient.DownloadFile(new Uri(lisenerdetaillist[port].imageurlForDownload), filepath);
                        //                lisenerdetaillist[port].newsocket.Send(webClient.DownloadData(filepath));

                        //            }
                        //            listenerlist[port].Stop();
                        //        }

                        //    }
                        //    catch (Exception)
                        //    {

                        //        ASCIIEncoding asen = new ASCIIEncoding();
                        //        lisenerdetaillist[port].newsocket.Send(asen.GetBytes("Error"));
                        //        listenerlist[port].Stop();
                        //        Console.WriteLine("Error..... ");
                        //    }




                        //    //Console.WriteLine("\nSent Acknowledgement");
                        //}
                        //// یوزر و پس
                        if (Convert.ToChar(lisenerdetaillist[port].b[0]).ToString() == "4")
                        {
                            try
                            {
                                lisenerdetaillist[port].loginmodel = JsonConvert.DeserializeObject<login>(lisenerdetaillist[port].message.Substring(1, lisenerdetaillist[port].message.Length - 1));
                                while (true)
                                {
                                    try
                                    {
                                        var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                                        string filepath = Path.Combine(directory, "Arvand", "text.txt");
                                        lisenerdetaillist[port].loginDB = System.IO.File.ReadAllText(filepath);
                                        break;
                                    }
                                    catch (Exception errr)
                                    {

                                    }
                                }
                                if (JsonConvert.DeserializeObject<List<login>>(lisenerdetaillist[port].loginDB).Any(i => i.username == lisenerdetaillist[port].loginmodel.username && i.password == lisenerdetaillist[port].loginmodel.password))
                                {
                                    bool isgo = true;
                                    string json5 = "";
                                    string error = "0";
                                    while (isgo)
                                    {
                                        try
                                        {
                                            using (WebClient client = new WebClient())
                                            {
                                                var collection = new System.Collections.Specialized.NameValueCollection();
                                                collection.Add("token", GlobalVariable.token);
                                                byte[] response =
                                                client.UploadValues("http://arvandfile.com/api/v1/getCats&Areas.php", collection);

                                                json5 = System.Text.Encoding.UTF8.GetString(response);

                                            }
                                            isgo = false;
                                        }
                                        catch (Exception)
                                        {

                                            error = "1";
                                            isgo = false;
                                        }
                                    }

                                    if (error == "1")
                                    {
                                        loginback answer = new loginback()
                                        {

                                            status = "خطای ارتباط با سرور",
                                            token = "",

                                        };
                                        ASCIIEncoding asen = new ASCIIEncoding();
                                        lisenerdetaillist[port].newsocket.Send(asen.GetBytes(JsonConvert.SerializeObject(answer)));
                                        listenerlist[port].Stop();
                                    }
                                    else
                                    {
                                        CatsAndAreasObject autocompleteObject = JsonConvert.DeserializeObject<CatsAndAreasObject>(json5);
                                        loginback answer = new loginback()
                                        {

                                            status = "success",
                                            token = GlobalVariable.token,
                                            autocompleteObject = autocompleteObject
                                        };
                                        ASCIIEncoding asen = new ASCIIEncoding();
                                        string RE = JsonConvert.SerializeObject(answer);
                                        lisenerdetaillist[port].newsocket.Send(utf8.GetBytes(RE));
                                        listenerlist[port].Stop();
                                    }




                                }
                                else
                                {
                                    loginback answer = new loginback()
                                    {

                                        status = "error2",
                                        token = "",

                                    };
                                    ASCIIEncoding asen = new ASCIIEncoding();
                                    lisenerdetaillist[port].newsocket.Send(asen.GetBytes(JsonConvert.SerializeObject(answer)));
                                    listenerlist[port].Stop();
                                }
                            }
                            catch (Exception errorrr)
                            {

                                ASCIIEncoding asen = new ASCIIEncoding();
                                lisenerdetaillist[port].newsocket.Send(asen.GetBytes("Error"));
                                listenerlist[port].Stop();
                                Console.WriteLine("Error..... ");
                            }
                            // lisenerdetaillist[port].loginjson = lisenerdetaillist[port].message.Substring(1, lisenerdetaillist[port].message.Length - 1);



                        }
                        // سه ینی درخواست اطلاعات یه محصول
                        else if (Convert.ToChar(lisenerdetaillist[port].b[0]).ToString() == "3")
                        {
                            try
                            {
                                string model = JsonConvert.SerializeObject(lisenerdetaillist[port].datumselected);
                                ASCIIEncoding asen = new ASCIIEncoding();
                                lisenerdetaillist[port].newsocket.Send(asen.GetBytes(model));
                                listenerlist[port].Stop();
                            }
                            catch (Exception)
                            {

                                ASCIIEncoding asen = new ASCIIEncoding();
                                lisenerdetaillist[port].newsocket.Send(asen.GetBytes("Error"));
                                listenerlist[port].Stop();
                                Console.WriteLine("Error..... ");
                            }
                            // GlobalVariable.result


                        }

                        else
                        {
                            try
                            {


                                string kj = lisenerdetaillist[port].message;

                                lisenerdetaillist[port].queryModel = JsonConvert.DeserializeObject<queryModel>(kj);

                                string result = "";
                                using (WebClient client = new WebClient())
                                {
                                    var collection = new System.Collections.Specialized.NameValueCollection();
                                    collection.Add("token", GlobalVariable.token);

                                    collection.Add("mantaghe_name", lisenerdetaillist[port].queryModel.mantaghe_name);
                                    collection.Add("mantaghe_id", lisenerdetaillist[port].queryModel.mantaghe_id);
                                    collection.Add("ashpazkhane", lisenerdetaillist[port].queryModel.ashpazkhane);
                                    collection.Add("apartment", lisenerdetaillist[port].queryModel.apartment);
                                    collection.Add("villa", lisenerdetaillist[port].queryModel.villa);
                                    collection.Add("mostaghellat", lisenerdetaillist[port].queryModel.mostaghellat);
                                    collection.Add("kolangi", lisenerdetaillist[port].queryModel.kolangi);
                                    collection.Add("office", lisenerdetaillist[port].queryModel.office);
                                    collection.Add("kind", lisenerdetaillist[port].queryModel.kind);



                                    collection.Add("isForoosh", lisenerdetaillist[port].queryModel.isForoosh);
                                    collection.Add("isEjare", lisenerdetaillist[port].queryModel.isEjare);
                                    collection.Add("isRahn", lisenerdetaillist[port].queryModel.isRahn);
                                    collection.Add("isMosharekat", lisenerdetaillist[port].queryModel.isMosharekat);
                                    collection.Add("isMoaveze", lisenerdetaillist[port].queryModel.isMoaveze);
                                    collection.Add("hasEstakhr", lisenerdetaillist[port].queryModel.hasEstakhr);
                                    collection.Add("hasSauna", lisenerdetaillist[port].queryModel.hasSauna);
                                    collection.Add("hasJakoozi", lisenerdetaillist[port].queryModel.hasJakoozi);
                                    collection.Add("sell2khareji", lisenerdetaillist[port].queryModel.sell2khareji);
                                    collection.Add("asansor", lisenerdetaillist[port].queryModel.asansor);
                                    collection.Add("anbari", lisenerdetaillist[port].queryModel.anbari);
                                    collection.Add("seraydar", lisenerdetaillist[port].queryModel.seraydar);
                                    collection.Add("hasGym", lisenerdetaillist[port].queryModel.hasGym);
                                    collection.Add("hasShooting", lisenerdetaillist[port].queryModel.hasShooting);
                                    collection.Add("hasHall", lisenerdetaillist[port].queryModel.hasHall);
                                    collection.Add("hasRoofGarden", lisenerdetaillist[port].queryModel.hasRoofGarden);
                                    collection.Add("isMoble", lisenerdetaillist[port].queryModel.isMoble);
                                    collection.Add("hasLobbyMan", lisenerdetaillist[port].queryModel.labi);
                                    collection.Add("parking", lisenerdetaillist[port].queryModel.parking);



                                    collection.Add("address", lisenerdetaillist[port].queryModel.address);
                                    collection.Add("tabaghe", lisenerdetaillist[port].queryModel.tabaghe);
                                    collection.Add("desc", lisenerdetaillist[port].queryModel.desc);
                                    collection.Add("phones", lisenerdetaillist[port].queryModel.phones);
                                    collection.Add("malek", lisenerdetaillist[port].queryModel.malek);
                                    collection.Add("wc", lisenerdetaillist[port].queryModel.wc);
                                    collection.Add("title", lisenerdetaillist[port].queryModel.title);
                                    collection.Add("ID", lisenerdetaillist[port].queryModel.ID);
                                    collection.Add("related_address", lisenerdetaillist[port].queryModel.relatedAddress);




                                    collection.Add("price_from", lisenerdetaillist[port].queryModel.total_price_from);
                                    collection.Add("price_to", lisenerdetaillist[port].queryModel.total_price_to);

                                    collection.Add("rahn_from", lisenerdetaillist[port].queryModel.rahn_from);
                                    collection.Add("rahn_to", lisenerdetaillist[port].queryModel.rahn_to);
                                    collection.Add("ejare_from", lisenerdetaillist[port].queryModel.ejare_from);
                                    collection.Add("ejare_to", lisenerdetaillist[port].queryModel.ejare_to);
                                    collection.Add("metri_from", lisenerdetaillist[port].queryModel.metri_from);
                                    collection.Add("metri_to", lisenerdetaillist[port].queryModel.metri_to);
                                    collection.Add("metraj_from", lisenerdetaillist[port].queryModel.zirbana_from);
                                    collection.Add("metraj_to", lisenerdetaillist[port].queryModel.zirbana_to);
                                    collection.Add("tabaghat_from", lisenerdetaillist[port].queryModel.tabaghe_from);
                                    collection.Add("tabaghat_to", lisenerdetaillist[port].queryModel.tabaghe_to);
                                    collection.Add("date_from", lisenerdetaillist[port].queryModel.date_from);
                                    collection.Add("date_to", lisenerdetaillist[port].queryModel.date_to);
                                    collection.Add("senn_from", lisenerdetaillist[port].queryModel.senn_from);
                                    collection.Add("senn_to", lisenerdetaillist[port].queryModel.senn_to);
                                    collection.Add("masahat_from", lisenerdetaillist[port].queryModel.masahat_from);
                                    collection.Add("masahat_to", lisenerdetaillist[port].queryModel.masahat_to);
                                    collection.Add("bed_from", lisenerdetaillist[port].queryModel.bed_from);
                                    collection.Add("bed_to", lisenerdetaillist[port].queryModel.bed_to);
                                    collection.Add("fileid_from", lisenerdetaillist[port].queryModel.id_from);
                                    collection.Add("fileid_to", lisenerdetaillist[port].queryModel.id_to);
                                    collection.Add("limit", "1000");










                                  //  string json = JsonConvert.SerializeObject(collection, Formatting.Indented);
                                    byte[] response =
                                    client.UploadValues("http://arvandfile.com/api/v1/listOfAds.php", collection);

                                    result = System.Text.Encoding.UTF8.GetString(response);
                                }

                                if (result.Contains("Invalid Token"))
                                {
                                    GlobalVariable.result = "";
                                    foreach (var item in listenerlist)
                                    {
                                        item.Stop();
                                    }
                                    foreach (var item in lisenerdetaillist)
                                    {
                                        item.newsocket.Close();
                                        item.newsocket = null;
                                    }
                                    listenerlist.Clear();
                                    lisenerdetaillist.Clear();
                                    return;
                                }

                                ASCIIEncoding asen = new ASCIIEncoding();
                                lisenerdetaillist[port].newsocket.Send(utf8.GetBytes(result));
                                listenerlist[port].Stop();
                                Console.WriteLine("\nSent Acknowledgement");
                            }
                            catch (Exception r)
                            {

                                ASCIIEncoding asen = new ASCIIEncoding();
                                lisenerdetaillist[port].newsocket.Send(asen.GetBytes("Error"));
                                listenerlist[port].Stop();
                                Console.WriteLine("Error..... ");
                            }

                        }



                        /* clean up */

                        lisenerdetaillist[port].newsocket.Close();
                        lisenerdetaillist[port].newsocket = null;
                        lisenerdetaillist[port].b = new byte[4000];
                        lisenerdetaillist[port].datumselected = new ListOfAdds.Datum();
                        lisenerdetaillist[port].imageurlForDownload = "";
                        lisenerdetaillist[port].imageNumberSent = 0;
                        lisenerdetaillist[port].imagenum = new byte[1];
                        lisenerdetaillist[port].message = "";
                        lisenerdetaillist[port].RecievedByteCount = 0;
                        lisenerdetaillist[port].counter = 0;



                    }
                    catch (Exception h)
                    {
                        //MessageBox.Show("خطا در برقراری اتصال با آی پی سرور");
                        e.Result = "error";
                        workers[port].CancelAsync();
                    }
                }

                // mybool = false;
            }

        }
        private void getDataBackGroundWorker_done(object sender, RunWorkerCompletedEventArgs e)
        {

            try
            {
                if (e.Result == "error")
                {
                    refresh.Visible = false;
                }
                else
                {
                    gifholder.Visible = false;
                    if (GlobalVariable.form2MustWait == true)
                    {
                        GlobalVariable.form2MustWait = false;
                       
                        Form2 form2 = new Form2();
                        form2.Show();
                    }
                    else
                    {
                        ListOfAdds.RootObject log = JsonConvert.DeserializeObject<ListOfAdds.RootObject>(GlobalVariable.result);
                        GlobalVariable.RowIDList.Clear();
                        foreach (var item in log.result.data)
                        {
                            GlobalVariable.RowIDList.Add(item.ID);
                            
                        }
                      
                        RefreshListhWithNewData(log);
                    }
                   
                }
            }
            catch (Exception)
            {

                MessageBox.Show("موردی وجود ندارد");
                refresh.Visible = false;
            }



        }

        void getDataBackGroundWorker_do(object sender, DoWorkEventArgs e)
        {


            string query = (string)e.Argument;
            // فعلاً پورت 8001 باشه
            int port = 8002;
            if (GlobalVariable.port != 0)
            {
                port = GlobalVariable.port;
            }

            TcpClient tcpclnt = new TcpClient();

            string ip = Settings1.Default.ServerIP;
            if (GlobalVariable.serverIP != null)
            {
                ip = GlobalVariable.serverIP;
            }
            try
            {
                tcpclnt.Connect(ip, port);
            }
            catch (Exception)
            {

                MessageBox.Show("خطا در اتصال به سرور");
                e.Result = "error";
                tcpclnt.Close();
                return;
            }


            try
            {
                // use the ipaddress as in the server program
                Console.WriteLine("Connected with port" + port.ToString());
                //Console.Write("Enter the string to be transmitted : ");

                //String str = Console.ReadLine();

                Stream stm = tcpclnt.GetStream();
                Encoding utf8 = Encoding.UTF8;
                // ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = utf8.GetBytes(query);
                Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length);
                string json = "";

                const int blockSize = 20000000;
                byte[] buffer = new byte[blockSize];
                int bytesRead;
                stm.Read(buffer, 0, buffer.Length);
                json = utf8.GetString(buffer);
                //while ((bytesRead = stm.Read(buffer, 0, buffer.Length)) > 0)
                //{
                //    for (int i = 0; i < bytesRead; i++)
                //    {
                //        json = json + Convert.ToChar(buffer[i]);
                //    }
                //}



                GlobalVariable.result = json;



                tcpclnt.Close();


            }
            catch (Exception f)
            {

                MessageBox.Show("خطا در ارتباط با سرور");

                tcpclnt.Close();
                return;
                e.Result = "error";
            }
        }
        
        public class CustomDetailListViewDataCellElement : DetailListViewDataCellElement
        {

            public mehrdadPanel obj = null;

            public CustomDetailListViewDataCellElement(DetailListViewVisualItem owner,
                ListViewDetailColumn column)
                : base(owner, column)
            {

            }


            protected override void CreateChildElements()
            {
                base.CreateChildElements();


                obj = new mehrdadPanel(width);


                this.Children.Add(obj.getView());
            }

            protected override Type ThemeEffectiveType
            {
                get
                {
                    return typeof(DetailListViewHeaderCellElement);
                }
            }

            public override void Synchronize()
            {
                base.Synchronize();
                this.Text = "";
                // DataRowView rowView = this.Row.DataBoundItem as DataRowView;
                DataRowView rowView = this.Row.DataBoundItem as DataRowView;
                //this.button.Text = "ddd";
                if (GlobalVariable.searchTabghe == "")
                {
                    GlobalVariable.searchTabghe = "1";
                }
                string tabaghe = GlobalVariable.searchTabghe;
                string fullprice = "tabaghe_1_total_price";
                string metriprice = "tabaghe_1_metri";
                string rahnprice = "tabaghe_1_rahn";
                string ejareprice = "tabaghe_1_ejare";
                string tabagh = "tabaghe1";
                string kha = "bed1";
                string zirban = "zirbana1";
                string tb1 = rowView.Row["tabaghe1"].ToString();
                string tb2 = rowView.Row["tabaghe2"].ToString();
                string tb3 = rowView.Row["tabaghe3"].ToString();

                if (tb1 == tabaghe)
                {
                    fullprice = "tabaghe_1_total_price";
                    metriprice = "tabaghe_1_metri";
                    rahnprice = "tabaghe_1_rahn";
                    ejareprice = "tabaghe_1_ejare";
                    tabagh = "tabaghe1";
                    kha = "bed1";
                    zirban = "zirbana1";
                }
                if (tb2 == tabaghe)
                {
                    fullprice = "tabaghe_2_total_price";
                    metriprice = "tabaghe_2_metri";
                    rahnprice = "tabaghe_2_rahn";
                    ejareprice = "tabaghe_2_ejare";
                    tabagh = "tabaghe2";
                    kha = "bed2";
                    zirban = "zirbana2";
                }
                if (tb3 == tabaghe)
                {
                    fullprice = "tabaghe_3_total_price";
                    metriprice = "tabaghe_3_metri";
                    rahnprice = "tabaghe_3_rahn";
                    ejareprice = "tabaghe_3_ejare";
                    tabagh = "tabaghe3";
                    kha = "bed3";
                    zirban = "zirbana3";
                }

                CatsAndAreasObject CATS = new CatsAndAreasObject();
                try
                {
                    CATS = JsonConvert.DeserializeObject<CatsAndAreasObject>(GlobalVariable.newCatsAndAreas);
                }
                catch 
                {

                    CATS = GlobalVariable.catsAndAreas;
                }
                
                string serverid = rowView.Row["ID"].ToString();
                string date = rowView.Row["date_updated"].ToString();
                string address = rowView.Row["address"].ToString();
                string owner = rowView.Row["malek"].ToString();
                string senn = rowView.Row["senn"].ToString() == "0" ? "-" : (from q in CATS.result.senn
                                                                   where q.ID == rowView.Row["senn"].ToString()
                                                                             select q.title).First();
                //if (rowView.Row["senn"].ToString() == "1")
                //{
                //    senn = "قدیمی";
                //}
                //else if (rowView.Row["senn"].ToString() == "2")
                //{
                //    senn = "نوساز";
                //}
                //else
                //{ 
                //    senn =(int.Parse(rowView.Row["senn"].ToString()) - 2).ToString();
                //}
                string melkkind = "";
                
                if (Convert.ToInt32(rowView.Row["maghaze"].ToString()) > 0)
                {
                    melkkind = melkkind + "مغازه،";
                }
                if (Convert.ToInt32(rowView.Row["apartment"].ToString()) > 0)
                {
                    melkkind = melkkind + "آپارتمان،";
                }
                if (Convert.ToInt32(rowView.Row["villa"].ToString()) > 0)
                {
                    melkkind = melkkind + "ویلا،";
                }
                if (Convert.ToInt32(rowView.Row["mostaghellat"].ToString()) > 0)
                {
                    melkkind = melkkind + "مستغلات،";
                }
                if (Convert.ToInt32(rowView.Row["kolangi"].ToString()) > 0)
                {
                    melkkind = melkkind + "کلنگی،";
                }
                if (Convert.ToInt32(rowView.Row["office"].ToString()) > 0)
                {
                    melkkind = melkkind + "دفتر،";
                }
                if (melkkind.Length > 0)
                {
                    melkkind = melkkind.Remove(melkkind.Length - 1, 1);
                }

                string Dealkind = "";
                if (Convert.ToInt32(rowView.Row["isForoosh"].ToString()) > 0)
                {
                    Dealkind = Dealkind + "فروش،";
                }
                if (Convert.ToInt32(rowView.Row["isRahn"].ToString()) > 0)
                {
                    Dealkind = Dealkind + "رهن،";
                }
                if (Convert.ToInt32(rowView.Row["isEjare"].ToString()) > 0)
                {
                    Dealkind = Dealkind + "اجاره،";
                }
                if (Dealkind.Length > 0)
                {
                    Dealkind = Dealkind.Remove(Dealkind.Length - 1, 1);
                }


                string totalrahn = rowView.Row["isForoosh"].ToString() == "1" ? rowView.Row[fullprice].ToString() : rowView.Row[rahnprice].ToString();
                string metriejare = rowView.Row["isForoosh"].ToString() == "1" ? rowView.Row[metriprice].ToString() : rowView.Row[ejareprice].ToString();


                string Rtabaghe = rowView.Row[tabagh].ToString();
                string khab = rowView.Row[kha].ToString();
                string zirbana = rowView.Row[zirban].ToString();



                bool mycheckbox = false;
                totalrahn = totalrahn.Replace(".", "");
                if (totalrahn == "0")
                {
                    totalrahn = "-";
                }
                else if (Convert.ToInt64(totalrahn) > 0)
                {
                    string mytotal = string.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToInt64(totalrahn));
                    totalrahn = mytotal;
                }
                else if (totalrahn == "-1")
                {
                    totalrahn = "توافقی";
                }
                else if (totalrahn == "-2")
                {
                    totalrahn = "رایگان";
                }

                metriejare = metriejare.Replace(".", "");
                if (Convert.ToInt64(metriejare) == 0)
                {
                    metriejare = "-";
                }
                else if (Convert.ToInt64(metriejare) > 0)
                {
                    string mymetriejare = string.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToInt64(metriejare));
                    metriejare = mymetriejare;
                }
                else if (metriejare == "-1")
                {
                    metriejare = "توافقی";
                }
                else if (metriejare == "-2")
                {
                    metriejare = "رایگان";
                }


                if (GlobalVariable.temporaryOwnList.Contains(serverid + ","))
                {
                    mycheckbox = true;
                }

                obj.setTitle(serverid, date, owner, melkkind, Dealkind, totalrahn, metriejare, Rtabaghe, khab, zirbana, mycheckbox, address,senn);
               
            }

            public override bool IsCompatible(ListViewDetailColumn data, object context)
            {
                if (data.Name != "title")
                {
                    return false;
                }
                return base.IsCompatible(data, context);
            }
        }
        public static class Util
        {
            public enum Effect { Roll, Slide, Center, Blend }

            public static void Animate(Control ctl, Effect effect, int msec, int angle)
            {
                int flags = effmap[(int)effect];
                if (ctl.Visible) { flags |= 0x10000; angle += 180; }
                else
                {
                    if (ctl.TopLevelControl == ctl) flags |= 0x20000;
                    else if (effect == Effect.Blend) throw new ArgumentException();
                }
                flags |= dirmap[(angle % 360) / 45];
                bool ok = AnimateWindow(ctl.Handle, msec, flags);
                if (!ok) throw new Exception("Animation failed");
                ctl.Visible = !ctl.Visible;
            }

            private static int[] dirmap = { 1, 5, 4, 6, 2, 10, 8, 9 };
            private static int[] effmap = { 0, 0x40000, 0x10, 0x80000 };

            [DllImport("user32.dll")]
            private static extern bool AnimateWindow(IntPtr handle, int msec, int flags);
        }

        private void connect_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

       
    }




}





//public void docopy()
//{
//    string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
//    string desfolder = System.IO.Path.Combine(path, "Datafolder");
//    if (!System.IO.Directory.Exists(desfolder))
//    {
//        System.IO.Directory.CreateDirectory(desfolder);
//    }
//    string desfile = System.IO.Path.Combine(desfolder, "myrealstatedes.mdf");
//    string desfile2 = System.IO.Path.Combine(desfolder, "myrealstatedes_log.ldf");

//    string source = "C:\\Users\\merioli";
//    string sourcFile = System.IO.Path.Combine(source, "myrealstate.mdf");
//    string sourcFile2 = System.IO.Path.Combine(source, "myrealstate_log.ldf");

//    try
//    {
//        if (File.Exists(sourcFile))
//        {
//            System.IO.File.Copy(sourcFile, desfile, true);
//        }
//        if (File.Exists(sourcFile2))
//        {
//            System.IO.File.Copy(sourcFile2, desfile2, true);
//        }
//    }
//    catch (Exception)
//    {

//    }



//}

