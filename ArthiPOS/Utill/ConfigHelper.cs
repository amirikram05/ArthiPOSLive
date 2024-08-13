using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ArthiPOS.Utill
{
    public class ConfigHelper
    {
        public static Config LoadConfig(string filePath)
        {
            string json = File.ReadAllText(filePath);
            Config config = JsonConvert.DeserializeObject<Config>(json);
            return config;
        }
    }

    public class Config
    {
        public string shop_name { get; set; }
        public string business_type { get; set; }
        public string address { get; set; }
        public string phone_number { get; set; }
        public string propriters_name { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string api_key { get; set; }
        public string license_exp { get; set; }
        public string licensekey { get; set; }
        public string registrationkey_exp { get; set; }
        public string datetime { get; set; }
        public string livedb { get; set; }
        public string testdb { get; set; }
        public string backupdb { get; set; }
        public string localdb { get; set; }
        public string name1 { get; set; }
        public string phone1 { get; set; }
        public string name2 { get; set; }
        public string phone2 { get; set; }
        public string trade_mark { get; set; }
        public string shopno { get; set; }
        public string email { get; set; }
        public string company { get; set; }
    }
}
