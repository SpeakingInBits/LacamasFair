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
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous] // Anybody can view the departments
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddDepartment() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(DepartmentModel department) 
        {
            if(ModelState.IsValid)
            {
                await DepartmentDb.AddDepartment(_context, department);
                TempData["Message"] = $"{department.DepartmentName} department added successfully";
                return RedirectToAction(nameof(Home));
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            DepartmentModel department = await DepartmentDb.GetDepartmentById(_context, id);
            if (department == null) 
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentModel department) 
        {
            if (ModelState.IsValid) 
            {
                await DepartmentDb.UpdateDepartment(_context, department);
                ViewData["Message"] = $"{department.DepartmentName} added successfully";
                return View(department);
            }
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id) 
        {
            DepartmentModel department = await DepartmentDb.GetDepartmentById(_context, id);
            if (department == null) 
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            DepartmentModel department = await DepartmentDb.GetDepartmentById(_context, id);
            await DepartmentDb.DeleteDepartmentById(_context, id);
            TempData["Message"] = $"{department.DepartmentName} department deleted successfully";
            return RedirectToAction(nameof(Home));
        }
    }
}