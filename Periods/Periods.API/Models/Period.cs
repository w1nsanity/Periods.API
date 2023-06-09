using System.ComponentModel.DataAnnotations;

namespace Periods.API.Models
{


    public class Period
    {
        [Key]
        public int ID { get; set; }
        public string start_time { get; set; }
        public string title { get; set; }
        public string building { get; set; }
        public string day_of_week { get; set; }
        public string week_number { get; set; }
    }
}
