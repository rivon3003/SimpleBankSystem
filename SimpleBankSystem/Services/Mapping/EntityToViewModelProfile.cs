using AutoMapper;
using SimpleBankSystem.Models.Entity;
using SimpleBankSystem.Models.ViewModel.Account;
using System;

namespace SimpleBankSystem.Services.Mapping
{
    public class EntityToViewModelProfile : Profile
    {
        public EntityToViewModelProfile()
        {
            CreateMap<Account, DetailViewModel>().ForMember(dest => dest.RowVersion, opts => opts.MapFrom(src => Convert.ToBase64String(src.RowVersion)));
        }
    }
}
