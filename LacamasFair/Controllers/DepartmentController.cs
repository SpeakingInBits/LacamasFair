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

        #region Methods for Department Home Page
        [AllowAnonymous] // Anybody can view the departments
        public async Task<IActionResult> Home(int id)
        {
            //Get the department name
            string departmentName = await GetDepartmentName(id);
            ViewData["DepartmentName"] = departmentName;
            ViewData["id"] = id;

            //Grab that department with the correct id passed to model bind to the add sub department link
            DepartmentModel dept = await GetDepartment(id);

            //Gets all of the sub departments in the database and put it in a ViewBag
            ViewBag.SubDepartments = await GetUniqueSubDepartments();

            return View(dept);
        }

        private async Task<List<SubDeptIdModel>> GetUniqueSubDepartments()
        {
            List<SubDeptIdModel> list = new List<SubDeptIdModel>();
            List<SubDeptIdModel> subDepts = await SubDepartmentDb.GetAllSubDepartments(_context);

            //Loop that goes through each item in the subDept list and filters out the duplicate names
            foreach (var item in subDepts.GroupBy(s => s.SubDeptName).Select(i => i.First())) 
            {
                list.Add(item);
            }
            return list;
        }

        private async Task<DepartmentModel> GetDepartment(int id)
        {
            return await DepartmentDb.GetDepartmentById(_context, id);
        }

        private async Task<string> GetDepartmentName(int id)
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
            return departmentName;
        }
        #endregion

        #region Add parent department methods
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
        #endregion

        #region Edit parent department methods
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
        #endregion

        #region Delete parent department methods
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
        #endregion

        #region Methods for Sub Departments Page
        [AllowAnonymous]
        public async Task<IActionResult> SubDepartment(int id)
        {
            //Gets all of the sub departments that are tied to that specific id and puts it in a ViewBag
            ViewBag.SubDepartments = await GetAllSubDepartments(id);

            //Get the sub department with the id and it's classes and put it in a view bag
            SubDeptIdModel subDept = await GetSubDepartment(id);
            ViewBag.SubDepartmentClasses = await GetAllSubDeptClasses(_context, subDept.SubDeptName);

            return View(subDept);
        }

        private async Task<List<SubDeptIdModel>> GetAllSubDeptClasses(ApplicationDbContext context, string subDepartmentName) 
        {
            return await SubDepartmentDb.GetAllSubDepartmentClasses(_context, subDepartmentName);
        }

        private async Task<List<SubDeptIdModel>> GetAllSubDepartments(int id) 
        {
            return await SubDepartmentDb.GetAllSubDepartmentsById(_context, id);
        }

        private async Task<SubDeptIdModel> GetSubDepartment(int id)
        {
            return await SubDepartmentDb.GetSubDepartmentById(_context, id);
        }
        #endregion

        #region Add sub department methods
        [HttpGet]
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
                TempData["Message"] = $"{subDepartment.SubDeptName} sub department added successfully";
                return RedirectToAction(nameof(Home));
            }
            return View();
        }
        #endregion

        #region Delete sub department methods
        public async Task<IActionResult> DeleteSubDepartment(int id) 
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> DeleteSubDepartmentConfirmed() 
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Add sub department class methods
        public async Task<IActionResult> AddSubDeptClass(int id) 
        {
            SubDeptIdModel subDept = await SubDepartmentDb.GetSubDepartmentById(_context, id);
            return View(subDept);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubDeptClass(SubDeptIdModel subDeptClass) 
        {
            if (ModelState.IsValid) 
            {
                await SubDepartmentDb.AddSubDepartment(_context, subDeptClass);
                TempData["Message"] = $"{subDeptClass.SubDeptName} sub department class added successfully";
                return RedirectToAction(nameof(Home));
            }
            return View();
        }
        #endregion

        #region Add class entry rule methods
        public async Task<IActionResult> EditSubDeptClass(int id) 
        {
            SubDeptIdModel subDept = await SubDepartmentDb.GetSubDepartmentById(_context, id);
            return View(subDept);
        }

        [HttpPost]
        public async Task<IActionResult> EditSubDeptClass(SubDeptIdModel subDeptClass) 
        {
            if (ModelState.IsValid) 
            {
                await SubDepartmentDb.UpdateSubDepartment(_context, subDeptClass);
                TempData["Message"] = "Class entry rule added successfully";
                return RedirectToAction(nameof(Home));
            }
            return View();
        }
        #endregion
    }
}