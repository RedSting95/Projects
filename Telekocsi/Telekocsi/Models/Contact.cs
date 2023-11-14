using System.ComponentModel.DataAnnotations;

namespace Telekocsi.Models
{
    public class Contact
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        public string CarId { get; set; }
    }
}
