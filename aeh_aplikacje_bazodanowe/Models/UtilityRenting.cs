using System;
namespace aeh_aplikacje_bazodanowe.Models
{
    public class UtilityRenting
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public int utility_id { get; set; }
        public DateTime rent_start { get; set; }
        public DateTime rent_end { get; set; }
        public int rent_insurance { get; set; }
    }
}