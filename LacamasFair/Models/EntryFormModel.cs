using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Models
{
    public class EntryFormModel
    {
        /// <summary>
        /// Unique id for the Department Number
        /// </summary>
        [Key]
        [Required]
        public int SubDeptId { get; set; }

        /// <summary>
        /// Unique id for the entry form
        /// </summary>
        [Required]
        public int EntryFormId { get; set; }

        /// <summary>
        /// Name of the file where it's stored
        /// </summary>
        [Required]
        public string FormFile { get; set; }
    }
}
