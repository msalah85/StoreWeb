using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hasseeb.Application.Domain;
using Hsasseeb.Web.Data;
using Hasseeb.Application.Service;
using Hsasseeb.Web.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using JqueryDataTables.ServerSide.AspNetCoreWeb;

namespace Hsasseeb.Web.Controllers
{
    public class AccountsController : Controller
    {

        private readonly IAccountManager _accountAppService;
        private readonly IAccountNatureManager _accNatureAppService;


        public AccountsController(IAccountManager accountAppService, IAccountNatureManager accNatureAppService)
        {
            _accountAppService = accountAppService;
            _accNatureAppService = accNatureAppService;
        }

        #region SPA Accounts 

        public ActionResult Accounts()
        {
            return View();
        }

        public async Task<IActionResult> GetAccounts(DTParameters param) // [FromBody]
        {
            var data = await _accountAppService.GetAllTable(param);

            int draw = param != null ? param.Draw : 1;

            var results = new JsonResult(new DTResult<Account>
            {
                draw = draw,
                data = data,
                recordsFiltered = data.Count(),
                recordsTotal = data.Count()
            });


            return results;
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //    return new JsonResult(new { error = "Thres error" });
            //}


            /*
             check this link for more information
            https://www.c-sharpcorner.com/article/jquery-datatables-with-asp-net-core-server-side-dynamic-multiple-column-searchin/
            **/

            // // You have to return data frmatted for datatable controls
            // search for any sample: datatables.net with MVC 
        }

        [HttpGet]
        public ActionResult Save(int ID)
        {
            ViewData["AccountNatureID"] = new SelectList(_accNatureAppService.GetAll().ToList(), "ID", "AccountNatureName");
            ViewData["AccountParentID"] = new SelectList(_accountAppService.GetAll().ToList(), "ID", "AccountName");

            return View();
        }
        [HttpPost]
        public ActionResult Save(Account account)
        {
            var newAccount = _accountAppService.GetID(account.ID);
            if (ModelState.IsValid)
            {

                if (newAccount != null)
                {
                    _accountAppService.Update(account);

                }
                else
                {
                    _accountAppService.Insert(account);
                }

            }
            ViewData["AccountNatureID"] = new SelectList(_accNatureAppService.GetAll().ToList(), "ID", "AccountNatureName", account.AccountNature);
            ViewData["AccountParentID"] = new SelectList(_accountAppService.GetAll().ToList(), "ID", "AccountName", account.ParentAccount);

            return View(account);
        }
        [HttpGet]
        public ActionResult SPADelete(int ID)
        {

            var account = _accountAppService.GetID(ID);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }
        [HttpPost]
        [ActionName("SPADelete")]
        public ActionResult DeleteAccount(int ID)
        {
            bool status = false;

            _accountAppService.Delete(ID);
            return Json(new { status = status });

        }
        #endregion

        public ActionResult OnDemandTree()
        {
            var geatAll = _accountAppService.GetAll();

            List<Account> Parent = new List<Account>();
            foreach (var item in geatAll)
            {
                Account viewPar = new Account();
                if (item.ParentAccount == null || item.ParentAccount == 0)
                {
                    viewPar.AccountDesc = item.AccountDesc;
                    viewPar.ID = item.ID;
                    viewPar.AccountName = item.AccountName;
                    viewPar.AccountNature = item.AccountNature;
                    viewPar.AccountSerial = item.AccountSerial;
                    viewPar.Active = item.Active;
                    viewPar.AddDate = item.AddDate;
                    viewPar.GroupOrder = item.GroupOrder;
                    viewPar.IsMain = item.IsMain;
                    viewPar.ParentAccount = item.ParentAccount;
                    Parent.Add(viewPar);

                }

            }
            return View(Parent);
        }


        public JsonResult GetSubMenu(string pid)
        {
            System.Threading.Thread.Sleep(5000);
            var geatAll = _accountAppService.GetAll();
            List<Account> Child = new List<Account>();
            int pID = 0;
            int.TryParse(pid, out pID);

            foreach (var item in geatAll)
            {
                Account viewPar = new Account();
                if (item.ParentAccount == pID)
                {
                    viewPar.AccountDesc = item.AccountDesc;
                    viewPar.ID = item.ID;
                    viewPar.AccountName = item.AccountName;
                    viewPar.AccountNature = item.AccountNature;
                    viewPar.AccountSerial = item.AccountSerial;
                    viewPar.Active = item.Active;
                    viewPar.AddDate = item.AddDate;
                    viewPar.GroupOrder = item.GroupOrder;
                    viewPar.IsMain = item.IsMain;
                    viewPar.ParentAccount = item.ParentAccount;
                    Child.Add(viewPar);
                }
            }

            return Json(Child);
            ////return new JsonResult { Data = Child, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #region region


        // GET: Accounts
        public IActionResult Index()
        {
            //var applicationDbContext = _accountAppService.GetAll();
            return View();
        }

        //// GET: Accounts/Details/5
        public IActionResult Details(int id)
        {


            var account = _accountAppService.GetID(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        //// GET: Accounts/Create
        public IActionResult Create()
        {
            ViewData["AccountNatureID"] = new SelectList(_accNatureAppService.GetAll().ToList(), "ID", "AccountNatureName");
            return View();
        }

        //// POST: Accounts/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("AccountNatureID,ParentAccountID,AccountSerial,AccountName,AccountDesc,GroupOrder,Active,AddDate,IsMain,ID")] Account account)
        {
            if (ModelState.IsValid)
            {
                _accountAppService.Insert(account);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountNatureID"] = new SelectList(_accNatureAppService.GetAll().ToList(), "ID", "AccountNatureName", account.AccountNature);
            return View(account);
        }

        //// GET: Accounts/Edit/5
        public IActionResult Edit(int id)
        {


            var account = _accountAppService.GetID(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["AccountNatureID"] = new SelectList(_accNatureAppService.GetAll().ToList(), "ID", "AccountNatureName", account.AccountNature);
            return View(account);
        }

        //// POST: Accounts/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("AccountNatureID,ParentAccountID,AccountSerial,AccountName,AccountDesc,GroupOrder,Active,AddDate,IsMain,ID")] Account account)
        {
            if (id != account.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _accountAppService.Update(account);

                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountNatureID"] = new SelectList(_accNatureAppService.GetAll(), "ID", "AccountNatureName", account.AccountNature);
            return View(account);
        }

        //// GET: Accounts/Delete/5
        public IActionResult Delete(int id)
        {


            var account = _accountAppService.GetID(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        //// POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _accountAppService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool AccountExists(int id)
        //{
        //    return _context.Accounts.Any(e => e.ID == id);
        //}
        #endregion
    }
}
