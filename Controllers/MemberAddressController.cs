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
        public async Task<IActionResult> Details(int? id, int?AuthId)
        {
            if (id == null || _context.MemberAddressDirectoryData == null)
            {
                return NotFound();
            }

            ViewBag.AuthId = AuthId;

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
            ViewData["MemNo"] = new SelectList(_context.MemberDirectoryData, "Id", "Id");
            return View();
        }

        // POST: MemberAddress/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Address,Country,State,City,PostalCode,AdditonalInfo,AddressType,MemNo,AuthId")] MemberAddressData memberAddressData)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(memberAddressData);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            //}
            //ViewData["MemNo"] = new SelectList(_context.MemberDirectoryData, "Id", "Email", memberAddressData.MemNo);
            //return View(memberAddressData);
        }

        // GET: MemberAddress/Edit/5
        public async Task<IActionResult> Edit(int id, int? AuthId)
        {
            if (id == null || _context.MemberAddressDirectoryData == null)
            {
                return NotFound();
            }

            ViewBag.AuthId = AuthId;

            var userdata = await _context.UserDirectoryData.FirstOrDefaultAsync(m => m.UserId == AuthId);
            if (userdata.UserRoleId == 1)
            {
                ViewBag.msg = "Success";
            }

            var memberAddressData = await _context.MemberAddressDirectoryData.FindAsync(id);
            if (memberAddressData == null)
            {
                return NotFound();
            }
            //ViewData["MemNo"] = new SelectList(_context.MemberDirectoryData, "Id", "Id", memberAddressData.MemNo);
            return View(memberAddressData);
        }

        // POST: MemberAddress/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address,Country,State,City,PostalCode,AdditonalInfo,AddressType,MemNo,AuthId")] MemberAddressData memberAddressData)
        {
            if (id != memberAddressData.Id)
            {
                return NotFound();
            }

            var userdata = await _context.UserDirectoryData.FirstOrDefaultAsync(m => m.UserId == memberAddressData.AuthId);

            if (userdata.UserRoleId == 2)
            {
                var allrecords = _context.MemberProposedDirectoryData.ToList();
                _context.MemberProposedDirectoryData.RemoveRange(allrecords);

                MemberProposedData proposedData = new MemberProposedData
                {
                    Address = memberAddressData.Address,
                    Country = memberAddressData.Country,
                    State = memberAddressData.State,
                    City = memberAddressData.City,
                    PostalCode = memberAddressData.PostalCode,
                    AdditonalInfo = memberAddressData.AdditonalInfo,
                    AddressType = memberAddressData.AddressType,
                    AuthId = memberAddressData.AuthId,
                    MemBusinessId = memberAddressData.Id
                };
                try
                {
                    _context.Update(proposedData);
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
            }

            else if(userdata.UserRoleId == 1)
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
            }
            ViewBag.AuthId = memberAddressData.AuthId;
            //ViewData["MemNo"] = new SelectList(_context.MemberDirectoryData, "Id", "Email", memberAddressData.MemNo);
            return RedirectToAction("Index","Home", new { AuthId = ViewBag.AuthId });
        }
            
        

        // GET: MemberAddress/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MemberAddressDirectoryData == null)
            {
                return NotFound();
            }

            var memberAddressData = await _context.MemberAddressDirectoryData.Include(m => m.MemberData).FirstOrDefaultAsync(m => m.Id == id);
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
            return RedirectToAction("Index", "Home");
        }

        private bool MemberAddressDataExists(int id)
        {
          return (_context.MemberAddressDirectoryData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
