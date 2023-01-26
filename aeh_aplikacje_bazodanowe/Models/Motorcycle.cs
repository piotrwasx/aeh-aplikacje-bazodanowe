using System;
namespace aeh_aplikacje_bazodanowe.Models
{
    public class Motorcycle
    {
        public int id { get; set; }
        public string motorcycle_brand { get; set; }
        public string motorcycle_model { get; set; }
        public int motorcycle_year { get; set; }
        public int motorcycle_mileage_km { get; set; }
        public string motorcycle_motor { get; set; }
        public string motorcycle_body_type { get; set; }
        public int motorcycle_rent_price_pln { get; set; }
    }
}
