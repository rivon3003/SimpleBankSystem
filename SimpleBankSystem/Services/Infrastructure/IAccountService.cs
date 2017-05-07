using SimpleBankSystem.Models.ResponseModel.Account;
using SimpleBankSystem.Models.ViewModel.Account;
using System.Threading.Tasks;

namespace SimpleBankSystem.Services.Infrastructure
{
    public interface IAccountService
    {
        void CreateAccount(RegisterViewModel account);
        bool IsAccNumExisted(string accNum);
        bool IsValidAccount(LoginViewModel model);
        DetailViewModel GetAccountDetail(string accNum);
        Task<DetailViewModel> GetAccountDetailAsync(string accNum);
        TransactionResponseModel Deposit(DepositViewModel model);
        Task<TransactionResponseModel> DepositAsync(DepositViewModel model);
        TransactionResponseModel Withdraw(WithdrawViewModel model);
        Task<TransactionResponseModel> WithdrawAsync(WithdrawViewModel model);
        TransactionResponseModel Transfer(TransferViewModel model);
        Task<TransactionResponseModel> TransferAsync(TransferViewModel model);
    }
}
