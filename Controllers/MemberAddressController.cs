using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemberDataEntryForm.Models;

namespace MemberDataEntryForm.Controllers
{
    public class MemberAddressController : Controller
    {
        private readonly MemberDataContext _context;

        public MemberAddressController(MemberDataContext context)
        {
            _context = context;
        }

        // GET: MemberAddress
        public async Task<IActionResult> Index()
        {
            var memberDataContext = _context.MemberAddressDirectoryData.Include(m => m.MemberData);
            return View(await memberDataContext.ToListAsync());
        }

        // GET: MemberAddress/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MemberAddressDirectoryData == null)
            {
                return NotFound();
            }

            var memberAddressData = await _context.MemberAddressDirectoryData.Include(m => m.MemberData).FirstOrDefaultAsync(m => m.MemNo == id);
            if (memberAddressData == null)
            {
                return RedirectToAction("Create");
            }

            return View(memberAddressData);
        }

        // GET: MemberAddress/Create
        public IActionResult Create()
        {
            ViewData["MemNo"] = new SelectList(_context.MemberDirectoryData, "Id", "Email");
            return View();
        }

        // POST: MemberAddress/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Address,Country,State,City,PostalCode,AdditonalInfo,AddressType,MemNo")] MemberAddressData memberAddressData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberAddressData);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            ViewData["MemNo"] = new SelectList(_context.MemberDirectoryData, "Id", "Email", memberAddressData.MemNo);
            return View(memberAddressData);
        }

        // GET: MemberAddress/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MemberAddressDirectoryData == null)
            {
                return NotFound();
            }

            var memberAddressData = await _context.MemberAddressDirectoryData.FindAsync(id);
            if (memberAddressData == null)
            {
                return NotFound();
            }
            ViewData["MemNo"] = new SelectList(_context.MemberDirectoryData, "Id", "Email", memberAddressData.MemNo);
            return View(memberAddressData);
        }

        // POST: MemberAddress/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address,Country,State,City,PostalCode,AdditonalInfo,AddressType,MemNo")] MemberAddressData memberAddressData)
        {
            if (id != memberAddressData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberAddressData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberAddressDataExists(memberAddressData.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = memberAddressData.MemNo });
            }
            ViewData["MemNo"] = new SelectList(_context.MemberDirectoryData, "Id", "Email", memberAddressData.MemNo);
            return View(memberAddressData);
        }

        // GET: MemberAddress/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MemberAddressDirectoryData == null)
            {
                return NotFound();
            }

            var memberAddressData = await _context.MemberAddressDirectoryData
                .Include(m => m.MemberData)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberAddressData == null)
            {
                return NotFound();
            }

            return View(memberAddressData);
        }

        // POST: MemberAddress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MemberAddressDirectoryData == null)
            {
                return Problem("Entity set 'MemberDataContext.MemberAddressDirectoryData'  is null.");
            }
            var memberAddressData = await _context.MemberAddressDirectoryData.FindAsync(id);
            if (memberAddressData != null)
            {
                _context.MemberAddressDirectoryData.Remove(memberAddressData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberAddressDataExists(int id)
        {
          return (_context.MemberAddressDirectoryData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
