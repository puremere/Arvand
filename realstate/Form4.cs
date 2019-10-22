using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;

namespace realstate
{
    public partial class Form4 : Form
    {

        List<TextBox> nameList = new List<TextBox>();
        List<TextBox> usernameList = new List<TextBox>();
        List<TextBox> passList = new List<TextBox>();
        List<int > portList = new List<int>();

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

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        #endregion

        public Form4()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.IsMdiContainer = true;

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            activezone.Text = GlobalVariable.activezonse;
            expire_date.Text = GlobalVariable.expirationdate;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            label9.Font = GlobalVariable.headerlistFONT;
            label8.Font = GlobalVariable.headerlistFONT;
            label7.Font = GlobalVariable.headerlistFONT;
            label6.Font = GlobalVariable.headerlistFONT;
            label5.Font = GlobalVariable.headerlistFONT;
            label4.Font = GlobalVariable.headerlistFONT;
            label3.Font = GlobalVariable.headerlistFONT;
            label2.Font = GlobalVariable.headerlistFONTBold;
            label15.Font = GlobalVariable.headerlistFONT;
            expire_date.Font = GlobalVariable.headerlistFONT;
            label13.Font = GlobalVariable.headerlistFONT;
            label12.Font = GlobalVariable.headerlistFONT;
            label11.Font = GlobalVariable.headerlistFONT;
            label10.Font = GlobalVariable.headerlistFONT;
            ipholder.Font = GlobalVariable.headerlistFONT;

            label1.Font = GlobalVariable.headerlistFONT;
            activezone.Font = GlobalVariable.headerlistFONT;


            if (Settings1.Default.ServerIP != null)
            {
                ipholder.Text = Settings1.Default.ServerIP;
            }
           
            PanelOne.PanelElement.Shape = new RoundRectShape();
            PanelOne.PanelElement.PanelFill.GradientStyle = GradientStyles.Solid;
            PanelOne.PanelElement.PanelFill.BackColor = Color.WhiteSmoke;

            PanelTwo.PanelElement.Shape = new RoundRectShape();
            PanelTwo.PanelElement.PanelFill.GradientStyle = GradientStyles.Solid;
            PanelTwo.PanelElement.PanelFill.BackColor = Color.WhiteSmoke;
            this.BackColor = Color.Black;


            string json = "";
            try
            {
               
                var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string path = Path.Combine(directory, "Arvand", "text.txt");
                if (System.IO.File.Exists(path) == false)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {

                    }
                }
                using (StreamReader sr = new StreamReader(path, true))
                {

                    json = sr.ReadToEnd();

                }

               
               
            }
            catch (Exception)
            {
                MessageBox.Show("adfsdf");
            }
            List<login> mymodel = new List<login>();
            if (json != "")
            {
                mymodel = JsonConvert.DeserializeObject<List<login>>(json);
            }
         

