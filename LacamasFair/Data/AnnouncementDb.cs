using LacamasFair.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Data
{
    public class AnnouncementDb
    {
        /// <summary>
        /// Gets all announcements from the database
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<List<Announcement>> GetAllAnnouncements(ApplicationDbContext context) 
        {
            List<Announcement> announcements = await (from a in context.Announcements
                                                      orderby a.AnnouncementId ascending
                                                      select a).ToListAsync();
            return announcements;
        }

        /// <summary>
        /// Gets a single announcement by id
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Announcement> GetAnnouncementById(ApplicationDbContext context, int id) 
        {
            Announcement announcement = await (from a in context.Announcements
                                               where a.AnnouncementId == id
                                               select a).SingleOrDefaultAsync();
            return announcement;
        }

        /// <summary>
        /// Adds an announcement to the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="announcement"></param>
        /// <returns></returns>
        public static async Task<Announcement> AddAnnouncement(ApplicationDbContext context, Announcement announcement) 
        {
            await context.AddAsync(announcement);
            await context.SaveChangesAsync();
            return announcement;
        }

        /// <summary>
        /// Updates an existing announcement in the database 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="announcement"></param>
        /// <returns></returns>
        public static async Task<Announcement> UpdateAnnouncement(ApplicationDbContext context, Announcement announcement) 
        {
            await context.AddAsync(announcement);
            context.Entry(announcement).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return announcement;
        }

        /// <summary>
        /// Deletes an existing announcement from the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="announcement"></param>
        /// <returns></returns>
        public static async Task DeleteAnnouncement(ApplicationDbContext context, Announcement announcement) 
        {
            await context.AddAsync(announcement);
            context.Entry(announcement).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}
