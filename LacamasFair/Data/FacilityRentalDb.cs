﻿using LacamasFair.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Data
{
    public class FacilityRentalDb
    {
        /// <summary>
        /// Gets all of the facility rentals out of the database
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<List<FacilityRental>> GetAllFacilityRentals(ApplicationDbContext context) 
        {
            List<FacilityRental> facilityRentals = await (from f in context.FacilityRentals
                                                          orderby f.FacilityName ascending
                                                          select f).ToListAsync();
            return facilityRentals;
        }

        /// <summary>
        /// Gets a single facility rental by id
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<FacilityRental> GetFacilityRentalById(ApplicationDbContext context, int id) 
        {
            FacilityRental facility = await (from f in context.FacilityRentals
                                             where f.FacilityId == id
                                             select f).SingleOrDefaultAsync();
            return facility;
        }

        /// <summary>
        /// Adds a new facility rental to the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="facility"></param>
        /// <returns></returns>
        public static async Task<FacilityRental> AddFacilityRental(ApplicationDbContext context, FacilityRental facility) 
        {
            await context.AddAsync(facility);
            await context.SaveChangesAsync();
            return facility;
        }

        /// <summary>
        /// Updates an existing facility rental from the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="facility"></param>
        /// <returns></returns>
        public static async Task<FacilityRental> UpdateFacilityRental(ApplicationDbContext context, FacilityRental facility) 
        {
            await context.AddAsync(facility);
            context.Entry(facility).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return facility;
        }

        /// <summary>
        /// Deletes a selected facility rental from the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="facility"></param>
        /// <returns></returns>
        public static async Task DeleteFacilityRental(ApplicationDbContext context, FacilityRental facility) 
        {
            await context.AddAsync(facility);
            context.Entry(facility).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}
