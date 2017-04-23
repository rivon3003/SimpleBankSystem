using AutoMapper;
using SimpleBankSystem.Models.Entity;
using SimpleBankSystem.Models.ViewModel.Account;

namespace SimpleBankSystem.Services.Mapping
{
    public class EntityToViewModelProfile : Profile
    {
        public EntityToViewModelProfile()
        {
            CreateMap<Account, DetailViewModel>();
        }
    }
}
