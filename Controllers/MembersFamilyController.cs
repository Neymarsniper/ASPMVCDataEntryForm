using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemberDataEntryForm.Models;
using SQLitePCL;
using System.Diagnostics.Metrics;

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
        public async Task<IActionResult> Details(int? id , int? AuthId)
        {
            if (id == 0 || _context.MemberFamilyDirectoryData == null)
            {
                return NotFound();
            }

            ViewBag.AuthId = AuthId;

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
            //using (var dbContext = new MemberDataContext())
            //{
            //    int memno = 
            //    MemberData member = dbContext.MemberDirectoryData.FirstOrDefaultAsync(m => m.Id == memno);
            //    MembersFamilyData membersFamilyData = new MembersFamilyData
            //    {
            //        MemNo = member.Id
            //    };
            //}
            ViewData["MemNo"] = new SelectList(_context.MemberDirectoryData, "Id", "Id");
            return View();
        }

        // POST: MemberFamily/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemNo,FirstName,LastName,Mobile,Relation,HomeAddress,ChildName,AuthId")] MembersFamilyData memberFamilyData)
        {
            //MembersFamilyData membersFamilyData = new MembersFamilyData()
            //{
            //    MemNo = memberViewModel.memberData.Id
            //};
            //using (var dbContext = new MemberDataContext())
            //{
            //    int memno = memberFamilyData.MemNo;
            //    MemberData member = await dbContext.MemberDirectoryData.FirstOrDefaultAsync(m => m.Id == memno);
            //    MembersFamilyData membersFamilyData = new MembersFamilyData
            //    {
            //        MemNo = member.Id
            //    };
            //}

            //if (ModelState.IsValid)
           // {
               _context.MemberFamilyDirectoryData.Add(memberFamilyData);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
           // }
            //return View(memberFamilyData);
        }

        // GET: MemberFamily/Edit/5
        public async Task<IActionResult> Edit(int? id, int? AuthId)
        {
            if (id == null || _context.MemberFamilyDirectoryData == null)
            {
                return NotFound();
            }

            ViewBag.AuthId = AuthId;

            var userdata = await _context.UserDirectoryData.FirstOrDefaultAsync(m => m.UserId == AuthId);
            if (userdata.UserRoleId == 1)
            {
                ViewBag.msg = "Success";
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
        public async Task<IActionResult> Edit(int id, int AuthId, [Bind("Id,MemNo,FirstName,LastName,Mobile,Relation,HomeAddress,ChildName,AuthId")] MembersFamilyData memberFamilyData)
        {
            if (id != memberFamilyData.Id)
            {
                return NotFound();
            }

            var userdata = await _context.UserDirectoryData.FirstOrDefaultAsync(m => m.UserId == memberFamilyData.AuthId);

            if (userdata.UserRoleId == 2)
            {
                var allrecords = _context.MemberProposedDirectoryData.ToList();
                _context.MemberProposedDirectoryData.RemoveRange(allrecords);

                MemberProposedData proposedData = new MemberProposedData
                {
                    FirstName = memberFamilyData.FirstName,
                    LastName = memberFamilyData.LastName,
                    Mobile = memberFamilyData.Mobile,
                    ChildName = memberFamilyData.ChildName,
                    Relation = memberFamilyData.Relation,
                    HomeAddress = memberFamilyData.HomeAddress,
                    AuthId = memberFamilyData.AuthId,
                    MemFamilyId = memberFamilyData.Id
                };
                try
                {
                    _context.Update(proposedData);
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
            }

            else if (userdata.UserRoleId == 1)
            {
                try
                {
                    _context.Update(memberFamilyData);
                    var allrecords = _context.MemberProposedDirectoryData.ToList();
                    _context.MemberProposedDirectoryData.RemoveRange(allrecords);
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
            }

            ViewBag.AuthId = memberFamilyData.AuthId;

            return RedirectToAction("Index","Home", new {AuthId = ViewBag.AuthId });
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
            return RedirectToAction("Index", "Home");
        }

        private bool MemberFamilyDataExists(int id)
        {
            return (_context.MemberFamilyDirectoryData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
