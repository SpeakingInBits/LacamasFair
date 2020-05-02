using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LacamasFair.Data;
using LacamasFair.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LacamasFair.Controllers
{
    [Authorize(IdentityHelper.Administrator)]
    public class BoardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BoardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> FairBoard()
        {
            List<BoardMember> clubOfficers = new List<BoardMember>();
            List<BoardMember> fairOfficers = new List<BoardMember>();

            List<BoardMember> members = await BoardMemberDb.GetAllBoardMembers(_context);

            foreach (BoardMember item in members)
            {
                if (item.FairOrClubOfficer.Equals("Club Officer"))
                {
                    clubOfficers.Add(item);
                }
                else
                {
                    fairOfficers.Add(item);
                }
            }

            ViewBag.ClubOfficers = clubOfficers;
            ViewBag.FairOfficers = fairOfficers;

            return View();
        }

        [HttpGet]
        public IActionResult AddBoardMember()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBoardMember(BoardMember member)
        {
            if (ModelState.IsValid)
            {
                await BoardMemberDb.AddBoardMember(_context, member);
                TempData["Message"] = $"{member.Name} added successfully";
                return RedirectToAction(nameof(FairBoard));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditBoardMember(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            BoardMember member = await BoardMemberDb.GetBoardMemberById(_context, id.Value);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> EditBoardMember(BoardMember member)
        {
            if (ModelState.IsValid)
            {
                await BoardMemberDb.UpdateBoardMember(_context, member);
                TempData["Message"] = $"{member.Name} edited successfully";
                return RedirectToAction(nameof(FairBoard));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBoardMember(int id)
        {
            BoardMember member = await BoardMemberDb.GetBoardMemberById(_context, id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        [HttpPost, ActionName("DeleteBoardMember")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            BoardMember member = await BoardMemberDb.GetBoardMemberById(_context, id);
            await BoardMemberDb.DeleteBoardMember(_context, member);
            TempData["Message"] = $"{member.Name} deleted successfully";
            return RedirectToAction(nameof(FairBoard));
        }
    }
}