using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetPhonebook.Data;
using DotnetPhonebook.Models.Domain;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Phone phone)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(phone);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong, invalid model");

            return View(phone);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var exist = await _context.Phones.Where(x => x.Id == id).FirstOrDefaultAsync();

            return View(exist);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Phone phone)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = _context.Phones.Where(x => x.Id == phone.Id).FirstOrDefault();

                    if(exist != null)
                    {
                        exist.OwnerName = phone.OwnerName;
                        exist.Number = phone.Number;
                        exist.Email = phone.Email;

                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong, invalid model");

            return View(phone);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var exist = await _context.Phones.Where(x => x.Id == id).FirstOrDefaultAsync();

            return View(exist);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Phone phone)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = _context.Phones.Where(x => x.Id == phone.Id).FirstOrDefault();

                    if(exist != null)
                    {
                        _context.Remove(exist);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    } 
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong, invalid model");

            return View();
        }
    }
}