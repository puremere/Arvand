using Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace realstate
{
    public partial class Form7 : Form
    {
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
        public static float width = 0;
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
                DataRowView rowView = this.Row.DataBoundItem as DataRowView;

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

                obj.setTitle(serverid, date, owner, melkkind, Dealkind, totalrahn, metriejare, Rtabaghe, khab, zirbana, mycheckbox, address, senn);



                // DataRowView rowView = this.Row.DataBoundItem as DataRowView;

                //this.button.Text = "ddd";

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
        public Form7()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            //add.Font = GlobalVariable.headerlistFONT;
            label2.Font = GlobalVariable.headerlistFONT;
            label5.Font = GlobalVariable.headerlistFONT;
            label26.Font = GlobalVariable.headerlistFONT;
            label27.Font = GlobalVariable.headerlistFONT;
            //   label28.Font = GlobalVariable.headerlistFONT;
            width = ClientRectangle.Width;
            radListView1.Height = flowLayoutPanel1.Height - 90;
            flowLayoutPanel1.Width = (int)width;
            tableLayoutPanel6.Width = (int)width - (Width * 3) / 100;
            radListView1.Width = (int)width - (Width * 3) / 100;
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
            ListOfAdds.Datum obj = new ListOfAdds.Datum();
            try
            {
                list2 = JsonConvert.DeserializeObject<List<ListOfAdds.Datum>>(allobject);

            }
            catch (Exception error)
            {
                obj = JsonConvert.DeserializeObject<ListOfAdds.Datum>(allobject);
            }

            if (list2 != null)
            {
                foreach (var item in list2)
                {
                    list.Add(item);
                }
                if (list.Count > 0)
                {

                    this.radListView1.VisualItemFormatting += new Telerik.WinControls.UI.ListViewVisualItemEventHandler(radListView1_VisualItemFormatting);
                    this.radListView1.CellCreating += new Telerik.WinControls.UI.ListViewCellElementCreatingEventHandler(radListView1_CellCreating);
                    this.radListView1.ColumnCreating += new ListViewColumnCreatingEventHandler(radListView1_ColumnCreating);

                    this.radListView1.CellFormatting += new Telerik.WinControls.UI.ListViewCellFormattingEventHandler(radListView1_CellFormatting);


                    radListView1.DataSource = ConvertToDataTable(list);
                    this.radListView1.ValueMember = "ID";
                    radListView1.ViewType = ListViewType.DetailsView;
                    radListView1.ShowColumnHeaders = false;
                    radListView1.Columns["title"].Width = this.radListView1.Size.Width - this.radListView1.ListViewElement.BorderWidth * 2 - 30;

                    // radListView1.Columns["title"].
                    radListView1.ItemSize = new Size(0, 33);
                }


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


        private void radListView1_DoubleClick(object sender, EventArgs e)
        {
            GlobalVariable.fromwhere = "sub";
            GlobalVariable.selectedOwnObject = new ListOfAdds.Datum();
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
            ListOfAdds.Datum obj = new ListOfAdds.Datum();
            try
            {
                list2 = JsonConvert.DeserializeObject<List<ListOfAdds.Datum>>(allobject);

            }
            catch (Exception)
            {
                obj = JsonConvert.DeserializeObject<ListOfAdds.Datum>(allobject);
            }

            if (list2 != null)
            {
                foreach (var item in list2)
                {
                    list.Add(item);
                }
            }

            string selectedindex = radListView1.SelectedItem.Value.ToString();
            ListOfAdds.Datum selected = list.Where(x => x.ID == selectedindex).FirstOrDefault();

            GlobalVariable.selectedOwnObject = selected;
            Form6 form6 = new Form6();
            form6.Show();



        }

        //private void pictureBox2_Click(object sender, EventArgs e)
        //{
        //   // string search = searchTextBox.Text;
        //    string filepath = Path.Combine(Application.StartupPath, "Resources", "objects.txt");
        //    string allobject = System.IO.File.ReadAllText(filepath);
        //    List<ListOfAdds.Datum> list = new List<ListOfAdds.Datum>();
        //    List<ListOfAdds.Datum> list2 = new List<ListOfAdds.Datum>();
        //    ListOfAdds.Datum obj = new ListOfAdds.Datum();
        //    try
        //    {
        //        list2 = JsonConvert.DeserializeObject<List<ListOfAdds.Datum>>(allobject);

        //    }
        //    catch (Exception)
        //    {
        //        obj = JsonConvert.DeserializeObject<ListOfAdds.Datum>(allobject);
        //    }

        //    if (list2 != null)
        //    {
        //        foreach (var item in list2)
        //        {
        //            list.Add(item);
        //        }
        //    }

        //    List<ListOfAdds.Datum> selected = list.Where(x => x.title.Contains(search)).Select(x => x).ToList();
        //    this.radListView1.VisualItemFormatting += new Telerik.WinControls.UI.ListViewVisualItemEventHandler(radListView1_VisualItemFormatting);
        //    this.radListView1.CellCreating += new Telerik.WinControls.UI.ListViewCellElementCreatingEventHandler(radListView1_CellCreating);
        //    this.radListView1.ColumnCreating += new ListViewColumnCreatingEventHandler(radListView1_ColumnCreating);

        //    this.radListView1.CellFormatting += new Telerik.WinControls.UI.ListViewCellFormattingEventHandler(radListView1_CellFormatting);

        //    radListView1.DataSource = ConvertToDataTable(selected);
        //    this.radListView1.ValueMember = "server_id";
        //    radListView1.ViewType = ListViewType.DetailsView;
        //    radListView1.ShowColumnHeaders = false;
        //    radListView1.Columns["title"].Width = this.radListView1.Size.Width - this.radListView1.ListViewElement.BorderWidth * 2 - 30;

        //    // radListView1.Columns["title"].
        //    radListView1.ItemSize = new Size(0, 100);

        //}



        public void getdata()
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
            ListOfAdds.Datum obj = new ListOfAdds.Datum();
            try
            {
                list2 = JsonConvert.DeserializeObject<List<ListOfAdds.Datum>>(allobject);

            }
            catch (Exception)
            {
                obj = JsonConvert.DeserializeObject<ListOfAdds.Datum>(allobject);
            }
            foreach (var item in list2)
            {
                list.Add(item);
            }
            this.radListView1.VisualItemFormatting += new Telerik.WinControls.UI.ListViewVisualItemEventHandler(radListView1_VisualItemFormatting);
            this.radListView1.CellCreating += new Telerik.WinControls.UI.ListViewCellElementCreatingEventHandler(radListView1_CellCreating);
            this.radListView1.ColumnCreating += new ListViewColumnCreatingEventHandler(radListView1_ColumnCreating);

            this.radListView1.CellFormatting += new Telerik.WinControls.UI.ListViewCellFormattingEventHandler(radListView1_CellFormatting);

            radListView1.DataSource = ConvertToDataTable(list);
            this.radListView1.ValueMember = "ID";
            radListView1.ViewType = ListViewType.DetailsView;
            radListView1.ShowColumnHeaders = false;
            radListView1.Columns["title"].Width = this.radListView1.Size.Width - this.radListView1.ListViewElement.BorderWidth * 2 - 30;

            // radListView1.Columns["title"].
            radListView1.ItemSize = new Size(0, 100);


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            getdata();
        }



        private void add_Click(object sender, EventArgs e)
        {
            Form6 form = new Form6();
            GlobalVariable.selectedOwnObject = null;
            form.Show();
            this.Dispose();
        }



        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radPanel26_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radListView1_SelectedItemChanged(object sender, EventArgs e)
        {

        }
    }
}
