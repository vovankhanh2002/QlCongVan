using AccsessLayer;
using BusinessLayer.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
namespace BusinessLayer.Repository
{
    public class Repository<T> : IRepository<T> where T: class
    {
        private readonly DBContextCV _dbContext;
        internal DbSet<T> _dbSet { get; set; }
        public Repository(DBContextCV dbContext)
        {

            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll(string? include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
            {
                foreach (var item in include.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }

            }
            return query.ToList();
        }

        public T GetById(Expression<Func<T, bool>> exception, string? include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
            {
                foreach (var item in include.Split(new char[','], StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }

            }
            query = query.Where(exception);
            return query.SingleOrDefault();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IEnumerable<T> GetAllWhere(Expression<Func<T, bool>> exception, string? include = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(exception);
            if (include != null)
            {
                foreach (var item in include.Split(new char[','], StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }

            }
            return query.ToList();
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }

        public IEnumerable<T> GetFlow(int skip, int pageSize, string sortColumn, string sortDirection, string? include = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Skip(skip).Take(pageSize);
            if (!(string.IsNullOrEmpty(sortColumn)) && !(string.IsNullOrEmpty(sortDirection)))
            {
                query = query.OrderBy(string.Concat(sortColumn, " ", sortDirection));
            }
            if(include != null)
            {
                foreach (var item in include.Split(new char[','], StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.ToList();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public IEnumerable<T> GetFlowRestore(Expression<Func<T, bool>> exception, int skip, int pageSize, string sortColumn, string sortDirection, string? include = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(exception);
            if (!(string.IsNullOrEmpty(sortColumn)) && !(string.IsNullOrEmpty(sortDirection)))
            {
                query = query.OrderBy(string.Concat(sortColumn, " ", sortDirection));
            }
            if (include != null)
            {
                foreach (var item in include.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            //query = query.Skip(skip).Take(pageSize);
            return query.ToList();
        }

        
    }
}
