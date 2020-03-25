using LacamasFair.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Data
{
    public class SubDeptClassDb
    {
        /// <summary>
        /// Gets all sub department classes out of the database
        /// </summary>
        /// <param name="context"></param>
        public static async Task<List<SubDeptClassModel>> GetAllSubDeptClasses(ApplicationDbContext context) 
        {
            List<SubDeptClassModel> classes = await (from c in context.SubDepartmentClasses
                                                     orderby c.ClassName ascending
                                                     select c).ToListAsync();
            return classes;
        }

        /// <summary>
        /// Gets all the classes for that sub department
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<List<SubDeptClassModel>> GetAllSubDeptClassesById(ApplicationDbContext context, int id) 
        {
            List<SubDeptClassModel> classes = await (from c in context.SubDepartmentClasses
                                               where c.SubDeptId == id
                                               select c).ToListAsync();
            return classes;
        }

        /// <summary>
        /// Gets a sub department class by id
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        public static async Task<SubDeptClassModel> GetSubDeptClassById(ApplicationDbContext context, int id) 
        {
            SubDeptClassModel classes = await (from c in context.SubDepartmentClasses
                                               where c.ClassId == id
                                               select c).SingleOrDefaultAsync();
            return classes;
        }

        /// <summary>
        /// Adds a new sub department class to the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="deptClass"></param>
        public static async Task<SubDeptClassModel> AddSubDeptClass(ApplicationDbContext context, SubDeptClassModel deptClass) 
        {
            await context.AddAsync(deptClass);
            await context.SaveChangesAsync();
            return deptClass;
        }

        /// <summary>
        /// Updates a sub department class in a database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="deptClass"></param>
        public static async Task<SubDeptClassModel> UpdateSubDeptClass(ApplicationDbContext context, SubDeptClassModel deptClass) 
        {
            await context.AddAsync(deptClass);
            context.Entry(deptClass).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return deptClass;
        }

        /// <summary>
        /// Deletes a sub department class in the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="deptClass"></param>
        /// <returns></returns>
        public static async Task DeleteSubDeptClass(ApplicationDbContext context, SubDeptClassModel deptClass) 
        {
            await context.AddAsync(deptClass);
            context.Entry(deptClass).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}
