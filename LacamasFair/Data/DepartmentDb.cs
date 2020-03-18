using LacamasFair.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Data
{
    public static class DepartmentDb
    {
        /// <summary>
        /// Gets all departments out of the database
        /// </summary>
        /// <param name="context">The Application Context</param>
        public static async Task<List<DepartmentModel>> GetAllDepartments(ApplicationDbContext context)
        {
            List<DepartmentModel> departments =
                await (from d in context.Departments
                       orderby d.DepartmentName ascending
                       select d).ToListAsync();
            return departments;
        }

        /// <summary>
        /// Gets the department by id
        /// </summary>
        /// <param name="context">The Application Context</param>
        /// <param name="id">The Department Id</param>
        public static async Task<DepartmentModel> GetDepartmentById(ApplicationDbContext context, int id)
        {
            DepartmentModel department =
                await (from d in context.Departments
                       where d.DepartmentId == id
                       select d).SingleOrDefaultAsync();
            return department;
        }

        /// <summary>
        /// Adds a new department to the database
        /// </summary>
        /// <param name="context">The Application Context</param>
        /// <param name="department">The Department Model</param>
        public static async Task<DepartmentModel> AddDepartment(ApplicationDbContext context, DepartmentModel department)
        {
            await context.AddAsync(department);
            await context.SaveChangesAsync();
            return department;
        }

        /// <summary>
        /// Updates department in the database
        /// </summary>
        /// <param name="context">The Application Context</param>
        /// <param name="department">The Department Model</param>
        public static async Task<DepartmentModel> UpdateDepartment(ApplicationDbContext context, DepartmentModel department)
        {
            await context.AddAsync(department);
            context.Entry(department).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return department;
        }

        /// <summary>
        /// Deletes Department in the database from its id
        /// </summary>
        /// <param name="context"The Application Context></param>
        /// <param name="id">The Department Id</param>
        public static async Task DeleteDepartment(ApplicationDbContext context, DepartmentModel department)
        {
            await context.AddAsync(department);
            context.Entry(department).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}
