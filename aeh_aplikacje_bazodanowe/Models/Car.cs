using System;
namespace aeh_aplikacje_bazodanowe.Models
{
    public class Car
    {
        public int id { get; set; }
        public string car_brand { get; set; }
        public string car_model { get; set; }
        public int car_year { get; set; }
        public int car_mileage_km { get; set; }
        public string car_transmisson { get; set; }
        public string car_motor { get; set; }
        public string car_body_type { get; set; }
        public int car_rent_price_pln { get; set; }
    }
}
