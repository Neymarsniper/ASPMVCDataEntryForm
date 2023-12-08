using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemberDataEntryForm.Models;
using Azure;

namespace PPCLUB.Controllers
{
    public class MemberProposedDataController : Controller
    {
        private readonly MemberDataContext _context;

        public MemberProposedDataController(MemberDataContext context)
        {
            _context = context;
        }

        // GET: MemberProposedData
        public async Task<IActionResult> Index()
        {
            return _context.MemberProposedDirectoryData != null ?
                        View(await _context.MemberProposedDirectoryData.ToListAsync()) :
                        Problem("Entity set 'MemberDataContext.MemberProposedDirectoryData'  is null.");
        }

        // GET: MemberProposedData/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MemberProposedDirectoryData == null)
            {
                return NotFound();
            }

            var memberProposedData = await _context.MemberProposedDirectoryData
                .FirstOrDefaultAsync(m => m.MemProId == id);
            if (memberProposedData == null)
            {
                return NotFound();
            }

            return View(memberProposedData);
        }

        // GET: MemberProposedData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberProposedData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemProId,Name,Dob,ResAddress,ResPhone,OfficeNo,Profession,OfficeAddress,MobileNo,AlternateMobileNo,Email,DateofMarriage,NameofSpouse,SpouseDob,ChildName,Address,Country,State,City,PostalCode,AdditonalInfo,AddressType,FirstName,LastName,Relation,HomeAddress,Mobile,FamilyChildName,BusinessName,BusinessDetail,BusinessAddress,BusinessCity,BusinessPostalCode,BusinessEmail,AuthId")] MemberProposedData memberProposedData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberProposedData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memberProposedData);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //this is for MemberData Proposed data
        public async Task<IActionResult> Edit(int? id, int AuthId)
        {
            if (id == null || _context.MemberProposedDirectoryData == null)
            {
                return NotFound();
            }

            ViewBag.AuthId = AuthId;

            var memberProposedData = await _context.MemberProposedDirectoryData.Include(m => m.MemberData).FirstOrDefaultAsync(m => m.MemId == id);
            //memberProposedData.AuthId = AuthId;
            ViewBag.MemId = id;

            if (memberProposedData == null)
            {
                return RedirectToAction("Index", "Home", new { AuthId = ViewBag.AuthId });
            }
            return View(memberProposedData);
        }

        // POST: MemberProposedData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int AuthId, [Bind("MemProId,Name,Dob,ResAddress,ResPhone,OfficeNo,Profession,OfficeAddress,MobileNo,AlternateMobileNo,Email,DateofMarriage,NameofSpouse,SpouseDob,ChildName,Address,Country,State,City,PostalCode,AdditonalInfo,AddressType,FirstName,LastName,Relation,HomeAddress,Mobile,FamilyChildName,BusinessName,BusinessDetail,BusinessAddress,BusinessCity,BusinessPostalCode,BusinessEmail,AuthId,MemId")] MemberProposedData memberProposedData)
        {
            if (id != memberProposedData.MemId)
            {
                return NotFound();
            }

            var memberdata = await _context.MemberDirectoryData.FirstOrDefaultAsync(m => m.Id == memberProposedData.MemId);

            memberdata.Name = memberProposedData.Name;
            memberdata.Dob = memberProposedData.Dob;
            memberdata.ResAddress = memberProposedData.ResAddress;
            memberdata.ResPhone = memberProposedData.ResPhone;
            memberdata.OfficeAddress = memberProposedData.OfficeAddress;
            memberdata.OfficeNo = memberProposedData.OfficeNo;
            memberdata.Profession = memberProposedData.Profession;
            memberdata.MobileNo = memberProposedData.MobileNo;
            memberdata.AlternateMobileNo = memberProposedData.AlternateMobileNo;
            memberdata.Email = memberProposedData.Email;
            memberdata.DateofMarriage = memberProposedData.DateofMarriage;
            memberdata.NameofSpouse = memberProposedData.NameofSpouse;
            memberdata.SpouseDob = memberProposedData.SpouseDob;
            memberdata.ChildName = memberProposedData.ChildName;
            memberdata.AuthId = memberProposedData.AuthId;

            _context.MemberDirectoryData.Update(memberdata);
            var allrecords = _context.MemberProposedDirectoryData.ToList();
            _context.MemberProposedDirectoryData.RemoveRange(allrecords);
            await _context.SaveChangesAsync();

            ViewBag.AuthId = memberProposedData.AuthId;

            return RedirectToAction("Index", "Home", new { AuthId = ViewBag.AuthId });
        }

