using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Models
{
    public class SubDeptClassModel
    {
        /// <summary>
        /// Unique id for the class
        /// </summary>
        [Key]
        [Required]
        public int ClassId { get; set; }

        /// <summary>
        /// Foreign key that links to SubDeptIdModel's SubDeptId property 
        /// </summary>
        [Required]
        public int SubDeptId { get; set; }

        /// <summary>
        /// The name of the sub department's class 
        /// (For example, the baking sub department has pies, cookies, etc classes)
        /// </summary>
        [StringLength(750)]
        [Display(Name = "Title")]
        public string ClassName { get; set; }

        /// <summary>
        /// Entry rules for that class
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "Details")]
        public string ClassRules { get; set; }
    }
}
