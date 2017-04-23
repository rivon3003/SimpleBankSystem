using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SimpleBankSystem.Models.ViewModel.Account
{
    public class LoginViewModel
    {
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
