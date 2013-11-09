using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Repository;
using System.Web.Configuration;
using System.Data.Objects.DataClasses;

namespace DataAccess.Repository
{
    public class UsersInGroupsRepository : IRepositoryBase<UsersInGroups>
    {
        public DemoContainer Context { get; set; }

        #region IRepositoryBase<UsersInGroups> Members

        public UsersInGroups Get(int id)
        {
            return Context.UsersInGroups.Where(x => x.Id == id).FirstOrDefault();
        }

        public IList<UsersInGroups> GetByIds(int[] ids)
        {
            return Context.UsersInGroups.Where(x => ids.Contains(x.Id)).ToList();
        }

        public void Save(UsersInGroups entity)
        {
            if (!Context.UsersInGroups.Any(x => x.Id == entity.Id))
            {
                Context.AddToUsersInGroups(entity);
            }
            Context.SaveChanges();
        }

        public void Delete(UsersInGroups entity)
        {
            Context.UsersInGroups.DeleteObject(entity);
            Context.SaveChanges();
        }

        public IList<UsersInGroups> GetAll()
        {
            return Context.UsersInGroups.ToList();
        }

        public IList<UsersInGroups> Filter(System.Linq.Expressions.Expression<Func<UsersInGroups, bool>> filter, System.Linq.Expressions.Expression<Func<UsersInGroups, object>> order = null)
        {
            if (order != null)
            {
                return Context.UsersInGroups.Where(filter).OrderBy(order).ToList();
            }
            return Context.UsersInGroups.Where(filter).ToList();
        }

        #endregion
    }
}
