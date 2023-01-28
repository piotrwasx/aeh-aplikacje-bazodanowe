using System;
namespace aeh_aplikacje_bazodanowe.Models
{
    public class Client
    {
        public int id { get; set; }
        public string client_name { get; set; }
        public string client_surname { get; set; }
        public string client_address { get; set; }
        public string client_city { get; set; }
        public string client_phone_nr { get; set; }
        public string client_email { get; set; }
        public DateTime client_driving_license_since { get; set; }
    }
}