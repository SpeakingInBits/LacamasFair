using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Models
{
    public class BoardMember
    {
        /// <summary>
        /// Unique Id assigned to a board member
        /// </summary>
        [Key]
        [Required]
        public int BoardMemberId { get; set; }

        /// <summary>
        /// Position/Title of the fair officer (Ex. Vice President)
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Fair Officer or Club Officer? (Select one)")]
        public string FairOrClubOfficer { get; set; }
    }
}
