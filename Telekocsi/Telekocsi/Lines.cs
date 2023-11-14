using System;
using System.Collections.Generic;
using Telekocsi.Models;
using System.ComponentModel.DataAnnotations;

namespace Telekocsi
{
    public partial class Lines
    {
        public int Id { get; set; }
        [Required]
        public string Start { get; set; } = null!;
        [Required]
        public string Arrival { get; set; } = null!;
        [Required]
        public double Distance { get; set; }
        [Required]
        public int Capacity { get; set; }

        
    }
}
