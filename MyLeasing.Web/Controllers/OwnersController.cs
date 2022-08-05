using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Common.Data;
using MyLeasing.Common.Data.Entities;
using MyLeasing.Common.Helperes;
using MyLeasing.Data;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUserHelper _userHelper;

        public OwnersController(
            IOwnerRepository ownerRepository,
            IUserHelper userHelper)
        {
            _ownerRepository = ownerRepository;
            _userHelper = userHelper;
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
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Create
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
                var path = string.Empty;
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\image\\owner",
                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/image/owner/{file}";
                }

                var owner = this.ToOwner(model, path);




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




        private Owner ToOwner (OwnerViewModel model, string path)
        {
            return new Owner
            {
                Id = model.Id,
                ImageUrl = path,
                FirstName = model.FirstName,
                LastName= model.LastName,
                FixPhone = model.FixPhone,
                CellPhone = model.CellPhone,
                Address = model.Address,
                Document = model.Document,
                User = model.User
            };
        }




























        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value); 
            if (owner == null)
            {
                return NotFound();
            }
            var model = this.ToOwnerViewModel(owner);
            return View(model);
        }

        private OwnerViewModel ToOwnerViewModel(Owner owner)
        {
            return new OwnerViewModel
            {
                Id = owner.Id,
                ImageUrl = owner.ImageUrl,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                FixPhone = owner.FixPhone,
                CellPhone = owner.CellPhone,
                Address = owner.Address,
                Document = owner.Document,
                User = owner.User

            };
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
                    var path = model.ImageUrl;
                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";

                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\image\\owner",
                            file);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(stream);
                        }
                        path = $"~/image/owner/{file}";
                    }
                    var owner = this.ToOwner(model, path);


                   


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

                    owner.User = await _userHelper.GetUserByEmailAsync($"{owner.FirstName}.{owner.LastName}@gmail.com"); 
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
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


            user = await _userHelper.GetUserByEmailAsync($"{owner.FirstName}.{owner.LastName}@gmail.com");
            await _userHelper.DeletAsync(user);
            


            return RedirectToAction(nameof(Index));
        }

    }
}
