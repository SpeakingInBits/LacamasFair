using LacamasFair.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LacamasFair.Data
{
    public class BoardMemberDb
    {
        /// <summary>
        /// Gets all of the board members out of the database
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<List<BoardMember>> GetAllBoardMembers(ApplicationDbContext context) 
        {
            List<BoardMember> boardMembers = await (from m in context.BoardMembers
                                                    orderby m.Name ascending
                                                    select m).ToListAsync();
            return boardMembers;
        }

        /// <summary>
        /// Gets a single board member by id
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<BoardMember> GetBoardMemberById(ApplicationDbContext context, int id) 
        {
            BoardMember member = await (from m in context.BoardMembers
                                        where m.BoardMemberId == id
                                        select m).SingleOrDefaultAsync();
            return member;
        }

        /// <summary>
        /// Adds a new board member to the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public static async Task<BoardMember> AddBoardMember(ApplicationDbContext context, BoardMember member) 
        {
            await context.AddAsync(member);
            await context.SaveChangesAsync();
            return member;
        }

        /// <summary>
        /// Updates the board member in the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public static async Task<BoardMember> UpdateBoardMember(ApplicationDbContext context, BoardMember member) 
        {
            await context.AddAsync(member);
            context.Entry(member).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return member;
        }

        /// <summary>
        /// Deletes a board member in the database
        /// </summary>
        /// <param name="context"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public static async Task DeleteBoardMember(ApplicationDbContext context, BoardMember member) 
        {
            await context.AddAsync(member);
            context.Entry(member).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}
