using System.Text;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;

namespace Telekocsi.Models
{
    public class Admin
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Pwd { get; set; }

        

    }
}
