using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetPhonebook.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetPhonebook.Controllers
{
    public class PhonesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PhonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var phones = await _context.Phones.ToListAsync();
            return View(phones);
        }
    }
}