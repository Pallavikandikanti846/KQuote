﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace passion_project_http5226.Models
{
    public class Quote
    {
        [Key]
        public int quote_id { get; set; }

        //Each quote belongs to one drama
        [ForeignKey("drama_id")]
        public required virtual Drama Drama { get; set; }
        public int drama_id { get; set; }
        public string? content { get; set; }
        public string? actor { get; set; }
        public int episode { get; set; }

        //A quote can have many moods
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<QuoteMood>? QuoteMoods { get; set; }
    }
    public class QuoteDto
    {
        public int quote_id { get; set; }
        public string? content { get; set; }
        public string? actor { get; set; }
        public int episode { get; set; }
        public int drama_id { get; set; }
        public string? title { get; set; }
    }
}
