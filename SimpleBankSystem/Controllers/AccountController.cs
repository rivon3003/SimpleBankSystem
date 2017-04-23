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
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        #endregion Private method

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (_accSer.IsValidAccount(model))
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("InvalidAccount", "Account is invalid");
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
                _accSer.CreateAccount(model);
                return RedirectToAction("Index");
            }
            return View();
        }



        public JsonResult CheckAccNumValid(string accountNumber)
        {
            var result = _accSer.IsAccNumExisted(accountNumber) ? String.Format(Message.AlreadInUse, accountNumber) : Common.TrueStrValJavascriptMatching;
            return Json(result);
        }
    }
}