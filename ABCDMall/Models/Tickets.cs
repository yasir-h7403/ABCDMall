using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ABCDMall.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int? MovieId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? UserName { get; set; }

        [Required]
        [MaxLength(500)]
        public string? CardDetails { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime BookingTimeCreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime BookingTimeUpdatedAt { get; set; }
        [ForeignKey("MovieId")]
        public Movie? Movie { get; set; }  // Navigation property for the relationship with the Movie entity
    }
}
