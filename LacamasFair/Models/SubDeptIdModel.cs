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
        /// Foreign Key, that links to DepartmentName primary table
        /// </summary>
        [Required(ErrorMessage = "A Department name is required")]
        public string ParentDept { get; set; }


        /// <summary>
        /// Department description (This is the animal department, etc etc)
        /// </summary>
        [StringLength(60)]
        public string SubDeptName { get; set; }

        /// <summary>
        /// Text of Fair Entry rules that should be able to be updated by admin
        /// </summary>
        [StringLength(1000)]
        public string FairEntryRules { get; set; }

        /// <summary>
        /// Classes should store information about current available department classes
        /// </summary>
        [StringLength(750)]
        public int DeptClasses { get; set; }
    }
}
