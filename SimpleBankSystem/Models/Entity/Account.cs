﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankSystem.Models.Entity
{
    public class Account : BaseEntity
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        
    }
}
