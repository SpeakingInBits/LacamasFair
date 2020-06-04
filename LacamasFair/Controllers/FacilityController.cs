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
    public class FacilityController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FacilityController(ApplicationDbContext context)
        {
            _context = context;
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
    }
}