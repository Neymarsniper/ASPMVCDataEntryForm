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
    public class MemberBusinessController : Controller
    {
        private readonly MemberDataContext _context;

        public MemberBusinessController(MemberDataContext context)
        {
            _context = context;
        }

        // GET: MemberBusiness
        public async Task<IActionResult> Index()
        {
              return _context.MemberBusinessDirectoryData != null ? View(await _context.MemberBusinessDirectoryData.ToListAsync()) :Problem("Entity set 'MemberDataContext.MemberBusinessDirectoryData'  is null.");
        }

        // GET: MemberBusiness/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == 0 || _context.MemberBusinessDirectoryData == null)
            {
                return NotFound();
            }

            var memberBusiessData = await _context.MemberBusinessDirectoryData.FirstOrDefaultAsync(m => m.MemNo == id);
            if (memberBusiessData == null)
            {
                return RedirectToAction(nameof(Create));
            }

            return View(memberBusiessData);
        }

        // GET: MemberBusiness/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberBusiness/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemNo,BusinessName,BusinessDetail,BusinessAddress,BusinessCity,BusinessPostalCode,BusinessEmail")] MemberBusiessData memberBusiessData)
        {
            //if (ModelState.IsValid)
            //{
                //MemberData memberData = new MemberData();
                //memberBusiessData.Id = memberData.Id;
                _context.Add(memberBusiessData);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            //}
            //return View(memberBusiessData);
        }

        // GET: MemberBusiness/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.MemberBusinessDirectoryData == null)
            {
                return NotFound();
            }

            var memberBusiessData = await _context.MemberBusinessDirectoryData.FindAsync(id);
            if (memberBusiessData == null)
            {
                return NotFound();
            }
            return View(memberBusiessData);
        }

        // POST: MemberBusiness/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MemNo,BusinessName,BusinessDetail,BusinessAddress,BusinessCity,BusinessPostalCode,BusinessEmail")] MemberBusiessData memberBusiessData)
        {
            if (id != memberBusiessData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberBusiessData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberBusiessDataExists(memberBusiessData.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = memberBusiessData.MemNo });
            }
            return View(memberBusiessData);
        }

        // GET: MemberBusiness/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MemberBusinessDirectoryData == null)
            {
                return NotFound();
            }

            var memberBusiessData = await _context.MemberBusinessDirectoryData.FirstOrDefaultAsync(m => m.Id == id);
            if (memberBusiessData == null)
            {
                return NotFound();
            }

            return View(memberBusiessData);
        }

        // POST: MemberBusiness/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MemberBusinessDirectoryData == null)
            {
                return Problem("Entity set 'MemberDataContext.MemberBusinessDirectoryData'  is null.");
            }
            var memberBusiessData = await _context.MemberBusinessDirectoryData.FindAsync(id);
            if (memberBusiessData != null)
            {
                _context.MemberBusinessDirectoryData.Remove(memberBusiessData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberBusiessDataExists(int id)
        {
          return (_context.MemberBusinessDirectoryData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
