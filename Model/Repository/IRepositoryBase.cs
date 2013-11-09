using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Objects.DataClasses;

namespace Model.Repository
{
    public interface IRepositoryBase<T> where T : EntityObject
    {
        T Get(int id);
        IList<T> GetByIds(int[] ids);
        void Save(T entity);
        void Delete(T entity);
        IList<T> GetAll();

        IList<T> Filter(Expression<Func<T, bool>> filter, Expression<Func<T, object>> order = null);
    }
}
