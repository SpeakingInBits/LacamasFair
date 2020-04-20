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

        [Required, StringLength(50), Display(Name = "Facility Name")]
        public string FacilityName { get; set; }

        [Required, DataType(DataType.Currency)]
        public double Price { get; set; }
    }
}
