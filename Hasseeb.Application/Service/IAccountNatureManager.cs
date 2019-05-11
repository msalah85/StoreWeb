
using Hasseeb.Application.Domain;
using Hasseeb.Application.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hasseeb.Application.Service
{
    public interface IAccountNatureManager : IBaseService<AccountNature>
    {
        AccountNature GetAccountNatureWithItems(int id);

    }
}
