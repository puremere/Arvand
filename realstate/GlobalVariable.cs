using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;
using System.Windows.Forms;
namespace realstate
{

    class GlobalVariable
    {
        public static bool fromarrow { get; set; }
        public static bool form2MustWait { get; set; }
        public static queryModel lastSearchModel { get; set; }
        public static List<string> RowIDList = new List<string>();
        public static List<string> RowIDListForPhone = new List<string>();
        public static List<string> RowPhoneList = new List<string>();
        public static string  searchTabghe { get; set; }
        public static List<Control> controllist = null;
        public static string mantagheIDes = "";
        public static string relatedAddress = "";
        public static string temporaryOwnList = "";
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private System.Drawing.Text.PrivateFontCollection fonts = new System.Drawing.Text.PrivateFontCollection();

        public static string expirationdate = null;
        public static int maxuser = 0;
        public static string activezonse = null;
        public static string isadmin = null;

        public static ListOfAdds.Datum selectedOwnObject = null;
        public static mylistViewManager mylistViewManager = null;
        public static string fromwhere = null;
        public static string fromwhere6 = null;
        public static String searchPicPath { get; set; }
        public static Font headerlistFONT { get; set; }
        public static Font headerlistFONTBold { get; set; }
        public static Font headerlistFONTsmall { get; set; }
        public static mehrdadPanel mehrdadpanel { get; set; }
        public static string  selectedIDofList { get; set; }


        public static string has_pic { get; set; }
        public static string areaaof { get; set; }
        public static string catof { get; set; }
        public static string owner { get; set; }
        public static string mtjfrom { get; set; }
        public static string mtjto { get; set; }
        public static string rom { get; set; }
        public static string vadfrom { get; set; }
        public static string vadto { get; set; }
        public static string ejfrom { get; set; }
        public static string ejto { get; set; }
        public static string Gtotal_from { get; set; }
        public static string Gtotal_to { get; set; }
        public static string Gprice_per_meter_from { get; set; }
        public static string Gprice_per_meter_to { get; set; }
        public static string Gowner { get; set; }
        public static string Gmain_street { get; set; }
        public static string Gsubsidiary_street { get; set; }
        public static string Galley { get; set; }
        public static string Gfloor { get; set; }
        public static string Gunit { get; set; }
        public static string Gvahed { get; set; }
     
        public static string Gappname { get; set; }
        public static string Gfacility { get; set; }
        public static string Gdirection { get; set; }
        public static string Gyear { get; set; }
        public static string Gtabdil { get; set; }
        public static string query { get; set; }


        public static string page { get; set; }
        public static string startID { get; set; }
        public static string  serverIP { get; set; }
        public static bool showimage { get; set; }
        public static String ConnectionString_IP= "Output.txt"; // Modifiable
        //public static readonly String CODE_PREFIX = "US-"; // Unmodifiable
        public static int port { get; set; }
        public static string  token { get; set; }
        public static  Classes.CatsAndAreasObject catsAndAreas { get; set; }
        public static string newCatsAndAreas { get; set; }
       
        public static string result { get; set; }
        public static int portlimit { get; set; }
    }

}
