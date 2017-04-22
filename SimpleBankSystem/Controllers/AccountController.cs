using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBankSystem.Models.ViewModel.Account;
using SimpleBankSystem.Services.Infrastructure;

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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
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
            }
            return View();
        }
    }
}