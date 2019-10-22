using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
using realstate;

namespace Classes
{
    class Class
    {
        public static void Click(object sender, EventArgs e)
        {
            RadCheckBoxElement checkbox = sender as RadCheckBoxElement;
            string srt = realstate.GlobalVariable.temporaryOwnList;
            string idtodo = checkbox.Name.ToString() + ",";
            if (checkbox.Checked == true)
            {

                srt = srt.Replace(idtodo,"");
                realstate.GlobalVariable.temporaryOwnList = srt;
              
            }
            else
            {
                srt = srt + idtodo;
                realstate.GlobalVariable.temporaryOwnList = srt;
            }
        }
      
    }

    public class Apartment
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class GarmayeshSarmayesh
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class KafType
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Kolangi
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Mantaghe
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class MantagheId
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Mostaghellat
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Office
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Samt
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Senn
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Seraydar
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Villa
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Ashpazkhane
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Sanad
    {
        public string ID { get; set; }
        public string title { get; set; }
    }

    public class Result
    {
        public List<Apartment> apartment { get; set; }
        public List<GarmayeshSarmayesh> garmayesh_sarmayesh { get; set; }
        public List<KafType> kaf_type { get; set; }
        public List<Kolangi> kolangi { get; set; }
        public List<Mantaghe> mantaghe { get; set; }
        public List<MantagheId> mantaghe_id { get; set; }
        public List<Mostaghellat> mostaghellat { get; set; }
        public List<Office> office { get; set; }
        public List<Samt> samt { get; set; }
        public List<Senn> senn { get; set; }
        public List<Seraydar> seraydar { get; set; }
        public List<Villa> villa { get; set; }
        public List<Ashpazkhane> ashpazkhane { get; set; }
        public List<Sanad> sanad { get; set; }
    }

   
    public class CatsAndAreasObject
    {
        public Result result { get; set; }
    }
    public static class PersianDate
    {
        /// <summary>
        /// یک استرینگ تاریخ شمسی را به معادل میلادی تبدیل میکند
        /// </summary>
        /// <param name="persianDate">تاریخ شمسی</param>
        /// <returns>تاریخ میلادی</returns>
        public static DateTime ToGeorgianDateTime(this string persianDate)
        {
            int year = Convert.ToInt32(persianDate.Substring(0, 4));
            int month = Convert.ToInt32(persianDate.Substring(5, 2));
            int day = Convert.ToInt32(persianDate.Substring(8, 2));
            DateTime georgianDateTime = new DateTime(year, month, day, new System.Globalization.PersianCalendar());
            return georgianDateTime;
        }

        /// <summary>
        /// یک تاریخ میلادی را به معادل فارسی آن تبدیل میکند
        /// </summary>
        /// <param name="georgianDate">تاریخ میلادی</param>
        /// <returns>تاریخ شمسی</returns>
        public static string ToPersianDateString(this DateTime georgianDate)
        {
            System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();

            string year = persianCalendar.GetYear(georgianDate).ToString();
            string month = persianCalendar.GetMonth(georgianDate).ToString().PadLeft(2, '0');
            string day = persianCalendar.GetDayOfMonth(georgianDate).ToString().PadLeft(2, '0');
            string persianDateString = string.Format("{0}/{1}/{2}", year, month, day);
            return persianDateString;
        }

        /// <summary>
        /// یک تعداد روز را از یک تاریخ شمسی کم میکند یا به آن آضافه میکند
        /// </summary>
        /// <param name="georgianDate">تاریخ شمسی اول</param>
        /// <param name="days">تعداد روزی که میخواهیم اضافه یا کم کنیم</param>
        /// <returns>تاریخ شمسی به اضافه تعداد روز</returns>
        public static string AddDaysToShamsiDate(this string persianDate, int days)
        {
            DateTime dt = persianDate.ToGeorgianDateTime();
            dt = dt.AddDays(days);
            return dt.ToPersianDateString();
        }
    }




}


