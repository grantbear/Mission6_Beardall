using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission6_Beardall.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }  // Primary key (CategoryId)

        public string CategoryName { get; set; }  // Category name
    }

    public class Movie
    {
        [Key]
        public int MovieID { get; set; }

        public int CategoryId { get; set; }  // Foreign key column

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        public string Title { get; set; } // Required field

        [Required]
        [Range(1888, int.MaxValue, ErrorMessage = "Year must be 1888 or later.")]
        public int Year { get; set; } // Required and validated

        public string? Director { get; set; }

        public string? Rating { get; set; }

        [Required]
        public bool Edited { get; set; } // Required field

        public string? LentTo { get; set; }

        [Required]
        public int CopiedToPlex { get; set; } // Required field

        [MaxLength(25)]
        public string? Notes { get; set; }

        
    }


}
