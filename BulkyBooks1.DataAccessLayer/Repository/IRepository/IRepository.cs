using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBooks1.DataAccessLayer.Repository.IRepository
{
    public interface IRepository<T> where T : class 
    {
        // T -- Category
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeproperties  = null);
        IEnumerable<T> GetAll(string? incluideproperties = null);
      
        void Add(T entity); 
        void Remove(T entity);
        void RemoveRange(T entity);
    }
}
