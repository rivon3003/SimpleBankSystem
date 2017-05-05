using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SimpleBankSystem.Models.ViewModel.Account
{
    public class BaseAccountViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^[0-9]{4}$")]
        [Display(Name = "Account Number")]
        [DisplayFormat(DataFormatString = "{0:#### #### #### ####}")]
        [Remote("CheckAccNumValid", "Account")]
        public string AccountNumber { get; set; }

        public string RowVersion { get; set; }
    }
}
