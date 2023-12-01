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
    public class UserStatusCodesController : Controller
    {
        private readonly MemberDataContext _context;

        public UserStatusCodesController(MemberDataContext context)
        {
            _context = context;
        }

        // GET: UserStatusCodes
        public async Task<IActionResult> Index()
        {
              return _context.UserStatusDirectoryData != null ? 
                          View(await _context.UserStatusDirectoryData.ToListAsync()) :
                          Problem("Entity set 'MemberDataContext.UserStatusDirectoryData'  is null.");
        }

        // GET: UserStatusCodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserStatusDirectoryData == null)
            {
                return NotFound();
            }

            var userStatusCodes = await _context.UserStatusDirectoryData
                .FirstOrDefaultAsync(m => m.StatusCode == id);
            if (userStatusCodes == null)
            {
                return NotFound();
            }

            return View(userStatusCodes);
        }

        // GET: UserStatusCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserStatusCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusCode,StatusMessage")] UserStatusCodes userStatusCodes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userStatusCodes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userStatusCodes);
        }

        // GET: UserStatusCodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserStatusDirectoryData == null)
            {
                return NotFound();
            }

            var userStatusCodes = await _context.UserStatusDirectoryData.FindAsync(id);
            if (userStatusCodes == null)
            {
                return NotFound();
            }
            return View(userStatusCodes);
        }

        // POST: UserStatusCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusCode,StatusMessage")] UserStatusCodes userStatusCodes)
        {
            if (id != userStatusCodes.StatusCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userStatusCodes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserStatusCodesExists(userStatusCodes.StatusCode))
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
            return View(userStatusCodes);
        }

        // GET: UserStatusCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserStatusDirectoryData == null)
            {
                return NotFound();
            }

            var userStatusCodes = await _context.UserStatusDirectoryData
                .FirstOrDefaultAsync(m => m.StatusCode == id);
            if (userStatusCodes == null)
            {
                return NotFound();
            }

            return View(userStatusCodes);
        }

        // POST: UserStatusCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserStatusDirectoryData == null)
            {
                return Problem("Entity set 'MemberDataContext.UserStatusDirectoryData'  is null.");
            }
            var userStatusCodes = await _context.UserStatusDirectoryData.FindAsync(id);
            if (userStatusCodes != null)
            {
                _context.UserStatusDirectoryData.Remove(userStatusCodes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserStatusCodesExists(int id)
        {
          return (_context.UserStatusDirectoryData?.Any(e => e.StatusCode == id)).GetValueOrDefault();
        }
    }
}
