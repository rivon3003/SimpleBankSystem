using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankSystem.Constants.Web
{
    public class Error
    {
        public string Key { get; set; }
        public string Message { get; set; }
    }

    public static class ErrorList
    {
        public static Error InvalidAccount = new Error { Key = "InvalidAccount", Message = "Account is invalid" };
    }
}
