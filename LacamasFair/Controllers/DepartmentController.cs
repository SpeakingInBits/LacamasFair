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
            //Grab that department with the correct id passed to model bind to the add sub department link
            DepartmentModel dept = await DepartmentDb.GetDepartmentById(_context, id);

            if (id == 0)
            {
                ViewData["DepartmentName"] = "";
            }
            else 
            {
                ViewData["DepartmentName"] = dept.DepartmentName;
            }

            ViewData["id"] = id;

            //Gets all of the sub departments in the database and put it in a ViewBag
            ViewBag.SubDepartments = await SubDepartmentDb.GetAllSubDepartments(_context);

            return View(dept);
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
                TempData["Message"] = $"{department.DepartmentName} Department added successfully";
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
                TempData["Message"] = $"{department.DepartmentName} Department edited successfully";
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
            TempData["Message"] = $"{department.DepartmentName} Department deleted successfully";
            return RedirectToAction(nameof(Home));
        }

        [AllowAnonymous]
        public async Task<IActionResult> SubDepartment(int id)
        {
            //Gets all of the sub departments that are tied to that specific id and puts it in a ViewBag
            ViewBag.SubDepartments = await SubDepartmentDb.GetAllSubDepartmentsById(_context, id);

            //Get the sub department with the id and it's classes and put it in a view bag
            SubDeptIdModel subDept = await SubDepartmentDb.GetSubDepartmentById(_context, id);
            ViewBag.SubDepartmentClasses = await SubDeptClassDb.GetAllSubDeptClassesById(_context, subDept.SubDeptId);

            return View(subDept);
        }

        [HttpGet]
        public IActionResult AddSubDepartment(int id)
        {
            ViewData["id"] = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSubDepartment(SubDeptIdModel subDepartment, int id)
        {
            if (ModelState.IsValid)
            {
                await SubDepartmentDb.AddSubDepartment(_context, subDepartment);
                TempData["Message"] = $"{subDepartment.SubDeptName} sub department added successfully";
                return Redirect($"/Department/Home/{id}");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditSubDepartment(int id) 
        {
            SubDeptIdModel subDept = await SubDepartmentDb.GetSubDepartmentById(_context, id);
            ViewData["id"] = subDept.DepartmentId;
            return View(subDept);
        }

        [HttpPost]
        public async Task<IActionResult> EditSubDepartment(SubDeptIdModel subDepartment, int id) 
        {
            if (ModelState.IsValid) 
            {
                await SubDepartmentDb.UpdateSubDepartment(_context, subDepartment);
                TempData["Message"] = $"{subDepartment.SubDeptName} sub department edited successfully";
                return Redirect($"/Department/Home/{id}");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSubDepartment(int id) 
        {
            SubDeptIdModel subDepartment = await SubDepartmentDb.GetSubDepartmentById(_context, id);
            ViewData["id"] = subDepartment.DepartmentId;
            if (subDepartment == null) 
            {
                return NotFound();
            }
            return View(subDepartment);
        }

        [HttpPost, ActionName("DeleteSubDepartment")]
        public async Task<IActionResult> DeleteSubDepartmentConfirmed(int id, int deptId) 
        {
            SubDeptIdModel subDepartment = await SubDepartmentDb.GetSubDepartmentById(_context, id);
            await SubDepartmentDb.DeleteSubDepartmentById(_context, subDepartment);
            TempData["Message"] = $"{subDepartment.SubDeptName} sub department deleted successfully";
            return Redirect($"/Department/Home/{deptId}");
        }

        [HttpGet]
        public IActionResult AddSubDeptClass(int id) 
        {
            ViewData["id"] = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSubDeptClass(SubDeptClassModel subDeptClass, int id) 
        {
            if (ModelState.IsValid)
            {
                await SubDeptClassDb.AddSubDeptClass(_context, subDeptClass);
                TempData["Message"] = "Added successfully";
                return Redirect($"/Department/SubDepartment/{id}");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditSubDeptClass(int classId, int subDeptId)
        {
            SubDeptClassModel subDeptClass = await SubDeptClassDb.GetSubDeptClassById(_context, classId);
            ViewData["ClassId"] = classId;
            ViewData["SubDeptId"] = subDeptId;
            return View(subDeptClass);
        }

        [HttpPost]
        public async Task<IActionResult> EditSubDeptClass(SubDeptClassModel subDeptClass, int id)
        {
            if (ModelState.IsValid)
            {
                await SubDeptClassDb.UpdateSubDeptClass(_context, subDeptClass);
                TempData["Message"] = "Edited successfully";
                return Redirect($"/Department/SubDepartment/{id}");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSubDeptClass(int id)
        {
            SubDeptClassModel subDeptClass = await SubDeptClassDb.GetSubDeptClassById(_context, id);
            ViewData["SubDeptId"] = subDeptClass.SubDeptId;
            return View(subDeptClass);
        }

        [HttpPost, ActionName("DeleteSubDeptClass")]
        public async Task<IActionResult> DeleteSubDeptClassConfirmed(int id, int subDeptId)
        {
            SubDeptClassModel subDepartment = await SubDeptClassDb.GetSubDeptClassById(_context, id);
            await SubDeptClassDb.DeleteSubDeptClass(_context, subDepartment);
            TempData["Message"] = "Deleted successfully";
            return Redirect($"/Department/SubDepartment/{subDeptId}");
        }
    }
}