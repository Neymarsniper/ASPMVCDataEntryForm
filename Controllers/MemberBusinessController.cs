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
        public async Task<IActionResult> Details(int? id, int? AuthId)
        {
            if (id == 0 || _context.MemberBusinessDirectoryData == null)
            {
                return NotFound();
            }

            ViewBag.AuthId = AuthId;

            var memberBusiessData = await _context.MemberBusinessDirectoryData.FirstOrDefaultAsync(m => m.MemNo == id);
            if (memberBusiessData == null)
            {
                return RedirectToAction("Create");
            }

            return View(memberBusiessData);
        }

        // GET: MemberBusiness/Create
        public IActionResult Create()
        {
            //var mydata = await _context.MemberBusinessDirectoryData.SingleOrDefaultAsync(b => b.MemNo == id);
            //MemberBusiessData memberBusiessData = new MemberBusiessData
            //{
            //    MemNo = mydata.MemNo
            //};
            //ViewData["MemNo"] = mydata.MemNo;
            ViewData["MemNo"] = new SelectList(_context.MemberDirectoryData, "Id", "Id");
            return View();
        }

        // POST: MemberBusiness/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MemNo,BusinessName,BusinessDetail,BusinessAddress,BusinessCity,BusinessPostalCode,BusinessEmail,AuthId")] MemberBusinessData memberBusiessData)
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
        public async Task<IActionResult> Edit(int id, int? AuthId)
        {
            if (id == null || _context.MemberBusinessDirectoryData == null)
            {
                return NotFound();
            }

            ViewBag.AuthId = AuthId;

            var userdata = await _context.UserDirectoryData.FirstOrDefaultAsync(m => m.UserId == AuthId);
            if (userdata.UserRoleId == 1)
            {
                ViewBag.msg = "Success";
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,MemNo,BusinessName,BusinessDetail,BusinessAddress,BusinessCity,BusinessPostalCode,BusinessEmail,AuthId")] MemberBusinessData memberBusiessData)
        {
            if (id != memberBusiessData.Id)
            {
                return NotFound();
            }

            var userdata = await _context.UserDirectoryData.FirstOrDefaultAsync(m => m.UserId == memberBusiessData.AuthId);

            if (userdata.UserRoleId == 2)
            {
                var allrecords = _context.MemberProposedDirectoryData.ToList();
                _context.MemberProposedDirectoryData.RemoveRange(allrecords);

                MemberProposedData proposedData = new MemberProposedData
                {
                    BusinessName = memberBusiessData.BusinessName,
                    BusinessDetail = memberBusiessData.BusinessDetail,
                    BusinessAddress = memberBusiessData.BusinessAddress,
                    BusinessCity = memberBusiessData.BusinessCity,
                    BusinessPostalCode = memberBusiessData.BusinessPostalCode,
                    BusinessEmail = memberBusiessData.BusinessEmail,
                    AuthId = memberBusiessData.AuthId,
                    MemBusinessId = memberBusiessData.Id
                };
                try
                {
                    _context.Update(proposedData);
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
            }

            else if (userdata.UserRoleId == 1)
            {
                try
                {
                    _context.Update(memberBusiessData);
                    var allrecords = _context.MemberProposedDirectoryData.ToList();
                    _context.MemberProposedDirectoryData.RemoveRange(allrecords);
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
            }

            ViewBag.AuthId = memberBusiessData.AuthId;

            return RedirectToAction("Index","Home", new { AuthId = ViewBag.AuthId });
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
            return RedirectToAction("Index", "Home");
        }

        private bool MemberBusiessDataExists(int id)
        {
          return (_context.MemberBusinessDirectoryData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
