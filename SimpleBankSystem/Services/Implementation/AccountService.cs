using SimpleBankSystem.Models.Entity;
using SimpleBankSystem.Models.ViewModel.Account;
using SimpleBankSystem.Repository.Infrastructure;
using SimpleBankSystem.Services.Infrastructure;
using SimpleBankSystem.Models.ResponseModel.Account;
using SimpleBankSystem.Constants.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;

namespace SimpleBankSystem.Services.Implementation
{
    public class AccountService : IAccountService
    {
        #region Contructor and Properties

        private readonly IUnitOfWorkFactory<IUnitOfWork> _unitOfWorkFactory;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWorkFactory<IUnitOfWork> unitOfWorkFactory, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        #endregion Contructor and Properties

        #region Private method

        private TransactionResponseModel Deposit(DepositViewModel model, IUnitOfWork unit)
        {
            var failureResponse = new TransactionResponseModel
            {
                IsSuccessful = false
            };

            var account = unit.Repository<Account>().Get().SingleOrDefault(a => a.AccountNumber == model.AccountNumber);

            if (account != null)
            {
                if (model.Amount < decimal.MaxValue - account.Balance)
                {
                    account.Balance += model.Amount;
                    var entity = unit.Repository<Account>().Attach(account);
                    return new TransactionResponseModel
                    {
                        IsSuccessful = true,
                        CurrentBalance = entity.Balance,
                        Message = Message.SuccessfulTracsaction
                    };
                }
                failureResponse.Message = Message.SystemError;
                return failureResponse;
            }

            failureResponse.Message = Message.AccountIsInvalid;
            return failureResponse;
        }

        private async Task<TransactionResponseModel> DepositAsync(DepositViewModel model, IUnitOfWork unit)
        {
            var account = await unit.Repository<Account>().Get().SingleOrDefaultAsync(a => a.AccountNumber == model.AccountNumber);
            var currentRowVerStr = Convert.ToBase64String(account.RowVersion);

            var failureResponse = new TransactionResponseModel
            {
                IsSuccessful = false,
                RowVersion = currentRowVerStr
            };

            var passRowVer = Convert.FromBase64String(model.RowVersion);
            var prop = unit.GetContext().Entry(account).Property(a => a.RowVersion);
            prop.CurrentValue = passRowVer;
            prop.OriginalValue = passRowVer;

            if (account != null)
            {
                if (model.Amount < decimal.MaxValue - account.Balance)
                {
                    account.Balance += model.Amount;
                    var entity = unit.Repository<Account>().Attach(account);
                    unit.GetContext().Entry(account).State = EntityState.Modified;

                    return new TransactionResponseModel
                    {
                        IsSuccessful = true,
                        CurrentBalance = entity.Balance,
                        Message = Message.SuccessfulTracsaction,
                        RowVersion = currentRowVerStr
                    };
                }
                failureResponse.Message = Message.SystemError;
                return failureResponse;
            }

            failureResponse.Message = Message.AccountIsInvalid;
            return failureResponse;
        }

        private TransactionResponseModel Withdraw(WithdrawViewModel model, IUnitOfWork unit)
        {
            var failureResponse = new TransactionResponseModel
            {
                IsSuccessful = false
            };

            var account = unit.Repository<Account>().Get().FirstOrDefault(a => a.AccountNumber == model.AccountNumber);
            if (account != null)
            {
                if (account.Balance >= model.Amount)
                {
                    account.Balance -= model.Amount;
                    var entity = unit.Repository<Account>().Attach(account);
                    return new TransactionResponseModel
                    {
                        IsSuccessful = true,
                        CurrentBalance = entity.Balance,
                        Message = Message.SuccessfulTracsaction
                    };
                }
                failureResponse.Message = Message.BalanceIsNotEnough;
                return failureResponse;
            }

            failureResponse.Message = Message.AccountIsInvalid;
            return failureResponse;
        }

        #endregion Private method
        public void CreateAccount(RegisterViewModel registerVm)
        {
            using (var unit = _unitOfWorkFactory.Create())
            {
                var account = _mapper.Map<RegisterViewModel, Account>(registerVm);
                unit.Repository<Account>().Insert(account);
                unit.Save();
            }
        }

        public bool IsAccNumExisted(string accNum)
        {
            using (var unit = _unitOfWorkFactory.Create())
            {
                return unit.Repository<Account>().Get().Any(a => a.AccountNumber == accNum);
            }
        }

