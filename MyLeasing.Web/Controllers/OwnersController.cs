using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Common.Data;
using MyLeasing.Common.Data.Entities;
using MyLeasing.Common.Helperes;
using MyLeasing.Common.Models;
using MyLeasing.Data;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Controllers
{
    
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;

        public OwnersController(
            IOwnerRepository ownerRepository,
            IUserHelper userHelper,
            IBlobHelper blobHelper,
            IConverterHelper converterHelper)
        {
            _ownerRepository = ownerRepository;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
        }

        // GET: Owners
        public IActionResult Index()
        {
            return View(_ownerRepository.GetAll().OrderBy(p => p.FirstName));
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                //return new NotFoundViewResult("OwnerNotFound");
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                //return new NotFoundViewResult("OwnerNotFound");
            }

            return View(owner);
        }

        // GET: Owners/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "owners"); 
                }
                var owner = _converterHelper.ToOwner(model, imageId, true);
             




                //TODO: Modificar para o user que esta logado

                var user = new User
                {
                    Document = owner.Document.ToString(),
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Address = owner.Address,
                    Email = owner.FirstName + "." + owner.LastName + "@gmail.com",
                    UserName = owner.FirstName + "." + owner.LastName + "@gmail.com",
                    PhoneNumber = owner.FixPhone.ToString()
                };

                await _userHelper.AddUserAsync(user, "123456");

                owner.User = user;

                await _ownerRepository.CreateAsync(owner); 
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }




        // GET: Owners/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                //return new NotFoundViewResult("OwnerNotFound");
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value); 
            if (owner == null)
            {
                //return new NotFoundViewResult("OwnerNotFound");
            }
            var model = _converterHelper.ToOwnerViewModel(owner);
            return View(model);
        }

       

        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OwnerViewModel model)
        {
            if (ModelState.IsValid)
            {    
                try
                {
                    Guid imageId = model.ImageId;
                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "owners"); 
                    }
                    var owner = _converterHelper.ToOwner(model, imageId, false);




                    var user = new User
                    {
                        Document = owner.Document.ToString(),
                        FirstName = owner.FirstName,
                        LastName = owner.LastName,
                        Address = owner.Address,
                        Email = owner.FirstName + "." + owner.LastName + "@gmail.com",
                        UserName = owner.FirstName + "." + owner.LastName + "@gmail.com",
                        PhoneNumber = owner.FixPhone.ToString()
                    };

                    await _userHelper.AddUserAsync(user, "123456");

                    owner.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name); 
                    await _ownerRepository.UpdateAsync(owner);



                   
                    //TODO: Modificar para o user que esta logado



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _ownerRepository.ExistAsync(model.Id))
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
            return View(model);
        }

































        // GET: Owners/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                //return new NotFoundViewResult("OwnerNotFound");
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                //return new NotFoundViewResult("OwnerNotFound");
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, User user)
        {
            var owner = await _ownerRepository.GetByIdAsync(id);
            await _ownerRepository.DeletAsync(owner);
            
            //TODO:???????


            user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            await _userHelper.DeletAsync(user);
            


            return RedirectToAction(nameof(Index));
        }










        public IActionResult OwnerNotFound()
        {
            return View();
        }
    }
}
