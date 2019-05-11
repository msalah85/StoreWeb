using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hasseeb.Application.Domain;
using Hasseeb.Application.Service;
using Hasseeb.Application.ViewModels;
using Hasseeb.Repository;
using Hasseeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;

namespace Hsasseeb.Web.Controllers
{
    public class SPAAccountsController : Controller
    {

        private readonly MyContext _ctx;
        private readonly IAccountManager _accountAppService;
        private readonly IAccountNatureManager _accNatureAppService;


    public SPAAccountsController(IAccountManager accountAppService, IAccountNatureManager accNatureAppService , MyContext ctx)
    {
        _accountAppService = accountAppService;
        _accNatureAppService = accNatureAppService;
            _ctx = ctx;
    }
    public IActionResult Index()
        {
            var geatAll = _accNatureAppService.GetAll();

             
            return View(geatAll);
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
                if (item.AccountNature == pID)
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
        public JsonResult AccountNatures()
        {
            return Json(_accNatureAppService.GetAll());
        }

        #region SPA Accounts 

        public IActionResult GetAccounts(int pageNumber = 1, int pageSize = 20)
        {
            var list = _accountAppService.GetAll();

            var pagedData = Pagination.PagedResult(list, pageNumber, pageSize);
            return Json(pagedData);

        }

        public JsonResult AllAccounts()
        {
            var list = _accountAppService.GetAll();

            return Json(list);

        }

        public IActionResult GetAccountByID(int id)
        {
            var account = _accountAppService.GetID(id);
            var ParentAccount = new Account();
            var accountNature = new AccountNature();
            if (account.ParentAccount != null)
            {
                 ParentAccount = _accountAppService.GetID((int)account.ParentAccount);
            }
            if (account.AccountNature != null)
            {
                accountNature = _accNatureAppService.GetID((int)account.AccountNature);
            }
            AccountViewModel model = new AccountViewModel();
            model.ID = account.ID;
            model.AccountDesc = account.AccountDesc;
            model.AccountName = account.AccountName;
            model.AccountNatureID = account.AccountNature;
            model.AccountNatureName = accountNature.AccountNatureName;
            model.AccountSerial = account.AccountSerial;
            model.Active = account.Active;
            model.GroupOrder = account.GroupOrder;
            model.IsMain = account.IsMain;
            model.ParentAccountID = account.ParentAccount;
            model.ParentAccountName = ParentAccount.AccountName;
            return Json(model);

        }

      
        [HttpPost]
        public JsonResult Save(Account item)
        {
            var newAccount = _accountAppService.GetID(item.ID);
            
         
                if (newAccount == null)
                {
                    _accountAppService.Insert(item);


                }
                else
                {


                Account viewPar = _ctx.Accounts.FirstOrDefault(F => F.ID == item.ID);

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
                _ctx.SaveChanges();


            }

            
           
            return Json(item);
        }
        
        [HttpPost]
        public JsonResult DeleteAccount(int ID)
        {
            bool status = false;

           status = _accountAppService.Delete(ID);
            return Json(new { status = status });

        }


        public IActionResult GetAccountNatureByID(int id)
        {
            var account = _accNatureAppService.GetID(id);
            
            
            return Json(account);

        }


        [HttpPost]
        public JsonResult SaveAccountNature(AccountNature item)
        {
            var newAccount = _accNatureAppService.GetID(item.ID);


            if (newAccount == null)
            {
                _accNatureAppService.Insert(item);


            }
            else
            {
                AccountNature viewPar = _ctx.AccountNatures.FirstOrDefault(F => F.ID == item.ID);

                viewPar.AccountNatureName = item.AccountNatureName;
                viewPar.ID = item.ID;
               
                _ctx.SaveChanges();
            }



            return Json(item);
        }

        [HttpPost]
        public JsonResult DeleteAccountNature(int ID)
        {
            bool status = false;

            status = _accNatureAppService.Delete(ID);
            return Json(new { status = status });

        }


        #endregion
    }
}