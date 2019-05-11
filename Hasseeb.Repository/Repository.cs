using Hasseeb.Application.Domain;
using Hasseeb.Application.Repository;
using JqueryDataTables.ServerSide.AspNetCoreWeb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hasseeb.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseObject
    {
        private readonly MyContext _context;
        private DbSet<T> _entities;
        string errorMessage = string.Empty;

        public Repository(MyContext context)
        {
            this._context = context;
            _entities = context.Set<T>();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);
        }

        public T Get(int ID)
        {

            return _entities.SingleOrDefault(s => s.ID == ID);
        }

        public async Task<IEnumerable<T>> GetAllTable(DTParameters table)
        {
            try
            {
                int start = table != null ? table.Start : 0,
                    length = table != null ? table.Length : 25;



                var Items = await _entities.AsNoTracking()
                                .Skip((start) * length).Take(length)
                                .ToArrayAsync();
                return Items;
            }
            catch (Exception e)
            {

                throw e;
            }



        }


        public List<T> GetAll()
        {
            List<T> list = _entities.ToList();
            return list;
        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = _entities.Where(predicate);
            return query;
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Add(entity);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}


