using Hasseeb.Application.Domain;
using JqueryDataTables.ServerSide.AspNetCoreWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hasseeb.Application.Repository
{
    public interface IRepository<T> where T:BaseObject
    {
        Task<IEnumerable<T>> GetAllTable(DTParameters param);
        List<T> GetAll();
        T Get(int ID);
        IQueryable<T> GetBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        bool SaveChanges();
    }
}
