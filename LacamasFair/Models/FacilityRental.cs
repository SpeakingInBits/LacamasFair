using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Models
{
    public class FacilityRental
    {
        /// <summary>
        /// Unique id for a single facility
        /// </summary>
        [Key]
        [Required]
        public int FacilityId { get; set; }

        [Required]
        public string FacilityName { get; set; }

        [Required]
        public string Price { get; set; }
    }
}
