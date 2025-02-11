using System.ComponentModel.DataAnnotations;

namespace Mission6_Beardall.Models
{
    public class Movie
    {

        [Key] // This marks Id as the primary key
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        public string Rating { get; set; } // "G", "PG", "PG-13", "R"

        public bool? Edited { get; set; } // Nullable for optional field

        public string? LentTo { get; set; } // Nullable for optional field

        [MaxLength(25)]
        public string? Notes { get; set; } // Nullable with a character limit

    }
}
