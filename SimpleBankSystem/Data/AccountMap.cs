using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBankSystem.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankSystem.Data
{
    public class AccountMap
    {
        public AccountMap(EntityTypeBuilder<Account> entityBuilder)
        {
            entityBuilder.HasKey(account => account.Id);
        }
    }
}
