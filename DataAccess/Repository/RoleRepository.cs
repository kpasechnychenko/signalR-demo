using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Repository;

namespace DataAccess.Repository
{
    public class RoleRepository : IRepositoryBase<Role>
    {
        public DemoContainer Context { get; set; }

        #region IRepositoryBase<Role> Members

        public Role Get(int id)
        {
            return Context.Roles.Where(x => x.Id == id).FirstOrDefault();
        }

        public IList<Role> GetByIds(int[] ids)
        {
            throw new NotImplementedException();
        }

        public void Save(Role entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Role entity)
        {
            throw new NotImplementedException();
        }

        public IList<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Role> Filter(System.Linq.Expressions.Expression<Func<Role, bool>> filter, System.Linq.Expressions.Expression<Func<Role, object>> order = null)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
