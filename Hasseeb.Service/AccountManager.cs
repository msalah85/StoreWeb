using Hasseeb.Application.Domain;
using Hasseeb.Application.Repository;
using Hasseeb.Application.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hasseeb.Service
{
    public class AccountManager : BaseService<Account>, IAccountManager
    {
        private readonly Repository.MyContext _context;
        public AccountManager(IRepository<Account> repository , Repository.MyContext context) : base(repository)
        {
            _context = context;
        }
        public Account GetAccountNatureWithItems(int id)
        {
            return _repository.GetAll().Where(x => x.ID == id).ToList().First();
        }

        public bool UpdateAccount(Account account)
        {
            bool result = false;
            _context.Accounts.Update(account);
            result = _context.SaveChanges() > 0;
            return result;
        }
    }
}
