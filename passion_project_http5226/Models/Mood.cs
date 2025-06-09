using System.ComponentModel.DataAnnotations;
namespace passion_project_http5226.Models
{
    public class Mood
    {
        [Key]
        public int mood_id { get; set; }
        public string? type { get; set; }

        //A Mood can linked to many Quotes
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<QuoteMood>? QuoteMoods { get; set; }
    }
    public class MoodDto
    {
        public int mood_id { get; set; }
        public string? type { get; set; }

    }

}
