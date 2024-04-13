using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //Lấy all class T
        IEnumerable<T> GetAll(string? include = null);
        IEnumerable<T> GetFlow(int skip,int pageSize,string sortColumn, string sortDirection, string? include = null);
        IEnumerable<T> GetFlowRestore(Expression<Func<T, bool>> exception,int skip, int pageSize, string sortColumn, string sortDirection, string? include = null);

        //Add ? kiểu T
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        //Tìm id 
        T GetById(Expression<Func<T, bool>> exception, string? include = null);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);

        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        IEnumerable<T> GetAllWhere(Expression<Func<T, bool>> exception, string? include = null);

    }
}
