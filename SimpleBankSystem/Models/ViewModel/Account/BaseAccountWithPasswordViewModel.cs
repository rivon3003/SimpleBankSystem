using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankSystem.Models.ViewModel.Account
{
    public class BaseAccountWithPasswordViewModel : BaseAccountViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[^\s\,]+$")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
