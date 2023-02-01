using System;
namespace aeh_aplikacje_bazodanowe.Models
{
    public class Utility
    {
        public int id { get; set; }
        public string utility_brand { get; set; }
        public string utility_model { get; set; }
        public int utility_year { get; set; }
        public int utility_mileage_km { get; set; }
        public string utility_transmission { get; set; }
        public string utility_motor { get; set; }
        public string utility_type { get; set; }
        public int utility_rent_price_pln { get; set; }
        public Boolean utility_availability { get; set; }
    }
}