        public bool IsValidAccount(LoginViewModel model)
        {
            using (var unit = _unitOfWorkFactory.Create())
            {
                return unit.Repository<Account>().Get().Any(a => a.AccountNumber == model.AccountNumber && a.Password == model.Password);
            }
        }

        public DetailViewModel GetAccountDetail(string accNum)
        {
            using (var unit = _unitOfWorkFactory.Create())
            {
                var data = unit.Repository<Account>().Get().FirstOrDefault(a => a.AccountNumber == accNum);
                return _mapper.Map<Account, DetailViewModel>(data);
            }
        }

        public async Task<DetailViewModel> GetAccountDetailAsync(string accNum)
        {
            using (var unit = _unitOfWorkFactory.Create())
            {
                var data = unit.Repository<Account>().Get().FirstOrDefault(a => a.AccountNumber == accNum);
                return _mapper.Map<Account, DetailViewModel>(data);
            }
        }

        public TransactionResponseModel Deposit(DepositViewModel model)
        {
            using (var unit = _unitOfWorkFactory.Create())
            {
                var result = Deposit(model, unit);
                unit.Save();
                return result;
            }

        }

        public async Task<TransactionResponseModel> DepositAsync(DepositViewModel model)
        {
            using (var unit = _unitOfWorkFactory.Create())
            {
                var result = await DepositAsync(model, unit);
                var saveResult = unit.SaveAsync().Result;
                if (saveResult.IsSuccessful)
                {
                    var account = unit.Repository<Account>().Get().SingleOrDefault(a => a.AccountNumber == model.AccountNumber);

                    if(account != null)
                    {
                        return new TransactionResponseModel
                        {
                            IsSuccessful = saveResult.IsSuccessful,
                            Message = saveResult.Message,
                            CurrentBalance = result.CurrentBalance,
                            RowVersion = Convert.ToBase64String(account.RowVersion)
                        };
                    }

                    return new TransactionResponseModel
                    {
                        IsSuccessful = false,
                        Message = Message.AccountNotFound
                    };

                }
                return new TransactionResponseModel
                {
                    IsSuccessful = saveResult.IsSuccessful,
                    Message = saveResult.Message,
                    RowVersion = result.RowVersion
                };
            }
        }

        public TransactionResponseModel Withdraw(WithdrawViewModel model)
        {
            using (var unit = _unitOfWorkFactory.Create())
            {
                var result = Withdraw(model, unit);
                unit.Save();
                return result;
            }
        }

        public TransactionResponseModel Transfer(TransferViewModel model)
        {
            if (model.AccountNumber != model.TargetAccountNumber)
            {
                var depositModel = new DepositViewModel
                {
                    AccountNumber = model.TargetAccountNumber,
                    Amount = model.Amount
                };

                var withdrawModel = new WithdrawViewModel
                {
                    AccountNumber = model.AccountNumber,
                    Amount = model.Amount
                };

                using (var unit = _unitOfWorkFactory.Create())
                {
                    try
                    {
                        var withdrawResult = Withdraw(withdrawModel, unit);
                        if (withdrawResult.IsSuccessful)
                        {
                            var depositResult = Deposit(depositModel, unit);
                            if (depositResult.IsSuccessful)
                            {
                                unit.Save();

                                var currentAccountBalance = unit.Repository<Account>().Get().FirstOrDefault(a => a.AccountNumber == withdrawModel.AccountNumber).Balance;

                                return new TransactionResponseModel
                                {
                                    IsSuccessful = true,
                                    Message = Message.SuccessfulTracsaction,
                                    CurrentBalance = currentAccountBalance
                                };
                            }

                            return new TransactionResponseModel
                            {
                                IsSuccessful = false,
                                Message = depositResult.Message
                            };
                        }
                        return new TransactionResponseModel
                        {
                            IsSuccessful = false,
                            Message = withdrawResult.Message
                        };
                    }
                    catch (Exception ex)
                    {
                        return new TransactionResponseModel
                        {
                            IsSuccessful = false,
                            Message = ex.InnerException.ToString()
                        };
                    }
                }
            }
            else
            {
                return new TransactionResponseModel
                {
                    IsSuccessful = false,
                    Message = Message.RedundantTransaction
                };
            }
        }
    }
}
