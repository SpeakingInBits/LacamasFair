using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Models
{


    public class InnerDptModel
    {
        /// <summary>
        /// Unique id for the Department Number
        /// </summary>
        [Key]
        [Required]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Required Key of Dept Name
        /// </summary>
        [Key]
        [Required(ErrorMessage = "A Department name is required")]
        public string DepartmentName { get; set; }

        [Key] //this one needs some work, unsure how to link, maybe nested property class and href to file
        [Required]
        [DataType(DataType.ImageUrl)]
        public BadImageFormatException DeptarmentPict { get; set; }

        /// <summary>
        /// Department description (This is the animal department, etc etc)
        /// </summary>
        [StringLength(500)]
        public String DepartmentDescript { get; set; }

        /// <summary>
        /// Text of Fair Entry rules that should be able to be updated by admin
        /// </summary>
        [StringLength(500)]
        public String FairEntryRules { get; set; }

        /// <summary>
        /// Foreign Key that is pulled from the "Class"Class
        /// </summary>
        //THIS NEEDS TO BE A FOREIGN KEY, References
        public int ClassId { get; set; }


        /// <summary>
        /// Foreign Key that pulls from EntryForm class
        /// </summary>
        [DataType(DataType.ImageUrl)]
        public BadImageFormatException EntryFormId { get; set; }

    }
}
