using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SimpleBankSystem.Models.ViewModel.Account;
using SimpleBankSystem.Models.Entity;

namespace SimpleBankSystem.Mapping
{
    public class ViewModelToEntityProfile : Profile
    {
        public ViewModelToEntityProfile()
        {
            CreateMap<RegisterViewModel, Account>().ForMember(dest => dest.RowVersion, opts => opts.MapFrom(src => Convert.FromBase64String(src.RowVersion)));
        }
    }
}
