using System;
using System.Collections.Generic;
using System.Text;

namespace Hasseeb.Application.Domain
{
    public class AccountNature : BaseObject
    {
    //    public AccountNature()
    //    {
    //        this.Accounts = new HashSet<Account>();
    //    }

        public string AccountNatureName { get; set; }

        //public virtual ICollection<Account> Accounts { get; set; }
    }
}
