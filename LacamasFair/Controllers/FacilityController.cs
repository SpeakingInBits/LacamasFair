using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LacamasFair.Data;
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
    }
}