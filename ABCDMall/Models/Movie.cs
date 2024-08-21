using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ABCDMall.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(500)]
        public string? ImageUrl { get; set; }


        [Required]
        [Range(0, int.MaxValue)] // Ensure TotalSeats is a non-negative integer
        public int TotalSeats { get; set; }

        [Required]
        [Range(0, int.MaxValue)] // Ensure AvailableSeats is a non-negative integer
        public int AvailableSeats { get; set; }

        [Required]
        public DateTime ShowTime { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedAt { get; set; }
    }
}