        // GET: MemberProposedData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MemberProposedDirectoryData == null)
            {
                return NotFound();
            }

            var memberProposedData = await _context.MemberProposedDirectoryData
                .FirstOrDefaultAsync(m => m.MemProId == id);
            if (memberProposedData == null)
            {
                return NotFound();
            }

            return View(memberProposedData);
        }

        // POST: MemberProposedData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MemberProposedDirectoryData == null)
            {
                return Problem("Entity set 'MemberDataContext.MemberProposedDirectoryData'  is null.");
            }
            var memberProposedData = await _context.MemberProposedDirectoryData.FindAsync(id);
            if (memberProposedData != null)
            {
                _context.MemberProposedDirectoryData.Remove(memberProposedData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberProposedDataExists(int id)
        {
            return (_context.MemberProposedDirectoryData?.Any(e => e.MemProId == id)).GetValueOrDefault();
        }





        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //this is for MemberBusinessData Proposed data
        public async Task<IActionResult> BusinessEdit(int? id, int? AuthId)
        {
            if (id == null || _context.MemberProposedDirectoryData == null)
            {
                return RedirectToAction("Index", "Home", new { AuthId = ViewBag.AuthId });
            }

            ViewBag.AuthId = AuthId;

            var memberProposedData = await _context.MemberProposedDirectoryData.Include(m => m.MemberBusinessData).FirstOrDefaultAsync(m => m.MemBusinessId == id);
            memberProposedData.AuthId = AuthId;
            ViewBag.MemBusinessId = id;

            if (memberProposedData == null)
            {
                return RedirectToAction("Index", "Home", new { AuthId = ViewBag.AuthId });
            }
            return View(memberProposedData);
        }

        [HttpPost,ActionName("BusinessEdit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BusinessEdit(int id, [Bind("MemProId,Name,Dob,ResAddress,ResPhone,OfficeNo,Profession,OfficeAddress,MobileNo,AlternateMobileNo,Email,DateofMarriage,NameofSpouse,SpouseDob,ChildName,Address,Country,State,City,PostalCode,AdditonalInfo,AddressType,FirstName,LastName,Relation,HomeAddress,Mobile,FamilyChildName,BusinessName,BusinessDetail,BusinessAddress,BusinessCity,BusinessPostalCode,BusinessEmail,AuthId,MemId,MemBusinessId,MemFamilyId,MemAddressId")] MemberProposedData memberProposedData)
        {
            if (id != memberProposedData.MemBusinessId)
            {
                return NotFound();
            }

            var memberbusinessdata = await _context.MemberBusinessDirectoryData.FirstOrDefaultAsync(m => m.Id == memberProposedData.MemBusinessId);

            memberbusinessdata.BusinessName = memberProposedData.BusinessName;
            memberbusinessdata.BusinessDetail = memberProposedData.BusinessDetail;
            memberbusinessdata.BusinessAddress = memberProposedData.BusinessAddress;
            memberbusinessdata.BusinessCity = memberProposedData.BusinessCity;
            memberbusinessdata.BusinessPostalCode = memberProposedData.BusinessPostalCode;
            memberbusinessdata.BusinessEmail = memberProposedData.BusinessEmail;
            memberbusinessdata.AuthId = memberProposedData.AuthId;

            _context.MemberBusinessDirectoryData.Update(memberbusinessdata);
            var allrecords = _context.MemberProposedDirectoryData.ToList();
            _context.MemberProposedDirectoryData.RemoveRange(allrecords);
            await _context.SaveChangesAsync();

            ViewBag.AuthId = memberProposedData.AuthId;

            return RedirectToAction("Index", "Home", new { AuthId = ViewBag.AuthId });
        }





        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //this is for MemberFamilyData Proposed data
        public async Task<IActionResult> FamilyEdit(int? id, int? AuthId)
        {
            if (id == null || _context.MemberProposedDirectoryData == null)
            {
                return NotFound();
            }

            ViewBag.AuthId = AuthId;

            var memberProposedData = await _context.MemberProposedDirectoryData.Include(m => m.MembersFamilyData).FirstOrDefaultAsync(m => m.MemFamilyId == id);
            //memberProposedData.AuthId = AuthId;
            ViewBag.MemFamilyId = id;

            if (memberProposedData == null)
            {
                return RedirectToAction("Index", "Home", new { AuthId = ViewBag.AuthId });
            }
            return View(memberProposedData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FamilyEdit(int id, int AuthId, [Bind("MemProId,Name,Dob,ResAddress,ResPhone,OfficeNo,Profession,OfficeAddress,MobileNo,AlternateMobileNo,Email,DateofMarriage,NameofSpouse,SpouseDob,ChildName,Address,Country,State,City,PostalCode,AdditonalInfo,AddressType,FirstName,LastName,Relation,HomeAddress,Mobile,FamilyChildName,BusinessName,BusinessDetail,BusinessAddress,BusinessCity,BusinessPostalCode,BusinessEmail,AuthId,MemId,MemBusinessId,MemFamilyId,MemAddressId")] MemberProposedData memberProposedData)
        {
            if (id != memberProposedData.MemFamilyId)
            {
                return NotFound();
            }

            var membersFamilydata = await _context.MemberFamilyDirectoryData.FirstOrDefaultAsync(m => m.Id == memberProposedData.MemFamilyId);

            membersFamilydata.FirstName = memberProposedData.FirstName;
            membersFamilydata.LastName = memberProposedData.LastName;
            membersFamilydata.Relation = memberProposedData.Relation;
            membersFamilydata.HomeAddress = memberProposedData.HomeAddress;
            membersFamilydata.Mobile = memberProposedData.Mobile;
            membersFamilydata.ChildName = memberProposedData.FamilyChildName;
            membersFamilydata.AuthId = memberProposedData.AuthId;

            _context.MemberFamilyDirectoryData.Update(membersFamilydata);
            var allrecords = _context.MemberProposedDirectoryData.ToList();
            _context.MemberProposedDirectoryData.RemoveRange(allrecords);
            await _context.SaveChangesAsync();

            ViewBag.AuthId = memberProposedData.AuthId;

            return RedirectToAction("Index", "Home", new { AuthId = ViewBag.AuthId });
        }




        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //this is for MemberAddressData Proposed data
        public async Task<IActionResult> AddressEdit(int? id, int? AuthId)
        {
            if (id == null || _context.MemberProposedDirectoryData == null)
            {
                return NotFound();
            }

            ViewBag.AuthId = AuthId;

            var memberProposedData = await _context.MemberProposedDirectoryData.Include(m => m.MemberAddressData).FirstOrDefaultAsync(m => m.MemAddressId == id);
           // memberProposedData.AuthId = AuthId;
            ViewBag.MemAddressId = id;

            if (memberProposedData == null)
            {
                return RedirectToAction("Index", "Home", new { AuthId = ViewBag.AuthId });
            }
            return View(memberProposedData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressEdit(int id, int AuthId, [Bind("MemProId,Name,Dob,ResAddress,ResPhone,OfficeNo,Profession,OfficeAddress,MobileNo,AlternateMobileNo,Email,DateofMarriage,NameofSpouse,SpouseDob,ChildName,Address,Country,State,City,PostalCode,AdditonalInfo,AddressType,FirstName,LastName,Relation,HomeAddress,Mobile,FamilyChildName,BusinessName,BusinessDetail,BusinessAddress,BusinessCity,BusinessPostalCode,BusinessEmail,AuthId,MemId,MemBusinessId,MemFamilyId,MemAddressId")] MemberProposedData memberProposedData)
        {
            if (id != memberProposedData.MemAddressId)
            {
                return NotFound();
            }

            var memberAddressdata = await _context.MemberAddressDirectoryData.FirstOrDefaultAsync(m => m.Id == memberProposedData.MemAddressId);

            memberAddressdata.Address = memberProposedData.Address;
            memberAddressdata.Country = memberProposedData.Country;
            memberAddressdata.City = memberProposedData.City;
            memberAddressdata.State = memberProposedData.State;
            memberAddressdata.PostalCode = memberProposedData.PostalCode;
            memberAddressdata.AdditonalInfo = memberProposedData.AdditonalInfo;
            memberAddressdata.AddressType = memberProposedData.AddressType;
            memberAddressdata.AuthId = memberProposedData.AuthId;

            _context.MemberAddressDirectoryData.Update(memberAddressdata);
            var allrecords = _context.MemberProposedDirectoryData.ToList();
            _context.MemberProposedDirectoryData.RemoveRange(allrecords);
            await _context.SaveChangesAsync();

            ViewBag.AuthId = memberProposedData.AuthId;

            return RedirectToAction("Index", "Home", new { AuthId = ViewBag.AuthId });
        }
    }
}