            int k = 0;
            for (int i = GlobalVariable.portlimit; i > 0; i--)
            {
               
                string name = "panel" + i;
                Telerik.WinControls.UI.RadPanel panel = new Telerik.WinControls.UI.RadPanel();
                panel.Dock = System.Windows.Forms.DockStyle.Top;
                panel.Location = new System.Drawing.Point(0, 0);
                panel.Name = name;
                panel.Size = new System.Drawing.Size(729, 50);
                ((Telerik.WinControls.Primitives.BorderPrimitive)(panel.GetChildAt(0).GetChildAt(1))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;

                System.Windows.Forms.TableLayoutPanel tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
                tableLayoutPanel1.ColumnCount = 6;
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
                tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
                tableLayoutPanel1.RowCount = 1;
                tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
                tableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                tableLayoutPanel1.Name = "tableLayoutPanel1";
                for (int j = 1; j < 4; j++)
                {

                    Telerik.WinControls.UI.RadPanel panelfortable = new Telerik.WinControls.UI.RadPanel();
                    panelfortable.BackColor = System.Drawing.SystemColors.ScrollBar;

                    System.Windows.Forms.TextBox textBox6 = new TextBox();
                    textBox6.BackColor = System.Drawing.SystemColors.ScrollBar;
                    textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    textBox6.Font = new System.Drawing.Font("B Nazanin", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
                    textBox6.Location = new System.Drawing.Point(12, 2);
                    textBox6.Name = "textBox6";
                    textBox6.Size = new System.Drawing.Size(149, 23);
                    textBox6.TextAlign =  System.Windows.Forms.HorizontalAlignment.Center;
                    
                    this.ipholder.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
                    textBox6.TabIndex = 1;
                   


                    panelfortable.Controls.Add(textBox6);
                    panelfortable.Location = new System.Drawing.Point(161, 37);
                    panelfortable.BackColor = System.Drawing.SystemColors.ScrollBar;
                    panelfortable.Name = "panelfortable";
                    panelfortable.Size = new System.Drawing.Size(180, 29);
                    panelfortable.Anchor = System.Windows.Forms.AnchorStyles.Right;
                    panelfortable.TabIndex = 16;
                    panelfortable.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
                    //panelfortable.BackColor = System.Drawing.Color.Transparent;

                    ((Telerik.WinControls.Primitives.BorderPrimitive)(panelfortable.GetChildAt(0).GetChildAt(1))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                    // panelfortable.Dock = System.Windows.Forms.DockStyle.Fill;
                    tableLayoutPanel1.Controls.Add(panelfortable, j, 0);
                    if (j == 1)
                    {
                        string tab = i.ToString() + "4";
                        textBox6.TabIndex = Convert.ToInt32(tab) ;
                        if (k < mymodel.Count)
                        {
                            textBox6.Text = mymodel[k].name;
                        }
                        else
                        {
                            textBox6.Text = "";
                        }
                       nameList.Add(textBox6);
                    }
                    else if (j == 2)
                    {
                        string tab = i.ToString() + "3";
                        textBox6.TabIndex = Convert.ToInt32(tab);

                        if (k < mymodel.Count)
                        {
                            textBox6.Text = mymodel[k].username;
                        }
                        else
                        {
                            textBox6.Text = "";
                        }
                        usernameList.Add(textBox6);
                    }
                    else if (j == 3)
                    {
                        string tab = i.ToString() + "2";
                        textBox6.TabIndex = Convert.ToInt32(tab);

                        if (k < mymodel.Count)
                        {
                            textBox6.Text = mymodel[k].password;
                        }
                        else
                        {
                            textBox6.Text = "";
                        }
                        passList.Add(textBox6);
                    }
                  

                }
                System.Windows.Forms.Label number = new Label();
                number.Anchor = System.Windows.Forms.AnchorStyles.Right;
                number.BackColor = System.Drawing.Color.Transparent;
                number.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
                number.Location = new System.Drawing.Point(630, 8);
                number.Name = "number";
                number.Size = new System.Drawing.Size(113, 28);
                number.TabIndex = 10;
                number.Text = i.ToString();
                number.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tableLayoutPanel1.Controls.Add(number, 0, 0);

                Telerik.WinControls.UI.RadPanel panelforport = new Telerik.WinControls.UI.RadPanel();
                panelforport.BackColor = System.Drawing.SystemColors.ScrollBar;
                panelforport.Location = new System.Drawing.Point(161, 37);
                panelforport.Name = "panelfortable";
                panelforport.Size = new System.Drawing.Size(180, 29);
                panelforport.Anchor = System.Windows.Forms.AnchorStyles.Right;
                panelforport.TabIndex = 16;
                ((Telerik.WinControls.Primitives.BorderPrimitive)(panelforport.GetChildAt(0).GetChildAt(1))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;

                System.Windows.Forms.Label Port = new Label();
                Port.Dock = System.Windows.Forms.DockStyle.Fill;
                Port.BackColor = System.Drawing.Color.Transparent;
                Port.Font = new System.Drawing.Font("B Nazanin", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
                Port.Location = new System.Drawing.Point(630, 8);
                Port.Name = "Port";
                Port.Size = new System.Drawing.Size(113, 28);
                Port.TabIndex = 10;
                Port.Text = (8001 + i).ToString();
                Port.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                panelforport.Controls.Add(Port);
                portList.Add(8001 + i);
                tableLayoutPanel1.Controls.Add(panelforport, 4, 0);
                tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;

                tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
                //  tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;

                panel.Controls.Add(tableLayoutPanel1);
                PanelOfDynamicData.Controls.Add(panel);
                k++;
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            List<login> list = new List<login>();
         
            int mesure = nameList.Count();

            for (int count = 0; count < nameList.Count; count++)
            {
                login row = new login();
                row.port = portList[count].ToString();
                row.password = passList[count].Text;
                row.name = nameList[count].Text; 
                row.username = usernameList[count].Text;
                list.Add(row);
            }
            string jsonmodel = JsonConvert.SerializeObject(list);
            while (true)
            {
                try
                {
                    var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string path = Path.Combine(directory, "Arvand", "text.txt");
                    System.IO.File.WriteAllText(path, string.Empty);
                    System.IO.File.WriteAllText(path, jsonmodel);
                    MessageBox.Show("تغییرات مورد نظر با موفقیت انجام شد");
                    break;
                }
                catch (Exception)
                {

                }
            }

        }

 

        private void label5_Click(object sender, EventArgs e)
        {
            string text = ipholder.Text;
            Settings1.Default.ServerIP = text;
            Settings1.Default.Save();
            MessageBox.Show(" آی سرور با موفقیت تنظیم شد");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }




    }
}
