using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ABCDMall.Models
{
    public class Users
    {
        [Key] 
        public int Id { get; set; }   
        [Required]
        [MaxLength(20)]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20)]
        public string? Password { get; set; }
    }

}
