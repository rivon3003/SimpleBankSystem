using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SimpleBankSystem.Models.ViewModel.Account
{
    public class LoginViewModel: BaseAccountWithPasswordViewModel
    {
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
