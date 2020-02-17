using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Models
{
    public class ClassInfoModel
    {
        /// <summary>
        /// ClassId is the primary key, must be present if there is a class
        /// </summary>
        [Key]
        [Required]
        public int ClassId { get; set; }

        /// <summary>
        /// The name is required
        /// </summary>
        [Required]
        [StringLength(60)]
        public string ClassName { get; set; }

        /// <summary>
        /// Check if class if available, so that the below information may not be necessary under the condition
        /// </summary>
        [Required]
        public bool classAvailable { get; set; }

        /// <summary>
        /// EntryRules, may not be present if the class is not exisit
        /// </summary>
        [StringLength(1000)]
        public string ClassEntryRules { get; set; }

        /// <summary>
        /// Class Description, may not be necessary if there is no class.
        /// </summary>
        [StringLength(500)]
        public string ClassDescrip { get; set; }
    }
}
