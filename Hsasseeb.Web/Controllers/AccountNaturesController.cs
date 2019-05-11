using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hasseeb.Application.Domain;
using Hasseeb.Repository;
using Hasseeb.Application.Service;

namespace Hsasseeb.Web.Controllers
{
    public class AccountNaturesController : Controller
    {
        private readonly IAccountNatureManager _accNatureAppService;


        public AccountNaturesController(IAccountNatureManager accNatureAppService)
        {
            _accNatureAppService = accNatureAppService;
        }


        // GET: AccountNatures
        public IActionResult Index()
        {
            return View(_accNatureAppService.GetAll());
        }
       

        // GET: AccountNatures/Details/5
        public IActionResult Details(int id)
        {


            var accountNature = _accNatureAppService.GetID(id);

            if (accountNature == null)
            {
                return NotFound();
            }

            return View(accountNature);
        }

        // GET: AccountNatures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccountNatures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("AccountNatureName,ID")] AccountNature accountNature)
        {
            if (ModelState.IsValid)
            {
                _accNatureAppService.Insert(accountNature);
                return RedirectToAction(nameof(Index));
            }
            return View(accountNature);
        }

        // GET: AccountNatures/Edit/5
        public IActionResult Edit(int id)
        {

            var accountNature = _accNatureAppService.GetID(id);
            if (accountNature == null)
            {
                return NotFound();
            }
            return View(accountNature);
        }

        // POST: AccountNatures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("AccountNatureName,ID")] AccountNature accountNature)
        {
            
            if (ModelState.IsValid)
            {
                    _accNatureAppService.Update(accountNature);
               
                return RedirectToAction(nameof(Index));
            }
            return View(accountNature);
        }

        // GET: AccountNatures/Delete/5
        public IActionResult Delete(int id)
        {
            

            var accountNature = _accNatureAppService.GetID(id);
            if (accountNature == null)
            {
                return NotFound();
            }

            return View(accountNature);
        }

        // POST: AccountNatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _accNatureAppService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool AccountNatureExists(int id)
        //{
        //    return _context.AccountNatures.Any(e => e.ID == id);
        //}
    }
}
