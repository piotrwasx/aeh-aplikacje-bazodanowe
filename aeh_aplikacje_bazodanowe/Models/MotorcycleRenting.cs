using System;
namespace aeh_aplikacje_bazodanowe.Models
{
    public class MotorcycleRenting
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public int motorcycle_id { get; set; }
        public DateTime rent_start { get; set; }
        public DateTime rent_end { get; set; }
        public Boolean rent_insurance { get; set; }
    }
}