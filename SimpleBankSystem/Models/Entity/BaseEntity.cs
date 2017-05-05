using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBankSystem.Models.Entity
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        //public DateTime CreatedBy { get; set; }
        //public DateTime UpdatedDate { get; set; }
        //public DateTime UpdatedBy { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
