using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankSystem.Constants.Web
{
    public static class Message
    {
        public static string AlreadInUse = "{0} is already in use";
        public static string AccountIsInvalid = "This account is invalid.";
        public static string BalanceIsNotEnough = "The balance is not enough to execute.";
        public static string SystemError = "System error. Please contact admin to know detail.";
        public static string SuccessfulTracsaction = "The transaction is successfull.";
        public static string ErrorWithDepositTransaction = "Error occurs in deposit process.";
        public static string ErrorWithWithdrawTransaction = "Error occurs in withdraw process.";
        public static string RedundantTransaction = "Target account is the current acount. it is a redundant transaction.";
        public static string NoFound = "Data is not found";
        public static string AccountNotFound = "The account is not found";
        public static string TargetNotExisted = "Target account is not existed";
    }
}
