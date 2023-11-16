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
    public class MembersFamilyController : Controller
    {
        private readonly MemberDataContext _context;

        public MembersFamilyController(MemberDataContext context)
        {
            _context = context;
        }

        // GET: MemberFamily
        public async Task<IActionResult> Index()
        {
            return _context.MemberFamilyDirectoryData != null ? View(await _context.MemberFamilyDirectoryData.ToListAsync()) : Problem("Entity set 'MemberDataContext.MemberFamilyDirectoryData'  is null.");
        }

        // GET: MemberFamily/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == 0 || _context.MemberFamilyDirectoryData == null)
            {
                return NotFound();
            }

            var memberFamilyData = await _context.MemberFamilyDirectoryData.FirstOrDefaultAsync(m => m.MemNo == id);
            if (memberFamilyData == null)
            {
                return RedirectToAction(nameof(Create));
            }

            return View(memberFamilyData);
        }

        // GET: MemberFamily/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberFamily/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemNo,FirstName,LastName,Mobile,Relation,HomeAddress,ChildName")] MembersFamilyData memberFamilyData)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(memberFamilyData);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            //}
            //return View(memberFamilyData);
        }

        // GET: MemberFamily/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MemberFamilyDirectoryData == null)
            {
                return NotFound();
            }

            var memberFamilyData = await _context.MemberFamilyDirectoryData.FindAsync(id);
            if (memberFamilyData == null)
            {
                return NotFound();
            }
            return View(memberFamilyData);
        }

        // POST: MemberFamily/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MemNo,FirstName,LastName,Mobile,Relation,HomeAddress,ChildName")] MembersFamilyData memberFamilyData)
        {
            if (id != memberFamilyData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberFamilyData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberFamilyDataExists(memberFamilyData.Id))
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
            return View(memberFamilyData);
        }

        // GET: MemberFamily/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MemberFamilyDirectoryData == null)
            {
                return NotFound();
            }

            var memberFamilyData = await _context.MemberFamilyDirectoryData.FirstOrDefaultAsync(m => m.Id == id);
            if (memberFamilyData == null)
            {
                return NotFound();
            }

            return View(memberFamilyData);
        }

        // POST: MemberFamily/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MemberFamilyDirectoryData == null)
            {
                return Problem("Entity set 'MemberDataContext.MemberFamilyDirectoryData'  is null.");
            }
            var memberFamilyData = await _context.MemberFamilyDirectoryData.FindAsync(id);
            if (memberFamilyData != null)
            {
                _context.MemberFamilyDirectoryData.Remove(memberFamilyData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberFamilyDataExists(int id)
        {
            return (_context.MemberFamilyDirectoryData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
