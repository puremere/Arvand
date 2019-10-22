using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace realstate.ListOfAdds
{
    class Class
    {
    }


    public class Dialog
    {
        public int status { get; set; }
        public bool showDialog { get; set; }
        public string dialogTitle { get; set; }
        public string dialogMessage { get; set; }
        public string positiveBtn { get; set; }
        public string positiveBtnUrl { get; set; }
        public string negativeBtn { get; set; }
    }

    public class Datum
    {
        public string ID { get; set; }
        public string title { get; set; }
        public string date_updated { get; set; }
        public List<string> phones { get; set; }
        public string malek { get; set; }
        public string mantaghe_name { get; set; }
        public string mantaghe_id { get; set; }
        public string address { get; set; }
        public string isForoosh { get; set; }
        public string isEjare { get; set; }
        public string isRahn { get; set; }
        public string isMosharekat { get; set; }
        public string isMoaveze { get; set; }
        public string hasEstakhr { get; set; }
        public string hasSauna { get; set; }
        public string hasJakoozi { get; set; }
        public string hasGym { get; set; }
        public string hasShooting { get; set; }
        public string hasHall { get; set; }
        public string hasRoofGarden { get; set; }
        public string isMoble { get; set; }
        public string hasLobbyMan { get; set; }
        public string sell2khareji { get; set; }
        public string maghaze { get; set; }
        public string suit { get; set; }
        public string samt { get; set; }
        public string apartment { get; set; }
        public string villa { get; set; }
        public string mostaghellat { get; set; }
        public string kolangi { get; set; }
        public string office { get; set; }
        public string seraydar { get; set; }
        public string kaf_type { get; set; }
        public string garmayesh_sarmayesh { get; set; }
        public string total_floor { get; set; }
        public string total_vahed { get; set; }
        public string vahed_per_floor { get; set; }
        public string gozar { get; set; }
        public string sanad { get; set; }
        public string takhlie { get; set; }
        public string tabaghe1 { get; set; }
        public string zirbana1 { get; set; }
        public string bed1 { get; set; }
        public string balkon1 { get; set; }
        public string wc1 { get; set; }
        public string ashpazkhane1 { get; set; }
        public string parking1 { get; set; }
        public string anbari1 { get; set; }
        public string asansor1 { get; set; }
        public string tabaghe2 { get; set; }
        public string zirbana2 { get; set; }
        public string bed2 { get; set; }
        public string balkon2 { get; set; }
        public string wc2 { get; set; }
        public string ashpazkhane2 { get; set; }
        public string parking2 { get; set; }
        public string anbari2 { get; set; }
        public string asansor2 { get; set; }
        public string tabaghe3 { get; set; }
        public string zirbana3 { get; set; }
        public string bed3 { get; set; }
        public string balkon3 { get; set; }
        public string wc3 { get; set; }
        public string ashpazkhane3 { get; set; }
        public string parking3 { get; set; }
        public string anbari3 { get; set; }
        public string asansor3 { get; set; }
        public string tabaghe_1_total_price { get; set; }
        public string tabaghe_2_total_price { get; set; }
        public string tabaghe_3_total_price { get; set; }
        public string tabaghe_4_total_price { get; set; }
        public string tabaghe_1_rahn { get; set; }
        public string tabaghe_2_rahn { get; set; }
        public string tabaghe_3_rahn { get; set; }
        public string tabaghe_4_rahn { get; set; }
        public string tabaghe_1_ejare { get; set; }
        public string tabaghe_2_ejare { get; set; }
        public string tabaghe_3_ejare { get; set; }
        public string tabaghe_4_ejare { get; set; }
        public string tabaghe_1_metri { get; set; }
        public string tabaghe_2_metri { get; set; }
        public string tabaghe_3_metri { get; set; }
        public string tabaghe_4_metri { get; set; }
        public string masahat_zamin { get; set; }
        public string senn { get; set; }
        public string ashpazkhane { get; set; }
        public string tarakom { get; set; }
        public string toole_bar { get; set; }
        public string ertefa { get; set; }
        public string eslahi { get; set; }
        public string zirzamin { get; set; }
        public string desc { get; set; }
    }

    public class Result
    {
        public List<Datum> data { get; set; }
        public int today_files { get; set; }
    }

    public class RootObject
    {
        public Dialog dialog { get; set; }
        public Result result { get; set; }
    }
}
