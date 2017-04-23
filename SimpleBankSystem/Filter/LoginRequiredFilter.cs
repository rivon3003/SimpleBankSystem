using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBankSystem.Extension;
using SimpleBankSystem.Models.ViewModel.Account;
using SimpleBankSystem.Constants.Web;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBankSystem.Filter
{
    public class LogInRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetObjectFromJson<LoginViewModel>(SessionName.LoggedAccount) != null) return;

            context.Result = new RedirectResult("/Account/Login?ReturnUrl=" + Uri.EscapeDataString(context.HttpContext.Request.Path));
        }
    }
}
