using System.ComponentModel.DataAnnotations;

namespace SimpleBankSystem.Models.ViewModel.Account
{
    public class RegisterViewModel
    {
        [Required(AllowEmptyStrings = false)]

        [RegularExpression(@"^[0-9]{4}$")]
        [Display(Name = "Account Number")]
        [DisplayFormat(DataFormatString = "{0:#### #### #### ####}")]
        public string AccountNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[^\s\,]+$")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password), Compare(nameof(Password))]
        [Display(Name = "Re-type password")]
        public string ConfirmPass { get; set; }
    }
}
