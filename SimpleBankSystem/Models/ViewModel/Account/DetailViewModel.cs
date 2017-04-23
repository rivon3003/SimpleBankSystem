using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankSystem.Models.ViewModel.Account
{
    public class DetailViewModel : BaseAccountWithPasswordViewModel
    {
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }
        public decimal Balance { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
    }
}
