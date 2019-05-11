using Hasseeb.Application.Domain;
using JqueryDataTables.ServerSide.AspNetCoreWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hasseeb.Application.Service
{
    public interface IBaseService<T> where T : BaseObject
    {
        Task<IEnumerable<T>> GetAllTable(DTParameters param);
        List<T> GetAll();
        T GetID(int id);
        void Insert(T entity);
        void Update(T entity);
        bool Delete(int id);
        IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    }
}

