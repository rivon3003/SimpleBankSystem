using Microsoft.AspNetCore.Mvc;
using SimpleBankSystem.Constants.Web;
using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleBankSystem.Models.ViewModel.Account
{
    public class RegisterViewModel : BaseAccountWithPasswordViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password), Compare(nameof(Password))]
        [Display(Name = "Re-type password")]
        public string ConfirmPass { get; set; }
    }
}
