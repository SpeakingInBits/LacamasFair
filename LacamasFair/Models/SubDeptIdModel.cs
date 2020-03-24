using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Models
{


    public class SubDeptIdModel
    {
        /// <summary>
        /// Unique id for the Department Number
        /// </summary>
        [Key]
        [Required]
        public int SubDeptId { get; set; }

        /// <summary>
        /// Foreign Key, that links to DepartmentModel's DepartmentId property (The parent department)
        /// </summary>
        [Required]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Department description (This is the animal department, etc etc)
        /// </summary>
        [Required(ErrorMessage = "A Sub Department Name is required")]
        [StringLength(60)]
        [Display(Name = "Sub Department Name")]
        public string SubDeptName { get; set; }
    }
}
