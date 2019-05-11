using Hasseeb.Application.Domain;
using Hasseeb.Application.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hasseeb.Application.Service
{
    public interface IAccountManager : IBaseService<Account>
    {
        Account GetAccountNatureWithItems(int id);
        bool UpdateAccount(Account account);
    }
}
