using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemberDataEntryForm.Models;
using Microsoft.AspNetCore.Authorization;

namespace MemberDataEntryForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly MemberDataContext _context;
        IWebHostEnvironment hostingenvironment;

        public HomeController(MemberDataContext context, IWebHostEnvironment hostingenvironment)
        {
            _context = context;
            this.hostingenvironment = hostingenvironment;
        }

        // GET: Home
        public async Task<IActionResult> Index(int AuthId)
        {
            if(AuthId != null)
            {
                ViewBag.AuthId = AuthId;
            }
            //List<MemberData> memberDataList = await _context.MemberDirectoryData.ToListAsync();
            //var viewModelList = memberDataList.Select(memberData => new MemberViewModel { memberData = memberData }).ToList();
            //return View(viewModelList);  
            return _context.MemberDirectoryData != null ? View(await _context.MemberDirectoryData.ToListAsync()) : Problem("Entity set 'MemberDirectoryContext.MemberDirectoryData'  is null.");
        }


        ////GET
        ////[Authorize(Roles = "Admin")]
        ////[Authorize(Policy = "UserAccess")]
        //public IActionResult Login()
        //{
        //    if (HttpContext.Session.GetString("UserSession") != null)
        //    {
        //        return RedirectToAction("Details");
        //    }
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Login(UserData user)
        //{
        //    var myuser = _context.UserDirectoryData.Where(item => item.Email == user.Email && item.Password == user.Password).FirstOrDefault();
        //    //It checks if a user with matching credentials exists in the database.
        //    if (myuser != null)
        //    {
        //        HttpContext.Session.SetString("UserSession", myuser.Email);  // In this case, the user's email is stored in a session variable named "UserSession."
        //                                                                     // This session variable is used to keep track of whether a user is logged in. Storing the email is a common practice to identify the user in future requests.
        //        return RedirectToAction("Details", new { id = myuser.UserId });
        //    }
        //    else
        //    {
        //        ViewBag.message = "Login Failed...";
        //    }
        //    return View();
        //}

        //public IActionResult Logout()
        //{
        //    if (HttpContext.Session.GetString("UserSession").ToString() != null)
        //    {
        //        HttpContext.Session.Remove("UserSession");
        //        return RedirectToAction("Login");
        //    }
        //    return View();
        //}


        // GET: Home/Details/5
        public async Task<IActionResult> Details(int id, int? AuthId)
        {
            ViewBag.AuthId = AuthId;
            var memberDirectoryDatum = await _context.MemberDirectoryData.FirstOrDefaultAsync(m => m.Id == id);
            memberDirectoryDatum.AuthId = AuthId;

            var userdata = await _context.UserDirectoryData.FirstOrDefaultAsync(m => m.UserId == AuthId);
            if (userdata.UserRoleId == 1)
            {
                ViewBag.msg = "success";
            }
            else if (userdata.UserRoleId == 2)
            {
                ViewBag.Msg = "Success";
            }

            if (memberDirectoryDatum == null)
            {
                return NotFound();
            }

            return View(memberDirectoryDatum);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            var memberViewModel = new MemberViewModel();
            return View(memberViewModel);
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberViewModel memberViewModel)
        {
            //this below code is for inserting image path into SQL server....
            string imagename = "";
            if (memberViewModel.memberImageModel.Photo != null)
            {
                string uploadfolder = Path.Combine(hostingenvironment.WebRootPath, "images");
                imagename = Guid.NewGuid().ToString() + "_" + memberViewModel.memberImageModel.Photo.FileName;
                string filepath = Path.Combine(uploadfolder, imagename);
                memberViewModel.memberImageModel.Photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }
            memberViewModel.memberData.Image = imagename;

            //this below code is for inserting image path into SQL server....
            string signname = "";
            if (memberViewModel.memberImageModel.Signature != null)
            {
                string uploadfolder = Path.Combine(hostingenvironment.WebRootPath, "images");
                signname = Guid.NewGuid().ToString() + "_" + memberViewModel.memberImageModel.Signature.FileName;
                string filepath = Path.Combine(uploadfolder, signname);
                memberViewModel.memberImageModel.Signature.CopyTo(new FileStream(filepath, FileMode.Create));
            }
            memberViewModel.memberData.Sign = signname;

            memberViewModel.memberData.CreatedAt = DateTime.UtcNow;

            //this below code is for saving the MemberData model values onto database and generate the Id value....
            _context.MemberDirectoryData.Add(memberViewModel.memberData);
            await _context.SaveChangesAsync();

            //this below code is for assigning the Id property value into the MemNo fields in all children models....
            memberViewModel.FamilyData.MemNo = memberViewModel.memberData.Id;
            memberViewModel.BusinessData.MemNo = memberViewModel.memberData.Id;
            memberViewModel.AddressData.MemNo = memberViewModel.memberData.Id;

            //this below code is to save data of all 3 child model tables....
            if (memberViewModel.FamilyData.MemNo != null && memberViewModel.BusinessData.MemNo != null && memberViewModel.AddressData.MemNo != null)
            {
                _context.AddRange(memberViewModel.FamilyData, memberViewModel.BusinessData, memberViewModel.AddressData);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewData["ErrorCreating"] = "Only MemberData is successfully saved!! Please complete all remaining forms...";
            return RedirectToAction("Create");
        }



        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int id, int AuthId)
        {
            ViewBag.AuthId = AuthId;

            var userdata = await _context.UserDirectoryData.FirstOrDefaultAsync(m => m.UserId == AuthId);
            if(userdata.UserRoleId == 1)
            {
                ViewBag.msg = "Success";
            }

            if (id <= 0  || _context.MemberDirectoryData == null)
            {
                return NotFound();
            }

            var memberDirectoryDatum = await _context.MemberDirectoryData.FindAsync(id);

            if (memberDirectoryDatum == null)
            {
                return NotFound();
            }
            return View(memberDirectoryDatum);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Dob,ResAddress,ResPhone,OfficeNo,Profession,OfficeAddress,MobileNo,AlternateMobileNo,Email,DateofMarriage,NameofSpouse,SpouseDob,ChildName,AuthId,Image,Sign,CreatedAt")] MemberData memberDirectoryDatum, IFormFile newPhoto, IFormFile newSignature)
        {
            if (id != memberDirectoryDatum.Id)
            {
                return NotFound();
            }

            var userdata = await _context.UserDirectoryData.FirstOrDefaultAsync(m => m.UserId == memberDirectoryDatum.AuthId);

            if (userdata.UserRoleId == 2)
            {
                var allrecords = _context.MemberProposedDirectoryData.ToList();
                _context.MemberProposedDirectoryData.RemoveRange(allrecords);

                MemberProposedData proposedData = new MemberProposedData
                {
                    Name = memberDirectoryDatum.Name,
                    Dob = memberDirectoryDatum.Dob,
                    ResAddress = memberDirectoryDatum.ResAddress,
                    ResPhone = memberDirectoryDatum.ResPhone,
                    OfficeAddress = memberDirectoryDatum.OfficeAddress,
                    OfficeNo = memberDirectoryDatum.OfficeNo,
                    Profession = memberDirectoryDatum.Profession,
                    MobileNo = memberDirectoryDatum.MobileNo,
                    AlternateMobileNo = memberDirectoryDatum.AlternateMobileNo,
                    Email = memberDirectoryDatum.Email,
                    DateofMarriage = memberDirectoryDatum.DateofMarriage,
                    NameofSpouse = memberDirectoryDatum.NameofSpouse,
                    SpouseDob = memberDirectoryDatum.SpouseDob,
                    ChildName = memberDirectoryDatum.ChildName,
                    AuthId = memberDirectoryDatum.AuthId,
                    MemId = memberDirectoryDatum.Id
                };

                try
                {
                    var existingData = await _context.MemberDirectoryData.FindAsync(id);

                    if (newPhoto != null)
                    {
                        // Upload new photo and update image property
                        string imagename = Guid.NewGuid().ToString() + "_" + newPhoto.FileName;
                        string uploadfolder = Path.Combine(hostingenvironment.WebRootPath, "images");
                        string filepath = Path.Combine(uploadfolder, imagename);
                        newPhoto.CopyTo(new FileStream(filepath, FileMode.Create));
                        existingData.Image = imagename;
                    }

                    if (newSignature != null)
                    {
                        // Upload new signature and update sign property
                        string signname = Guid.NewGuid().ToString() + "_" + newSignature.FileName;
                        string uploadfolder = Path.Combine(hostingenvironment.WebRootPath, "images");
                        string filepath = Path.Combine(uploadfolder, signname);
                        newSignature.CopyTo(new FileStream(filepath, FileMode.Create));
                        existingData.Sign = signname;
                    }

                    memberDirectoryDatum.Image = existingData.Image;
                    memberDirectoryDatum.Sign = existingData.Sign;

                    // Detach the existing entity if it's sahring the same Id(Primary Key)...
                    if (existingData != null)
                    {
                        _context.Entry(existingData).State = EntityState.Detached;
                    }

                    _context.Update(proposedData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberDirectoryDatumExists(memberDirectoryDatum.Id))
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
                var allrecords = _context.MemberProposedDirectoryData.ToList();
                _context.MemberProposedDirectoryData.RemoveRange(allrecords);
                try
                {
                    var existingData = await _context.MemberDirectoryData.FindAsync(id);

                    if (newPhoto != null)
                    {
                        // Upload new photo and update image property
                        string imagename = Guid.NewGuid().ToString() + "_" + newPhoto.FileName;
                        string uploadfolder = Path.Combine(hostingenvironment.WebRootPath, "images");
                        string filepath = Path.Combine(uploadfolder, imagename);
                        newPhoto.CopyTo(new FileStream(filepath, FileMode.Create));
                        existingData.Image = imagename;
                    }

                    if (newSignature != null)
                    {
                        // Upload new signature and update sign property
                        string signname = Guid.NewGuid().ToString() + "_" + newSignature.FileName;
                        string uploadfolder = Path.Combine(hostingenvironment.WebRootPath, "images");
                        string filepath = Path.Combine(uploadfolder, signname);
                        newSignature.CopyTo(new FileStream(filepath, FileMode.Create));
                        existingData.Sign = signname;
                    }

                    memberDirectoryDatum.Image = existingData.Image;
                    memberDirectoryDatum.Sign = existingData.Sign;

                    // Detach the existing entity if it's sahring the same Id(Primary Key)...
                    if (existingData != null)
                    {
                        _context.Entry(existingData).State = EntityState.Detached;
                    }

                    _context.Update(memberDirectoryDatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberDirectoryDatumExists(memberDirectoryDatum.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.AuthId = memberDirectoryDatum.AuthId;
            return RedirectToAction("Index", new { AuthId = ViewBag.AuthId });
        }




        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int id, int AdminUserId, int FrontDeskUserId)
        {
            if (AdminUserId != null)
            {
                ViewBag.AdminUserId = AdminUserId;
            }
            else if (FrontDeskUserId != null)
            {
                ViewBag.FrontDeskUserId = FrontDeskUserId;
            }

            if (id < 0 || _context.MemberDirectoryData == null)
            {
                return NotFound();
            }

            var memberDirectoryDatum = await _context.MemberDirectoryData.FirstOrDefaultAsync(m => m.Id == id);
            if (memberDirectoryDatum == null)
            {
                return NotFound();
            }

            return View(memberDirectoryDatum);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MemberDirectoryData == null)
            {
                return Problem("Entity set 'MemberDirectoryContext.MemberDirectoryData'  is null.");
            }
            var memberDirectoryDatum = await _context.MemberDirectoryData.FirstOrDefaultAsync(m => m.Id == id);

            var memNo = memberDirectoryDatum.Id;

            if (memberDirectoryDatum != null)
            {
                _context.MemberDirectoryData.Remove(memberDirectoryDatum);
            }

            // Delete MemberBusinessData
            var memberBusinessData = await _context.MemberBusinessDirectoryData.FirstOrDefaultAsync(m => m.MemNo == memNo);
            if (memberBusinessData != null)
            {
                _context.MemberBusinessDirectoryData.Remove(memberBusinessData);
            }

            // Delete MemberFamilyData
            var memberFamilyData = await _context.MemberFamilyDirectoryData.FirstOrDefaultAsync(m => m.MemNo == memNo);
            if (memberFamilyData != null)
            {
                _context.MemberFamilyDirectoryData.Remove(memberFamilyData);
            }

            //var image = memberDirectoryDatum.Image;

            //if (!string.IsNullOrEmpty(image))
            //{
            //    string imagePath = Path.Combine(hostingenvironment.WebRootPath, "images", image);

            //    if (System.IO.File.Exists(imagePath))
            //    {
            //        System.IO.File.Delete(imagePath);
            //    }
            //}

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MemberDirectoryDatumExists(int id)
        {
            return (_context.MemberDirectoryData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
