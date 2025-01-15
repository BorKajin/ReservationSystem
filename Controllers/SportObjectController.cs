using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Models;

namespace ReservationSystem.Controllers
{
    public class SportObjectController : Controller
    {
        private readonly ReservationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SportObjectController(ReservationContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SportObject
        public async Task<IActionResult> Index()
        {
            IQueryable<SportObject> sportObjectContext =
                _context.SportObjects
                    .Include(s => s.User);
            return View(await sportObjectContext.ToListAsync());
        }

        // GET: SportObject/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportObject = await _context.SportObjects
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sportObject == null)
            {
                return NotFound();
            }

            return View(sportObject);
        }

        // GET: SportObject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SportObject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Location,Capacity")] SportObject sportObject)
        {
            sportObject.User = await _userManager.GetUserAsync(User);
            sportObject.UserId = sportObject.User.Id;
            if (ModelState.IsValid)
            {
                _context.Add(sportObject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sportObject);
        }

        // GET: SportObject/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportObject = await _context.SportObjects.FindAsync(id);
            if (sportObject == null)
            {
                return NotFound();
            }
            return View(sportObject);
        }

        // POST: SportObject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Location,Capacity,UserId")] SportObject sportObject)
        {
            if (id != sportObject.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sportObject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SportObjectExists(sportObject.ID))
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
            ViewData["UserId"] = sportObject.UserId;
            return View(sportObject);
        }

        // GET: SportObject/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sportObject = await _context.SportObjects
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sportObject == null)
            {
                return NotFound();
            }

            return View(sportObject);
        }

        // POST: SportObject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sportObject = await _context.SportObjects.FindAsync(id);
            if (sportObject != null)
            {
                _context.SportObjects.Remove(sportObject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SportObjectExists(int id)
        {
            return _context.SportObjects.Any(e => e.ID == id);
        }
    }
}
