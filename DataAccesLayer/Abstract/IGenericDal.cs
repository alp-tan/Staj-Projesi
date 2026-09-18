using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccesLayer.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(int id);
        List<T> TGetAll(params Expression<Func<T, object>>[] includes);
    }
    
}
