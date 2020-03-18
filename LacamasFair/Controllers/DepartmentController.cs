using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LacamasFair.Data;
using LacamasFair.Data.Migrations;
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
        public async Task<IActionResult> Home(int id)
        {
            List<DepartmentModel> departments = await DepartmentDb.GetAllDepartments(_context);
            string departmentName = "";
            foreach (DepartmentModel item in departments) 
            {
                if (item.DepartmentId == id) 
                {
                    departmentName = item.DepartmentName;
                }
            }
            ViewData["DepartmentName"] = departmentName;
            ViewData["id"] = id;
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
        public async Task<IActionResult> EditDepartment(int? id) 
        {
            if (id == null)
            {
                //HTTP 400
                return BadRequest();
            }
            DepartmentModel department = await DepartmentDb.GetDepartmentById(_context, id.Value);
            if (department == null)
            {
                //Returns an HTTP 404 - Not Found Error
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> EditDepartment(DepartmentModel department) 
        {
            if (ModelState.IsValid) 
            {
                await DepartmentDb.UpdateDepartment(_context, department);
                TempData["Message"] = $"{department.DepartmentName} edited successfully";
                return RedirectToAction(nameof(Home));
            }
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDepartment(int id) 
        {
            DepartmentModel department = await DepartmentDb.GetDepartmentById(_context, id);
            if (department == null) 
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost, ActionName("DeleteDepartment")]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            DepartmentModel department = await DepartmentDb.GetDepartmentById(_context, id);
            await DepartmentDb.DeleteDepartment(_context, department);
            TempData["Message"] = $"{department.DepartmentName} department deleted successfully";
            return RedirectToAction(nameof(Home));
        }

        public IActionResult AddSubDepartment(int id)
        {
            ViewData["id"] = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSubDepartment(SubDeptIdModel subDepartment)
        {
            if (ModelState.IsValid)
            {
                await SubDepartmentDb.AddSubDepartment(_context, subDepartment);
                TempData["Message"] = $"{subDepartment.SubDeptName} department added successfully";
                return RedirectToAction(nameof(Home));
            }
            return View();
        }
    }
}