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
