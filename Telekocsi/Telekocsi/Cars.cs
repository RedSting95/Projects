using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Telekocsi
{
    public partial class Cars
    {
        public int Id { get; set; }
        [Required]
        public int LineId { get; set; }
        [Required]
        public int OccupiedSpots { get; set; }
        [Required]
        public DateTime Start { get; set; }
    }
}
