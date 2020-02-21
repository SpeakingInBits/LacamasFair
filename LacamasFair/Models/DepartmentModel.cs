using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Models
{
    public class DepartmentModel
    {

        /// <summary>
        /// This is the primary key that connects to ParentDept in the SubDept class.
        /// </summary>
        [Key]
        [Required]
        public int DepartmentId { get; set; }

        /// <summary>
        /// This is a required name of the Department, that will have a list of SubDepartment Names
        /// </summary>
        [Required]
        [StringLength(75)]
        public string DepartmenName { get; set; }


    }
}
