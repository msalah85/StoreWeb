using Hasseeb.Application.Domain;
using Hasseeb.Application.Repository;
using Hasseeb.Application.Service;
using JqueryDataTables.ServerSide.AspNetCoreWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hasseeb.Service
{
    public class BaseService<T> : IBaseService<T> where T : BaseObject
    {
        public IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;

        }

        public bool Delete(int id)
        {
            bool result = false;
            _repository.Delete(_repository.Get(id));
           result = _repository.SaveChanges();
            return result;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _repository.GetBy(predicate);
        }

        public async Task<IEnumerable<T>> GetAllTable(DTParameters param)
        {
            return await _repository.GetAllTable(param);
        }
        public List<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T GetID(int id)
        {
            return _repository.Get(id);
        }

        public void Insert(T entity)
        {
            _repository.Insert(entity);
            _repository.SaveChanges();
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
            _repository.SaveChanges();
        }
    }
}
