using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rejestr_ludzikow_zaginionych_v5.Data;
using rejestr_ludzikow_zaginionych_v5.Models;
using rejestr_ludzikow_zaginionych_v5.ModelViews;

namespace rejestr_ludzikow_zaginionych_v5.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PeopleController(ApplicationDbContext context, IAuthorizationService authorizationService, IWebHostEnvironment environment)
        {
            _context = context;
            _authorizationService = authorizationService;
            _webHostEnvironment = environment;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.People.Include(p => p.Owner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(PersonViewModel personViewModel)
        {
            if (ModelState.IsValid)
            {
                Person person = new Person
                {
                    Name = personViewModel.Name,
                    Surname = personViewModel.Surname,
                    IsWoman = personViewModel.IsWoman,
                    Description = personViewModel.Description,
                    LastSeenLocation = personViewModel.LastSeenLocation,
                };
                person.OwnerId = QueryForOwnerId();
                person.Image = UploadImageFile(personViewModel.Image);
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: People/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            if ((await _authorizationService.AuthorizeAsync(User, person, "EditPolicy")).Succeeded)
            {
                return View(person);
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,IsWoman,Surname,Name,Description,LastSeenLocation,OwnerId")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            if ((await _authorizationService.AuthorizeAsync(User, person, "EditPolicy")).Succeeded)
            {
                return View(person);
            }
            else
            {
                return new ChallengeResult();
            }
        }

        // GET: People/Delete/5
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.FindAsync(id);
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }

        private int QueryForOwnerId()
        {
            Models.User owner = _context.Users.Where(b => b.Email.Equals(this.User.Identity.Name)).FirstOrDefault();
            return owner.Id;
        }

        private string UploadImageFile(IFormFile image)
        {
            string uniqueFileName = null;

            if (image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString()+ "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
