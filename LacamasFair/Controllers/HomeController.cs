using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LacamasFair.Models;
using LacamasFair.Data;
using Microsoft.AspNetCore.Authorization;

namespace LacamasFair.Controllers
{
    [Authorize(IdentityHelper.Administrator)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult HistoryAndGoal() 
        {
            return View();
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

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> FacilityRental(int id)
        {
            List<FacilityRental> facilities = await FacilityRentalDb.GetAllFacilityRentals(_context);
            ViewBag.Facility = facilities;
            return View();
        }

        [HttpGet]
        public IActionResult AddFacilityRental() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFacilityRental(FacilityRental facility) 
        {
            if (ModelState.IsValid) 
            {
                await FacilityRentalDb.AddFacilityRental(_context, facility);
                TempData["Message"] = $"{facility.FacilityName} rental added successfully";
                return RedirectToAction(nameof(FacilityRental));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditFacilityRental(int? id) 
        {
            if (id == null) 
            {
                return BadRequest();
            }
            FacilityRental facility = await FacilityRentalDb.GetFacilityRentalById(_context, id.Value);
            if (facility == null) 
            {
                return NotFound();
            }
            return View(facility);
        }

        [HttpPost]
        public async Task<IActionResult> EditFacilityRental(FacilityRental facility) 
        {
            if (ModelState.IsValid) 
            {
                await FacilityRentalDb.UpdateFacilityRental(_context, facility);
                TempData["Message"] = $"{facility.FacilityName} edited successfully";
                return RedirectToAction(nameof(FacilityRental));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFacilityRental(int id) 
        {
            FacilityRental facility = await FacilityRentalDb.GetFacilityRentalById(_context, id);
            if (facility == null) 
            {
                return NotFound();
            }
            return View(facility);
        }

        [HttpPost, ActionName("DeleteFacilityRental")]
        public async Task<IActionResult> DeleteFacilityRentalConfirmed(int id) 
        {
            FacilityRental facility = await FacilityRentalDb.GetFacilityRentalById(_context, id);
            await FacilityRentalDb.DeleteFacilityRental(_context, facility);
            TempData["Message"] = $"{facility.FacilityName} deleted successfully";
            return RedirectToAction(nameof(FacilityRental));
        }

        [AllowAnonymous]
        public IActionResult CommunityCenter() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
