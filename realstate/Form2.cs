using Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;


//class OvalPictureBox : PictureBox
//{
//    public OvalPictureBox()
//    {
//        this.BackColor = Color.DarkGray;
//    }
//    protected override void OnResize(EventArgs e)
//    {
//        base.OnResize(e);
//        using (var gp = new GraphicsPath())
//        {
//            gp.AddEllipse(new Rectangle(0, 0, this.Width - 1, this.Height - 1));
//            this.Region = new Region(gp);
//        }
//    }
//}
namespace realstate
{
    public partial class Form2 : Form
    {
        
        ListOfAdds.Datum obj = new ListOfAdds.Datum();
        // List<string> checkBoxList = new List<string>() { "sanad", "samt", "senn", "ashpazkhane3", "ashpazkhane2", "ashpazkhane1", "seraydar", "kaf_type", "garmayesh_sarmayesh", "apartment"
        //, "villa", "mostaghellat", "kolangi", "office", "mantaghe_name", "mantaghe_id" };
        CatsAndAreasObject CATS = null;
        CatsAndAreasObject cats2 = new CatsAndAreasObject();
        private Dictionary<string, string> fileData = new Dictionary<string, string>();
        
        public string getChangedValue(string value)
        {
            string final = "";
            if (value == "-1")
            {
                final = "توافقی";

            }
            else if (value == "-2")
            {
                final = "رایگان";
            }
            else
            {
                final = string.Format(CultureInfo.InvariantCulture, "{0:#,##0}", Convert.ToInt64(value));
            }
            return final;
        }
        public void InitControlAuto()
        {
            if (GlobalVariable.isadmin == "1")
            {
                CATS = JsonConvert.DeserializeObject<CatsAndAreasObject>(GlobalVariable.newCatsAndAreas);
            }
            try
            {

                foreach (var item in GlobalVariable.controllist)
                {

                    string name = item.Name;
                    string valueofProperty = "";
                    try
                    {
                        //valueofProperty= fileData[name];

                        Type type = obj.GetType();
                        PropertyInfo[] properties = type.GetProperties();
                        //valueofProperty = (from p in properties
                        //                   where p.Name == name
                        //                   select p.GetValue(obj, null).ToString()).First();

                        foreach (var foreachItem in properties)
                        {
                            if (foreachItem.Name == name)
                            {
                                valueofProperty = foreachItem.GetValue(obj, null).ToString();
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }


                    if (name == "phone1" || name == "phone2" || name == "phone2")
                    {

                    }
                    else if (item is CheckBox)
                    {
                        CheckBox cb = (CheckBox)item;
                        cb.Checked = valueofProperty == "0" ? false : true;
                    }
                    //else if (item is ComboBox)
                    //{
                    //    if (valueofProperty != "0")
                    //    {
                    //        switch (name)
                    //        {

                    //            case "sanad":
                    //                item.Text = (from q in CATS.result.sanad
                    //                             where q.ID == valueofProperty
                    //                             select q.title).First();
                    //                break;
                    //            case "samt":
                    //                item.Text = (from q in CATS.result.samt
                    //                             where q.ID == valueofProperty
                    //                             select q.title).First();
                    //                break;
                    //            case "senn":
                    //                item.Text = (from q in CATS.result.senn
                    //                             where q.ID == valueofProperty
                    //                             select q.title).First();
                    //                break;
                    //            case "ashpazkhane3":
                    //                if (valueofProperty.Length > 0)
                    //                {
                    //                    string final = "";
                    //                    string srt = valueofProperty.Remove(valueofProperty.Length - 1, 1);
                    //                    srt = srt.Remove(0, 1);

                    //                    if (srt.Contains(','))
                    //                    {
                    //                        List<string> lstsg3 = srt.Split(',').ToList();
                    //                        foreach (var itemm in lstsg3)
                    //                        {
                    //                            final = final + (from q in CATS.result.ashpazkhane
                    //                                             where q.ID == itemm
                    //                                             select q.title).First() + ",";
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        final = (from q in CATS.result.ashpazkhane
                    //                                 where q.ID == srt
                    //                                 select q.title).First();
                    //                    }

                    //                    item.Text = final;

                    //                }

                    //                break;
                    //            case "ashpazkhane2":
                    //                if (valueofProperty.Length > 0)
                    //                {
                    //                    string final = "";
                    //                    string srt = valueofProperty.Remove(valueofProperty.Length - 1, 1);
                    //                    srt = srt.Remove(0, 1);

                    //                    if (srt.Contains(','))
                    //                    {
                    //                        List<string> lstsg3 = srt.Split(',').ToList();
                    //                        foreach (var itemm in lstsg3)
                    //                        {
                    //                            final = final + (from q in CATS.result.ashpazkhane
                    //                                             where q.ID == itemm
                    //                                             select q.title).First() + ",";
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        final = (from q in CATS.result.ashpazkhane
                    //                                 where q.ID == srt
                    //                                 select q.title).First();
                    //                    }

                    //                    item.Text = final;
                    //                }
                    //                break;
                    //            case "ashpazkhane1":
                    //                if (valueofProperty.Length > 0)
                    //                {
                    //                    string final = "";
                    //                    string srt = valueofProperty.Remove(valueofProperty.Length - 1, 1);
                    //                    srt = srt.Remove(0, 1);

                    //                    if (srt.Contains(','))
                    //                    {
                    //                        List<string> lstsg3 = srt.Split(',').ToList();
                    //                        foreach (var itemm in lstsg3)
                    //                        {
                    //                            final = final + (from q in CATS.result.ashpazkhane
                    //                                             where q.ID == itemm
                    //                                             select q.title).First() + ",";
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        final = (from q in CATS.result.ashpazkhane
                    //                                 where q.ID == srt
                    //                                 select q.title).First();
                    //                    }

                    //                    item.Text = final;
                    //                }
                    //                break;
                    //            case "seraydar":
                    //                item.Text = (from q in CATS.result.seraydar
                    //                             where q.ID == valueofProperty
                    //                             select q.title).First();
                    //                break;
                    //            case "kaf_type":
                    //                item.Text = (from q in CATS.result.kaf_type
                    //                             where q.ID == valueofProperty
                    //                             select q.title).First();
                    //                break;
                    //            case "garmayesh_sarmayesh":
                    //                if (valueofProperty.Length > 0)
                    //                {
                    //                    string final = "";
                    //                    string srt = valueofProperty.Remove(valueofProperty.Length - 1, 1);
                    //                    srt = srt.Remove(0, 1);

                    //                    if (srt.Contains(','))
                    //                    {
                    //                        List<string> lstsg3 = srt.Split(',').ToList();
                    //                        foreach (var itemm in lstsg3)
                    //                        {
                    //                            final = final + (from q in CATS.result.garmayesh_sarmayesh
                    //                                             where q.ID == itemm
                    //                                             select q.title).First() + ",";
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        final = (from q in CATS.result.garmayesh_sarmayesh
                    //                                 where q.ID == srt
                    //                                 select q.title).First();
                    //                    }

                    //                    item.Text = final;
                    //                }

                    //                break;
                    //            case "apartment":
                    //                item.Text = (from q in CATS.result.apartment
                    //                             where q.ID == valueofProperty
                    //                             select q.title).First();
                    //                break;
                    //            case "villa":
                    //                item.Text = (from q in CATS.result.villa
                    //                             where q.ID == valueofProperty
                    //                             select q.title).First();
                    //                break;
                    //            case "mostaghellat":
                    //                item.Text = (from q in CATS.result.mostaghellat
                    //                             where q.ID == valueofProperty
                    //                             select q.title).First();
                    //                break;
                    //            case "kolangi":
                    //                item.Text = (from q in CATS.result.kolangi
                    //                             where q.ID == valueofProperty
                    //                             select q.title).First();
                    //                break;
                    //            case "office":
                    //                item.Text = (from q in CATS.result.office
                    //                             where q.ID == valueofProperty
                    //                             select q.title).First();
                    //                break;
                    //            case "mantaghe_name":
                    //                item.Text = (from q in CATS.result.mantaghe
                    //                             where q.ID == valueofProperty
                    //                             select q.title).First();
                    //                break;
                    //            case "mantaghe_id":
                    //                item.Text = (from q in CATS.result.mantaghe_id
                    //                             where q.ID == valueofProperty
                    //                             select q.title).First();
                    //                break;


                    //        }
                    //    }
                    //    else
                    //    {
                    //        item.Text = "-";
                    //    }



                    //}
                    else
                    {
                        item.Text = valueofProperty;
                    }
                }

            }
            catch (Exception)
            {


            }

        }
        public void InitControl()
        {

            try
            {
                CATS = JsonConvert.DeserializeObject<CatsAndAreasObject>(GlobalVariable.newCatsAndAreas);
            }
            catch
            {

                CATS = GlobalVariable.catsAndAreas;
            }

            title.Text = obj.title;
            
            if (obj.phones != null)
            {
                try
                {
                    phone1.Text = obj.phones[0];
                }
                catch (Exception)
                {

                    phone1.Text = "";
                }
                try
                {
                    phone2.Text = obj.phones[1];
                }
                catch (Exception)
                {

                    phone2.Text = "";
                }
                try
                {
                    phone3.Text = obj.phones[2];
                }
                catch (Exception)
                {

                    phone3.Text = "";
                }
               
            }
          

            owner.Text = obj.malek;
            address.Text = obj.address;
            date_updated.Text = obj.date_updated;
            ID.Text = obj.ID;
            total_vahed.Text = obj.total_vahed;
            total_floor.Text = obj.total_floor;
            vahed_per_floor.Text = obj.vahed_per_floor;
            zirbana1.Text = obj.zirbana1;
            zirbana2.Text = obj.zirbana2;
            zirbana3.Text = obj.zirbana3;
            khab1.Text = obj.bed1;
            khab2.Text = obj.bed2;
            khab3.Text = obj.bed3;
            tabaghe1.Text = obj.tabaghe1;
            tabaghe2.Text = obj.tabaghe2;
            tabaghe3.Text = obj.tabaghe3;
            tabaghe_1_rahn.Text = getChangedValue(obj.tabaghe_1_rahn);
            tabaghe_2_rahn.Text =getChangedValue( obj.tabaghe_2_rahn);// == "-2" ? "رایگان" : string.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToInt64(obj.tabaghe_2_rahn));
            tabaghe_3_rahn.Text = getChangedValue(obj.tabaghe_3_rahn);// == "-2" ? "رایگان" : string.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToInt64(obj.tabaghe_3_rahn));
            tabaghe_1_ejare.Text = getChangedValue(obj.tabaghe_1_ejare);// == "-2" ? "رایگان" : string.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToInt64(obj.tabaghe_1_ejare));
            tabaghe_2_ejare.Text = getChangedValue(obj.tabaghe_2_ejare);// == "-2" ? "رایگان" : string.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToInt64(obj.tabaghe_2_ejare));
            tabaghe_3_ejare.Text = getChangedValue(obj.tabaghe_3_ejare);// == "-2" ? "رایگان" : string.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToInt64(obj.tabaghe_3_ejare));
            tabaghe_1_total_price.Text = getChangedValue(obj.tabaghe_1_total_price );//== "-2" ? "رایگان" : string.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToInt64(obj.tabaghe_1_total_price));
            tabaghe_2_total_price.Text = getChangedValue(obj.tabaghe_2_total_price);// == "-2" ? "رایگان" : string.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToInt64(obj.tabaghe_2_total_price));
            tabaghe_3_total_price.Text =getChangedValue( obj.tabaghe_3_total_price);// == "-2" ? "رایگان" : string.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToInt64(obj.tabaghe_3_total_price));
            tabaghe_1_metri.Text =getChangedValue(obj.tabaghe_1_metri);// string.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToInt64();
            tabaghe_2_metri.Text =getChangedValue(obj.tabaghe_2_metri);// string.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToInt64();
            tabaghe_3_metri.Text = getChangedValue(obj.tabaghe_3_metri);// string.Format(CultureInfo.InvariantCulture, "{0:0,0}", Convert.ToInt64();
            tarakom.Text = obj.tarakom;
            toole_bar.Text = obj.toole_bar;
            masahat_zamin.Text = obj.masahat_zamin;
            ertefa.Text = obj.ertefa;
            eslahi.Text = obj.eslahi;
            zirzamin.Text = obj.zirzamin;
            desc.Text = obj.desc;
            isForoosh.Checked =  obj.isForoosh == "1" ? true : false;
            isEjare.Checked = obj.isEjare == "1" ? true : false;
            isMosharekat.Checked = obj.isMosharekat == "1" ? true : false;
            isRahn.Checked = obj.isRahn == "1" ? true : false;
            isMoaveze.Checked = obj.isMoaveze == "1" ? true : false;
            sell2khareji.Checked = obj.sell2khareji == "1" ? true : false;
            hasEstakhr.Checked = obj.hasEstakhr == "1" ? true : false;
            hasJakoozi.Checked = obj.hasJakoozi == "1" ? true : false;
            hasSauna.Checked = obj.hasSauna == "1" ? true : false;
            takhlie.Checked = obj.takhlie == "1" ? true : false;
            balkon1.Checked = obj.balkon1 == "1" ? true : false;
            balkon2.Checked = obj.balkon2 == "1" ? true : false;
            balkon3.Checked = obj.balkon3 == "1" ? true : false;
            parking1.Checked = obj.parking1 == "1" ? true : false;
            parking2.Checked = obj.parking2 == "1" ? true : false;
            parking3.Checked = obj.parking3 == "1" ? true : false;
            anbari1.Checked = obj.anbari1 == "1" ? true : false;
            anbari2.Checked = obj.anbari2 == "1" ? true : false;
            anbari3.Checked = obj.anbari3 == "1" ? true : false;
            asansor1.Checked = obj.asansor1 == "1" ? true : false;
            asansor2.Checked = obj.asansor2 == "1" ? true : false;
            asansor3.Checked = obj.asansor3 == "1" ? true : false;
            hasGym.Checked = obj.hasGym == "1" ? true : false;
            hasShooting.Checked = obj.hasShooting == "1" ? true : false;
            hasHall.Checked = obj.hasHall == "1" ? true : false;
            hasRoofGarden.Checked = obj.hasRoofGarden == "1" ? true : false;
            isMoble.Checked = obj.isMoble == "1" ? true : false;
            hasLobbyMan.Checked = obj.hasLobbyMan == "1" ? true : false;
            maghaze.Checked = obj.maghaze == "1" ? true : false;
            

            mantaghe_id.Text = obj.mantaghe_id == "0" ? "-" : (from q in CATS.result.mantaghe_id
                                                               where q.ID == obj.mantaghe_id
                                                               select q.title).First();

            mantaghe_name.Text = obj.mantaghe_name == "0" ? "-" : (from q in CATS.result.mantaghe
                                                                   where q.ID == obj.mantaghe_name
                                                                   select q.title).First();


            apartment.Text = obj.apartment == "0" ? "-" : (from q in CATS.result.apartment
                                                           where q.ID == obj.apartment
                                                           select q.title).First();

            office.Text = obj.office == "0" ? "-" : (from q in CATS.result.office
                                                     where q.ID == obj.office
                                                     select q.title).First();

            villa.Text = obj.villa == "0" ? "-" : (from q in CATS.result.villa
                                                   where q.ID == obj.villa
                                                   select q.title).First();

            mostaghellat.Text = obj.mostaghellat == "0" ? "-" : (from q in CATS.result.mostaghellat
                                                                 where q.ID == obj.mostaghellat
                                                                 select q.title).First();


            kolangi.Text = obj.kolangi == "0" ? "-" : (from q in CATS.result.kolangi
                                                       where q.ID == obj.kolangi
                                                       select q.title).First();

            seraydar.Text = obj.seraydar == "0" ? "-" : (from q in CATS.result.seraydar
                                                         where q.ID == obj.seraydar
                                                         select q.title).First();

            kaf_type.Text = obj.kaf_type == "0" ? "-" : (from q in CATS.result.kaf_type
                                                         where q.ID == obj.kaf_type
                                                         select q.title).First();

            if (obj.garmayesh_sarmayesh.Length > 1)
            {
                string final = "";
                string srt = obj.garmayesh_sarmayesh.Remove(obj.garmayesh_sarmayesh.Length - 1, 1);
                srt = srt.Remove(0, 1);

                if (srt.Contains(','))
                {
                    List<string> lstsg3 = srt.Split(',').ToList();
                    foreach (var itemm in lstsg3)
                    {
                        final = final + (from q in CATS.result.garmayesh_sarmayesh
                                         where q.ID == itemm
                                         select q.title).First() + ",";
                    }
                }
                else
                {
                    final = (from q in CATS.result.garmayesh_sarmayesh
                             where q.ID == srt
                             select q.title).First();
                }

                //final = final.Remove(final.Length - 1, 1);
                garmayesh_sarmayesh.Text = final;

            }
            else if (obj.garmayesh_sarmayesh.Length == 1)
            {

                if (obj.garmayesh_sarmayesh == "0")
                {
                    garmayesh_sarmayesh.Text = "-";
                }
                else
                {
                    garmayesh_sarmayesh.Text = (from q in CATS.result.garmayesh_sarmayesh
                                                where q.ID == obj.garmayesh_sarmayesh
                                                select q.title).First();
                }

            }
            else
            {
                garmayesh_sarmayesh.Text = "-";
            }

            if (obj.ashpazkhane1.Length > 1)
            {
                string final = "";
                string srt = obj.ashpazkhane1.Remove(obj.ashpazkhane1.Length - 1, 1);
                srt = srt.Remove(0, 1);

                if (srt.Contains(','))
                {
                    List<string> lstsg3 = srt.Split(',').ToList();
                    foreach (var itemm in lstsg3)
                    {
                        final = final + (from q in CATS.result.ashpazkhane
                                         where q.ID == itemm
                                         select q.title).First() + ",";
                    }
                }
                else
                {
                    final = (from q in CATS.result.ashpazkhane
                             where q.ID == srt
                             select q.title).First();
                }
               // final = final.Remove(final.Length - 1, 1);
                ashpazkhane1.Text = final;

            }
            else if (obj.ashpazkhane1.Length == 1)
            {

                if (obj.ashpazkhane1 == "0")
                {
                    ashpazkhane1.Text = "-";
                }
                else
                {
                    ashpazkhane1.Text = (from q in CATS.result.ashpazkhane
                                         where q.ID == obj.ashpazkhane1
                                         select q.title).First();
                }

            }
            else
            {
                ashpazkhane1.Text = "-";
            }

            if (obj.ashpazkhane2.Length > 0)
            {
                string final = "";
                string srt = obj.ashpazkhane2.Remove(obj.ashpazkhane2.Length - 1, 1);
                srt = srt.Remove(0, 1);

                if (srt.Contains(','))
                {
                    List<string> lstsg3 = srt.Split(',').ToList();
                    foreach (var itemm in lstsg3)
                    {
                        final = final + (from q in CATS.result.ashpazkhane
                                         where q.ID == itemm
                                         select q.title).First() + ",";
                    }
                }
                else
                {
                    final = (from q in CATS.result.ashpazkhane
                             where q.ID == srt
                             select q.title).First();
                }
                //final = final.Remove(final.Length - 1, 1);
                ashpazkhane2.Text = final;

            }

            else if (obj.ashpazkhane2.Length == 1)
            {

                if (obj.ashpazkhane2 == "0")
                {
                    ashpazkhane2.Text = "-";
                }
                else
                {
                    ashpazkhane2.Text = (from q in CATS.result.ashpazkhane
                                         where q.ID == obj.ashpazkhane2
                                         select q.title).First();
                }

            }
            else
            {
                ashpazkhane2.Text = "-";
            }
            if (obj.ashpazkhane3.Length > 0)
            {
                string final = "";
                string srt = obj.ashpazkhane3.Remove(obj.ashpazkhane3.Length - 1, 1);
                srt = srt.Remove(0, 1);

                if (srt.Contains(','))
                {
                    List<string> lstsg3 = srt.Split(',').ToList();
                    foreach (var itemm in lstsg3)
                    {
                        final = final + (from q in CATS.result.ashpazkhane
                                         where q.ID == itemm
                                         select q.title).First() + ",";
                    }
                }
                else
                {
                    final = (from q in CATS.result.ashpazkhane
                             where q.ID == srt
                             select q.title).First();
                }
                //final = final.Remove(final.Length - 1, 1);
                ashpazkhane3.Text = final;

            }
            else if (obj.ashpazkhane3.Length == 1)
            {

                if (obj.ashpazkhane3 == "0")
                {
                    ashpazkhane3.Text = "-";
                }
                else
                {
                    ashpazkhane3.Text = (from q in CATS.result.ashpazkhane
                                         where q.ID == obj.ashpazkhane3
                                         select q.title).First();
                }

            }
            else
            {
                ashpazkhane3.Text = "-";
            }
               senn.Text = obj.senn == "0"?  "-" : (from q in CATS.result.senn
                                                 where q.ID == obj.senn
                                                 select q.title).First();


                samt.Text = obj.samt == "0" ? "-" : (from q in CATS.result.samt
                                                 where q.ID == obj.samt
                                                 select q.title).First();

            sanad.Text = obj.sanad == "0" ? "-" : (from q in CATS.result.sanad
                                                   where q.ID == obj.sanad
                                                   select q.title).First();







        }

        private List<Control> GetAllControls(Control container, List<Control> list)
        {
            foreach (Control c in container.Controls)
            {
                if (c.Controls.Count > 0)
                    list = GetAllControls(c, list);
                else
                {
                    if (c is TextBox || c is Label)
                    {
                        list.Add(c);
                    }
                }
                   
                   

            }

            return list;
        }
        private List<Control> GetAllControls(Control container)
        {
            return GetAllControls(container, new List<Control>());
        }



       
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
           
            //capture left arrow key
            if (keyData == Keys.Left)
            {
                string id = ID.Text;
         
                int Indexof = GlobalVariable.RowIDList.IndexOf(id);
                int mustbe = Indexof - 1;


                if (mustbe <= GlobalVariable.RowIDList.Count() - 1 && mustbe >= 0)
                {
                    GlobalVariable.fromarrow = true;
                   
                    int mustid = int.Parse(GlobalVariable.RowIDList[mustbe]);
                    getDataFromServer(mustid);
                  

                }
                return true;
            }
            //capture right arrow key
            if (keyData == Keys.Right)
            {
                string id = ID.Text;
                int Indexof = GlobalVariable.RowIDList.IndexOf(id);
                int mustbe = Indexof + 1;
                if (mustbe <= GlobalVariable.RowIDList.Count() - 1 && mustbe >= 0)
                {
                    GlobalVariable.fromarrow = true;
                
                    int mustid = int.Parse(GlobalVariable.RowIDList[mustbe]);
                    getDataFromServer(mustid);

                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        protected override void OnLoad(EventArgs e)
        {

            this.KeyPreview = true;

            CatsAndAreasObject CATS = null;
            if (GlobalVariable.isadmin == "1")
            {
                CATS = JsonConvert.DeserializeObject<CatsAndAreasObject>(GlobalVariable.newCatsAndAreas);
            }
            else
            {
                CATS = GlobalVariable.catsAndAreas;
            }


            List<Control> allControls = GetAllControls(this);
            allControls.ForEach(k => k.Font = GlobalVariable.headerlistFONT);


            if (GlobalVariable.fromwhere != "main")
            {
                next.Visible = false;
                back.Visible = false;
            }
            la7.Font = GlobalVariable.headerlistFONTsmall;
            la8.Font = GlobalVariable.headerlistFONTsmall;
            la9.Font = GlobalVariable.headerlistFONTsmall;
            la10.Font = GlobalVariable.headerlistFONTsmall;
            la11.Font = GlobalVariable.headerlistFONTsmall;
            la12.Font = GlobalVariable.headerlistFONTsmall;
            la13.Font = GlobalVariable.headerlistFONTsmall;
            la14.Font = GlobalVariable.headerlistFONTsmall;
            la15.Font = GlobalVariable.headerlistFONTsmall;
            la09.Font = GlobalVariable.headerlistFONTsmall;
            la17.Font = GlobalVariable.headerlistFONTsmall;
            la18.Font = GlobalVariable.headerlistFONTsmall;
            la19.Font = GlobalVariable.headerlistFONTsmall;
            //la20.Font = GlobalVariable.headerlistFONTsmall;
            //la21.Font = GlobalVariable.headerlistFONTsmall;
            //la22.Font = GlobalVariable.headerlistFONTsmall;
            //label13.Font = GlobalVariable.headerlistFONTsmall;
            label12.Font = GlobalVariable.headerlistFONTsmall;
            la57.Font = GlobalVariable.headerlistFONTsmall;

            this.MinimizeBox = true;
            this.MaximizeBox = true;
            whishon.Visible = false;
            ListOfAdds.RootObject log = JsonConvert.DeserializeObject<ListOfAdds.RootObject>(GlobalVariable.result);
            obj = log.result.data.First();

            InitControl();

            wishoff.Name = "0-" + obj.ID;
            whishon.Name = "1-" + obj.ID;
            saveToPrivate.Name = "2-" + obj.ID;
            next.Name = "3-" + obj.ID;
            back.Name = "4-" + obj.ID;

            string ideas = Settings1.Default.ides;
            if (ideas.Contains("," + obj.ID))
            {
                wishoff.Visible = false;
                whishon.Visible = true;
            }
            else
            {
                whishon.Visible = false;
                wishoff.Visible = true;

            }


            base.OnLoad(e);

        }

        void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
               
            }
            else if (e.KeyCode == Keys.Left)
            {
                
            }
        }

        private void MyCode()
        {
           
            if (GlobalVariable.showimage)
            {
                try
                {
                    int port = 8001;
                    if (GlobalVariable.port != 0)
                    {
                        port = GlobalVariable.port;
                    }


                    Console.WriteLine("Connecting.....");

                    int j = 0;
                    ListOfAdds.RootObject log = JsonConvert.DeserializeObject<ListOfAdds.RootObject>(GlobalVariable.result);



                }

                catch (Exception f)
                {
                    Console.Write("Error..... " + f.StackTrace);
                }

            }
        }
        // your "special" method to handle "load is complete" event

        string form2id = "";
        List<System.Drawing.Image> imglist = new List<System.Drawing.Image>();

        //string metraj = "";
        //string cat = "";
        //string room = "";
        //string title = "";
        //string desc = "";

        public Form2()
        {
            GlobalVariable.showimage = true;
            //form2id = id;
            InitializeComponent();
            // radRotator1.BeginRotate += new BeginRotateEventHandler(radRotator1_BeginRotate);
           // this.WindowState = FormWindowState.Maximized;
        }

        private void Form2_Load(object sender, EventArgs e)
        {



        }


        void radRotator1_BeginRotate(object sender, BeginRotateEventArgs e)
        {
            // radLabelElement1.Text = String.Format("Rotating from item {0} to {1}", e.From, e.To);
        }



        private void Form2_MouseEnter(object sender, EventArgs e)
        {

        }

        private void wishoff_Click(object sender, EventArgs e)
        {
            string allobject = "";
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



            var pic = (PictureBox)sender;
            string name = pic.Name;
            string id = name.Substring(2, name.Length - 2);
            ListOfAdds.RootObject log = JsonConvert.DeserializeObject<ListOfAdds.RootObject>(GlobalVariable.result);
            ListOfAdds.Datum newobj = log.result.data.Where(p => p.ID == id).Single();
            list.Add(newobj);
            string jsonmodel = JsonConvert.SerializeObject(list);
            try
            {
                System.IO.File.WriteAllText(path, string.Empty);
                System.IO.File.WriteAllText(path, jsonmodel);
            }
            catch (Exception)
            {

            }
            wishoff.Visible = false;
            whishon.Visible = true;
            string ideas = Settings1.Default.ides;
            Settings1.Default.ides = ideas + "," + id;
            Settings1.Default.Save();
        }

        private void whishon_Click(object sender, EventArgs e)
        {
            string allobject = "";
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



            var pic = (PictureBox)sender;
            string name = pic.Name;
            string id = name.Substring(2, name.Length - 2);
            ListOfAdds.RootObject log = JsonConvert.DeserializeObject<ListOfAdds.RootObject>(GlobalVariable.result);
            list.RemoveAll(p => p.ID == id);
            string jsonmodel = JsonConvert.SerializeObject(list);
            try
            {
                System.IO.File.WriteAllText(path, string.Empty);
                System.IO.File.WriteAllText(path, jsonmodel);


            }
            catch (Exception)
            {

            }
            string idesss = Settings1.Default.ides.Remove(0, 1);
            List<string> TagIds = idesss.Split(',').ToList();
            int index = 0;
            foreach (var item in TagIds)
            {
                if (item.ToString() == id)
                {
                    index = TagIds.IndexOf(item);
                }
            }
            TagIds.RemoveAt(index);
            string ides = "";
            foreach (var item in TagIds)
            {
                ides = "," + item;
            }

            Settings1.Default.ides = ides;
            Settings1.Default.Save();
            wishoff.Visible = true;
            whishon.Visible = false;
        }

        private void saveToPrivate_Click(object sender, EventArgs e)
        {
            var directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = Path.Combine(directory, "Arvand", "objects.txt");
            string allobject = "";
            try
            {
               
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



            var pic = (PictureBox)sender;
            string name = pic.Name;
            string id = name.Substring(2, name.Length - 2);
            ListOfAdds.RootObject log = JsonConvert.DeserializeObject<ListOfAdds.RootObject>(GlobalVariable.result);
            ListOfAdds.Datum newobj = log.result.data.First();



            //try
            //{
            //    CATS = JsonConvert.DeserializeObject<CatsAndAreasObject>(GlobalVariable.newCatsAndAreas);
            //}
            //catch
            //{

            //    CATS = GlobalVariable.catsAndAreas;
            //}

            //newobj.mantaghe_id = newobj.mantaghe_id == "0" ? "-" : (from q in CATS.result.mantaghe_id
            //                                                   where q.ID == obj.mantaghe_id
            //                                                   select q.title).First();

            //newobj.mantaghe_name = newobj.mantaghe_name == "0" ? "-" : (from q in CATS.result.mantaghe
            //                                                       where q.ID == obj.mantaghe_name
            //                                                       select q.title).First();


            //newobj.apartment = newobj.apartment == "0" ? "-" : (from q in CATS.result.apartment
            //                                               where q.ID == obj.apartment
            //                                               select q.title).First();

            //newobj.office = newobj.office == "0" ? "-" : (from q in CATS.result.office
            //                                         where q.ID == obj.office
            //                                         select q.title).First();

            //newobj.villa = newobj.villa == "0" ? "-" : (from q in CATS.result.villa
            //                                       where q.ID == obj.villa
            //                                       select q.title).First();

            //newobj.mostaghellat = newobj.mostaghellat == "0" ? "-" : (from q in CATS.result.mostaghellat
            //                                                     where q.ID == obj.mostaghellat
            //                                                     select q.title).First();


            //newobj.kolangi = newobj.kolangi == "0" ? "-" : (from q in CATS.result.kolangi
            //                                           where q.ID == obj.kolangi
            //                                           select q.title).First();

            //newobj.seraydar = newobj.seraydar == "0" ? "-" : (from q in CATS.result.seraydar
            //                                             where q.ID == obj.seraydar
            //                                             select q.title).First();

            //newobj.kaf_type = newobj.kaf_type == "0" ? "-" : (from q in CATS.result.kaf_type
            //                                             where q.ID == obj.kaf_type
            //                                             select q.title).First();

            //if (newobj.garmayesh_sarmayesh.Length > 1)
            //{
            //    string final = "";
            //    string srt = newobj.garmayesh_sarmayesh.Remove(newobj.garmayesh_sarmayesh.Length - 1, 1);
            //    srt = srt.Remove(0, 1);

            //    if (srt.Contains(','))
            //    {
            //        List<string> lstsg3 = srt.Split(',').ToList();
            //        foreach (var itemm in lstsg3)
            //        {
            //            final = final + (from q in CATS.result.garmayesh_sarmayesh
            //                             where q.ID == itemm
            //                             select q.title).First() + ",";
            //        }
            //    }
            //    else
            //    {
            //        final = (from q in CATS.result.garmayesh_sarmayesh
            //                 where q.ID == srt
            //                 select q.title).First();
            //    }

            //    //final = final.Remove(final.Length - 1, 1);
            //    newobj.garmayesh_sarmayesh = final;

            //}
            //else if (newobj.garmayesh_sarmayesh.Length == 1)
            //{

            //    if (newobj.garmayesh_sarmayesh == "0")
            //    {
            //        newobj.garmayesh_sarmayesh = "-";
            //    }
            //    else
            //    {
            //        newobj.garmayesh_sarmayesh = (from q in CATS.result.garmayesh_sarmayesh
            //                                    where q.ID == obj.garmayesh_sarmayesh
            //                                    select q.title).First();
            //    }

            //}
            //else
            //{
            //    newobj.garmayesh_sarmayesh = "-";
            //}

            //if (newobj.ashpazkhane1.Length > 1)
            //{
            //    string final = "";
            //    string srt = newobj.ashpazkhane1.Remove(newobj.ashpazkhane1.Length - 1, 1);
            //    srt = srt.Remove(0, 1);

            //    if (srt.Contains(','))
            //    {
            //        List<string> lstsg3 = srt.Split(',').ToList();
            //        foreach (var itemm in lstsg3)
            //        {
            //            final = final + (from q in CATS.result.ashpazkhane
            //                             where q.ID == itemm
            //                             select q.title).First() + ",";
            //        }
            //    }
            //    else
            //    {
            //        final = (from q in CATS.result.ashpazkhane
            //                 where q.ID == srt
            //                 select q.title).First();
            //    }
            //    // final = final.Remove(final.Length - 1, 1);
            //    newobj.ashpazkhane1 = final;

            //}
            //else if (newobj.ashpazkhane1.Length == 1)
            //{

            //    if (newobj.ashpazkhane1 == "0")
            //    {
            //        newobj.ashpazkhane1 = "-";
            //    }
            //    else
            //    {
            //        newobj.ashpazkhane1 = (from q in CATS.result.ashpazkhane
            //                             where q.ID == obj.ashpazkhane1
            //                             select q.title).First();
            //    }

            //}
            //else
            //{
            //    newobj.ashpazkhane1 = "-";
            //}

            //if (newobj.ashpazkhane2.Length > 0)
            //{
            //    string final = "";
            //    string srt = newobj.ashpazkhane2.Remove(newobj.ashpazkhane2.Length - 1, 1);
            //    srt = srt.Remove(0, 1);

            //    if (srt.Contains(','))
            //    {
            //        List<string> lstsg3 = srt.Split(',').ToList();
            //        foreach (var itemm in lstsg3)
            //        {
            //            final = final + (from q in CATS.result.ashpazkhane
            //                             where q.ID == itemm
            //                             select q.title).First() + ",";
            //        }
            //    }
            //    else
            //    {
            //        final = (from q in CATS.result.ashpazkhane
            //                 where q.ID == srt
            //                 select q.title).First();
            //    }
            //    //final = final.Remove(final.Length - 1, 1);
            //    newobj.ashpazkhane2 = final;

            //}

            //else if (newobj.ashpazkhane2.Length == 1)
            //{

            //    if (newobj.ashpazkhane2 == "0")
            //    {
            //        newobj.ashpazkhane2 = "-";
            //    }
            //    else
            //    {
            //        newobj.ashpazkhane2 = (from q in CATS.result.ashpazkhane
            //                             where q.ID == obj.ashpazkhane2
            //                             select q.title).First();
            //    }

            //}
            //else
            //{
            //    newobj.ashpazkhane2 = "-";
            //}
            //if (newobj.ashpazkhane3.Length > 0)
            //{
            //    string final = "";
            //    string srt = newobj.ashpazkhane3.Remove(newobj.ashpazkhane3.Length - 1, 1);
            //    srt = srt.Remove(0, 1);

            //    if (srt.Contains(','))
            //    {
            //        List<string> lstsg3 = srt.Split(',').ToList();
            //        foreach (var itemm in lstsg3)
            //        {
            //            final = final + (from q in CATS.result.ashpazkhane
            //                             where q.ID == itemm
            //                             select q.title).First() + ",";
            //        }
            //    }
            //    else
            //    {
            //        final = (from q in CATS.result.ashpazkhane
            //                 where q.ID == srt
            //                 select q.title).First();
            //    }
            //    //final = final.Remove(final.Length - 1, 1);
            //    newobj.ashpazkhane3 = final;

            //}
            //else if (newobj.ashpazkhane3.Length == 1)
            //{

            //    if (newobj.ashpazkhane3 == "0")
            //    {
            //        newobj.ashpazkhane3 = "-";
            //    }
            //    else
            //    {
            //        newobj.ashpazkhane3 = (from q in CATS.result.ashpazkhane
            //                             where q.ID == obj.ashpazkhane3
            //                             select q.title).First();
            //    }

            //}
            //else
            //{
            //    newobj.ashpazkhane3 = "-";
            //}
            //newobj.senn = newobj.senn; // == "0" ? "-" : (from q in CATS.result.senn
            //                                     //where q.ID == obj.senn
            //                                    // select q.title).First();


            //newobj.samt = newobj.samt == "0" ? "-" : (from q in CATS.result.samt
            //                                     where q.ID == obj.samt
            //                                     select q.title).First();

            //newobj.sanad = newobj.sanad == "0" ? "-" : (from q in CATS.result.sanad
            //                                       where q.ID == obj.sanad
            //                                       select q.title).First();




            






            list.Add(newobj);
            string jsonmodel = JsonConvert.SerializeObject(list);
            try
            {

                System.IO.File.WriteAllText(path, string.Empty);
                System.IO.File.WriteAllText(path, jsonmodel);



                MessageBox.Show("فایل مورد نظر با موفقیت به فایل های اختصاصی شما اضافه شد");

            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);

            }
        }

        private void next_Click(object sender, EventArgs e)
        {
            
            string id = ID.Text;
            int Indexof = GlobalVariable.RowIDList.IndexOf(id);
            int mustbe = Indexof + 1;
            if (mustbe <= GlobalVariable.RowIDList.Count() - 1 && mustbe >= 0)
            {
                GlobalVariable.fromarrow = true;
              
                int mustid = int.Parse(GlobalVariable.RowIDList[mustbe]);
                getDataFromServer(mustid);

            }

        }

        private void back_Click(object sender, EventArgs e)
        {
            string id = ID.Text;

            int Indexof = GlobalVariable.RowIDList.IndexOf(id);
            int mustbe = Indexof - 1;


            if (mustbe <= GlobalVariable.RowIDList.Count() - 1 && mustbe >= 0)
            {
                GlobalVariable.fromarrow = true;
              
                int mustid = int.Parse(GlobalVariable.RowIDList[mustbe]);
                getDataFromServer(mustid);


            }
        }

        private void bel56_Click(object sender, EventArgs e)
        {
            string finalstring = address.Text + "+++";
            GlobalVariable.relatedAddress =  finalstring;
            GlobalVariable.temporaryOwnList = "";
            
          
            getDataFromServer(null);
           
        }

        private void getDataFromServer(int? id)
        {
            if (refresh.Visible == false)
            {
                refresh.Visible = true;
                BackgroundWorker getDataBackGroundWorker = new BackgroundWorker();
                getDataBackGroundWorker.WorkerSupportsCancellation = true;
                getDataBackGroundWorker.DoWork += new DoWorkEventHandler(getDataBackGroundWorker_do);
                getDataBackGroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(getDataBackGroundWorker_done);

                try
                {
                    queryModel model = new queryModel();
                    if (id != null)
                    {
                        model.ID = id.ToString();
                    }
                    model.relatedAddress = GlobalVariable.relatedAddress;

                    string str = JsonConvert.SerializeObject(model);

                    getDataBackGroundWorker.RunWorkerAsync(argument: str);
                    refresh.Visible = true;
                }
                catch (Exception)
                {



                }
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

                    if (GlobalVariable.fromarrow == true) {
                        ListOfAdds.RootObject log = JsonConvert.DeserializeObject<ListOfAdds.RootObject>(GlobalVariable.result);
                        obj = log.result.data[0];
                        InitControl();
                        GlobalVariable.fromarrow = false;
                        refresh.Visible = false;
                    }
                    else
                    {
                        foreach (Form item in Application.OpenForms)
                        {
                            if (item.Name == "Form1")
                            {
                                item.Close();
                                break;
                            }
                        }
                        GlobalVariable.fromwhere = "main";

                        Form1 form1 = new Form1();
                        Control clt0 = form1.Controls.Find("flowLayoutPanel1", true).First();
                        clt0.BackgroundImage = null;
                        Control clt = form1.Controls.Find("radListView1", true).First();
                        clt.Visible = true;

                        Control clt2 = form1.Controls.Find("searchpanel", true).First();
                        clt2.Visible = true;

                        Control clt3 = form1.Controls.Find("dissconnect", true).First();
                        clt3.Visible = false;

                        Control clt4 = form1.Controls.Find("connect", true).First();
                        clt4.Visible = true;


                        form1.Show();
                        this.Close();
                        refresh.Visible = false;
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

                const int blockSize = 30000000;
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
            catch (Exception)
            {

                MessageBox.Show("خطا در ارتباط با سرور");

                tcpclnt.Close();
                return;
                e.Result = "error";
            }
        }

        private void next_Click_1(object sender, EventArgs e)
        {

        }

        private void radPanel44_Paint(object sender, PaintEventArgs e)
        {

        }

        private void la15_Click(object sender, EventArgs e)
        {

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            
        }

       



        //private void Form2_Paint(object sender, PaintEventArgs e)
        //{
        //    System.Drawing.Graphics graphics = e.Graphics;
        //    System.Drawing.Rectangle gradient_rectangle = new System.Drawing.Rectangle(0, 0, this.Width, this.Height);
        //    System.Drawing.Brush b = new LinearGradientBrush(this.ClientRectangle,  Color.WhiteSmoke, Color.LightGray,  90F);
        //    graphics.FillRectangle(b, gradient_rectangle);
        //}
    }
}
