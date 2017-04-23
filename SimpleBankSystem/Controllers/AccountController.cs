using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBankSystem.Models.ViewModel.Account;
using SimpleBankSystem.Services.Infrastructure;
using SimpleBankSystem.Constants.Web;
using SimpleBankSystem.Constants.Value;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SimpleBankSystem.Extension;
using SimpleBankSystem.Filter;

namespace SimpleBankSystem.Controllers
{
    public class AccountController : Controller
    {
        #region Contructor and Properties

        private IAccountService _accSer;

        public AccountController(IAccountService accSer)
        {
            _accSer = accSer;
        }

        #endregion Contructor and Properties

        #region Private method

        #endregion Private method

        [LogInRequired]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.SetObjectAsJson(SessionName.LoggedAccount, null);
            return RedirectToAction("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (_accSer.IsValidAccount(model))
            {
                HttpContext.Session.SetObjectAsJson(SessionName.LoggedAccount, new LoginViewModel { AccountNumber = model.AccountNumber });
                return RedirectToAction("Detail");
            }

            ModelState.AddModelError(ErrorList.InvalidAccount.Key, ErrorList.InvalidAccount.Message);
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                HttpContext.Session.SetObjectAsJson(SessionName.LoggedAccount, new LoginViewModel { AccountNumber = model.AccountNumber });
                _accSer.CreateAccount(model);
                return RedirectToAction("Detail");
            }
            return View();
        }

        [LogInRequired]
        public JsonResult CheckAccNumValid(string accountNumber)
        {
            var result = _accSer.IsAccNumExisted(accountNumber) ? String.Format(Message.AlreadInUse, accountNumber) : Common.TrueStrValJavascriptMatching;
            return Json(result);
        }

        [LogInRequired]
        public IActionResult Detail()
        {
            var curAccNum = HttpContext.Session.GetObjectFromJson<LoginViewModel>(SessionName.LoggedAccount).AccountNumber;
            var model = _accSer.GetAccountDetail(curAccNum);
            return View(model);
        }

        [HttpPost]
        [LogInRequired]
        public JsonResult Deposit([FromBody] DepositViewModel model)
        {
            model.AccountNumber = HttpContext.Session.GetObjectFromJson<LoginViewModel>(SessionName.LoggedAccount).AccountNumber;
            return Json(_accSer.Deposit(model));
        }

        [HttpPost]
        [LogInRequired]
        public JsonResult Withdraw([FromBody] WithdrawViewModel model)
        {
            model.AccountNumber = HttpContext.Session.GetObjectFromJson<LoginViewModel>(SessionName.LoggedAccount).AccountNumber;
            return Json(_accSer.Withdraw(model));
        }

        [HttpPost]
        [LogInRequired]
        public JsonResult Transfer([FromBody] TransferViewModel model)
        {
            model.AccountNumber = HttpContext.Session.GetObjectFromJson<LoginViewModel>(SessionName.LoggedAccount).AccountNumber;
            return Json(_accSer.Transfer(model));
        }
    }
}