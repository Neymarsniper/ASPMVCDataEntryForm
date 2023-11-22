using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemberDataEntryForm.Models;

namespace PPCLUB.Controllers
{
    public class UserTypeController : Controller
    {
        private readonly MemberDataContext _context;

        public UserTypeController(MemberDataContext context)
        {
            _context = context;
        }

        // GET: UserType
        public async Task<IActionResult> Index()
        {
              return _context.GetUserTypes != null ? View(await _context.GetUserTypes.ToListAsync()) :Problem("Entity set 'MemberDataContext.GetUserTypes'  is null.");
        }

        // GET: UserType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GetUserTypes == null)
            {
                return NotFound();
            }

            var userType = await _context.GetUserTypes.FirstOrDefaultAsync(m => m.RoleId == id);
            if (userType == null)
            {
                return NotFound();
            }

            return View(userType);
        }

        // GET: UserType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,RoleName")] UserType userType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userType);
        }

        // GET: UserType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GetUserTypes == null)
            {
                return NotFound();
            }

            var userType = await _context.GetUserTypes.FindAsync(id);
            if (userType == null)
            {
                return NotFound();
            }
            return View(userType);
        }

        // POST: UserType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,RoleName")] UserType userType)
        {
            if (id != userType.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTypeExists(userType.RoleId))
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
            return View(userType);
        }

        // GET: UserType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GetUserTypes == null)
            {
                return NotFound();
            }

            var userType = await _context.GetUserTypes.FirstOrDefaultAsync(m => m.RoleId == id);
            if (userType == null)
            {
                return NotFound();
            }

            return View(userType);
        }

        // POST: UserType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GetUserTypes == null)
            {
                return Problem("Entity set 'MemberDataContext.GetUserTypes'  is null.");
            }
            var userType = await _context.GetUserTypes.FindAsync(id);
            if (userType != null)
            {
                _context.GetUserTypes.Remove(userType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTypeExists(int id)
        {
          return (_context.GetUserTypes?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }
    }
}
