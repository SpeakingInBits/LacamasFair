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
        public async Task<IActionResult> Index()
        {
            ViewBag.Announcements = await AnnouncementDb.GetAllAnnouncements(_context);
            return View();
        }

        [HttpGet]
        public IActionResult AddAnnouncement() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAnnouncement(Announcement announcement) 
        {
            if (ModelState.IsValid) 
            {
                await AnnouncementDb.AddAnnouncement(_context, announcement);
                TempData["Message"] = $"{announcement.Title} announcement added successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditAnnouncement(int? id) 
        {
            if (id == null) 
            {
                return BadRequest();
            }
            Announcement announcement = await AnnouncementDb.GetAnnouncementById(_context, id.Value);
            if (announcement == null) 
            {
                return NotFound();
            }
            return View(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> EditAnnouncement(Announcement announcement) 
        {
            if (ModelState.IsValid) 
            {
                await AnnouncementDb.UpdateAnnouncement(_context, announcement);
                TempData["Message"] = $"{announcement.Title} announcement edited successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAnnouncement(int id) 
        {
            Announcement announcement = await AnnouncementDb.GetAnnouncementById(_context, id);
            if (announcement == null)
            {
                return NotFound();
            }
            return View(announcement);
        }

        [HttpPost, ActionName("DeleteAnnouncement")]
        public async Task<IActionResult> DeleteAnnouncementConfirmed(int id) 
        {
            Announcement announcement = await AnnouncementDb.GetAnnouncementById(_context, id);
            await AnnouncementDb.DeleteAnnouncement(_context, announcement);
            TempData["Message"] = $"{announcement.Title} announcement deleted";
            return RedirectToAction(nameof(Index));
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
