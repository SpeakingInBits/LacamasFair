using LacamasFair.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Data.Migrations
{
    public static class SubDepartmentDb
    {
        /// <summary>
        /// Gets all sub departments out of the database
        /// </summary>
        /// <param name="context">The Application Context</param>
        public static async Task<List<SubDeptIdModel>> GetAllSubDepartments(ApplicationDbContext context)
        {
            List<SubDeptIdModel> model =
                await (from s in context.SubDepartments
                       orderby s.SubDeptName ascending
                       select s).ToListAsync();
            return model;
        }

        /// <summary>
        /// Gets the sub department out of the database with id
        /// </summary>
        /// <param name="context">The Application Context</param>
        /// <param name="id">the SubDepartment Id</param>
        public static async Task<SubDeptIdModel> GetSubDepartmentById(ApplicationDbContext context, int id)
        {
            SubDeptIdModel sub =
                await (from s in context.SubDepartments
                       where s.SubDeptId == id
                       select s).SingleOrDefaultAsync();
            return sub;
        }

        /// <summary>
        /// Adds a sub department to the database
        /// </summary>
        /// <param name="context">The Application Context</param>
        /// <param name="model">The SubDepartment Model</param>
        public static async Task<SubDeptIdModel> AddSubDepartment(ApplicationDbContext context, SubDeptIdModel model)
        {
            await context.AddAsync(model);
            await context.SaveChangesAsync();
            return model;
        }

        /// <summary>
        /// Updates sub department from database
        /// </summary>
        /// <param name="context">The Application Context</param>
        /// <param name="model">The SubDepartment Model</param>
        public static async Task<SubDeptIdModel> UpdateSubDepartment(ApplicationDbContext context, SubDeptIdModel model)
        {
            context.Update(model);
            await context.SaveChangesAsync();
            return model;
        }

        /// <summary>
        /// Deletes sub department from the database
        /// </summary>
        /// <param name="context">The Application Context</param>
        /// <param name="subId">The SubDepartment Id</param>
        public static async Task DeleteSubDepartmentById(ApplicationDbContext context, int subId)
        {
            SubDeptIdModel model = new SubDeptIdModel()
            { 
                SubDeptId = id
            };
            context.Entry(model).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}
