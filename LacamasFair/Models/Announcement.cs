using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Models
{
    public class Announcement
    {
        /// <summary>
        /// Unique id for an announcement
        /// </summary>
        [Key]
        [Required]
        public int AnnouncementId { get; set; }

        /// <summary>
        /// Announcement title
        /// </summary>
        [Required, StringLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// Details of the announcement
        /// </summary>
        [Required, StringLength(1000)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Date { get; set; }
    }
}
