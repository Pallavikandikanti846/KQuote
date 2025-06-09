using System.ComponentModel.DataAnnotations.Schema;

namespace passion_project_http5226.Models
{
    public class QuoteMood
    {
        public int quote_id { get; set; }
        public int mood_id { get; set; }

        [ForeignKey("quote_id")]
        [System.Text.Json.Serialization.JsonIgnore]
        public required virtual Quote Quote { get; set; }

        [ForeignKey("mood_id")]
        [System.Text.Json.Serialization.JsonIgnore]
        public required virtual Mood Mood { get; set; }
    }

}
