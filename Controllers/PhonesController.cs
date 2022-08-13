using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotnetPhonebook.Data;
using DotnetPhonebook.Models.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var phones = await _context.Phones.OrderBy(p => p.OwnerName).ToListAsync();
            return View(phones);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Phone()
        {
            var phones = await _context.Phones.OrderBy(p => p.OwnerName).ToListAsync();
            return View(phones);
        }

        // [Authorize(Roles = "Admin")]
        // [HttpGet]
        // public IActionResult Create()
        // {
        //     return View();
        // }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            Phone phone = new Phone();
            return PartialView("_PhoneModal", phone);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var exist = await _context.Phones.Where(x => x.Id == id).FirstOrDefaultAsync();

            return View(exist);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var exist = await _context.Phones.Where(x => x.Id == id).FirstOrDefaultAsync();

            return View(exist);
        }

        [Authorize(Roles = "Admin")]
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