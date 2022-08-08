using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Common.Data;
using MyLeasing.Common.Data.Entities;
using MyLeasing.Common.Helperes;
using MyLeasing.Common.Models;

namespace MyLeasing.Web.Controllers
{
    public class LesseesController : Controller
    {

        private readonly ILesseeRepository _lesseeRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;
        public LesseesController(
            ILesseeRepository lesseeRepository,
            IUserHelper userHelper,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)
        {
            _lesseeRepository = lesseeRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;

        }

        // GET: Lessees
        public IActionResult Index()
        {
            return View(_lesseeRepository.GetAll().OrderBy(l => l.FirstName));
        }

        // GET: Lessees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _lesseeRepository.GetByIdAsync(id.Value);
            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        // GET: Lessees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lessees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LesseeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "lessee");
                }
                var lessee = _converterHelper.ToLessee(model, path, true);




                var user = new User
                {
                    Document = lessee.Document.ToString(),
                    FirstName = lessee.FirstName,
                    LastName = lessee.LastName,
                    Address = lessee.Address,
                    Email = lessee.FirstName + "." + lessee.LastName + "@gmail.com",
                    UserName = lessee.FirstName + "." + lessee.LastName + "@gmail.com",
                    PhoneNumber = lessee.FixPhone.ToString()
                };

                await _userHelper.AddUserAsync(user, "123456");

                lessee.User = user;










                await _lesseeRepository.CreateAsync(lessee);
                
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Lessees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _lesseeRepository.GetByIdAsync(id.Value);
            if (lessee == null)
            {
                return NotFound();
            }
            var model = _converterHelper.ToLesseeViewModel(lessee);
            return View(model);
        }

        // POST: Lessees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LesseeViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.PhotoUrl;
                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "lessee");
                    }
                    var lessee = _converterHelper.ToLessee(model, path, false);





                    var user = new User
                    {
                        Document = lessee.Document.ToString(),
                        FirstName = lessee.FirstName,
                        LastName = lessee.LastName,
                        Address = lessee.Address,
                        Email = lessee.FirstName + "." + lessee.LastName + "@gmail.com",
                        UserName = lessee.FirstName + "." + lessee.LastName + "@gmail.com",
                        PhoneNumber = lessee.FixPhone.ToString()
                    };

                    await _userHelper.AddUserAsync(user, "123456");

                    lessee.User = await _userHelper.GetUserByEmailAsync($"{lessee.FirstName}.{lessee.LastName}@gmail.com");




                    await _lesseeRepository.UpdateAsync(lessee);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _lesseeRepository.ExistAsync(model.Id))
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

        // GET: Lessees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _lesseeRepository.GetByIdAsync(id.Value);

            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        // POST: Lessees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, User user)
        {
            var lessee = await _lesseeRepository.GetByIdAsync(id);
            await _lesseeRepository.DeletAsync(lessee);





            user = await _userHelper.GetUserByEmailAsync($"{lessee.FirstName}.{lessee.LastName}@gmail.com");
            await _userHelper.DeletAsync(user);







            return RedirectToAction(nameof(Index));
        }

    }
}
