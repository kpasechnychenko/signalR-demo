using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Repository;
using System.Web.Configuration;
using System.Data.Objects.DataClasses;

namespace DataAccess.Repository
{
    public class UserRepository : IRepositoryBase<User>
    {
        public DemoContainer Context { get; set; }

        #region IRepositoryBase<User> Members

        public User Get(int id)
        {
            return Context.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public IList<User> GetByIds(int[] ids)
        {
            return Context.Users.Where(x => ids.Contains(x.Id)).ToList();
        }

        public void Save(User entity)
        {
            if (!Context.Users.Any( x=> x.UserName == entity.UserName))
            {
                Context.AddToUsers(entity);
            }
            Context.SaveChanges();
        }

        public void Delete(User entity)
        {
            var user = Context.Users.Where(x => x.Id == entity.Id).FirstOrDefault();
            if (user != null)
            {
                user.IsActive = false;
                Context.SaveChanges();
            }
        }

        public IList<User> GetAll()
        {
            return Context.Users.ToList();
        }

        public IList<User> Filter(System.Linq.Expressions.Expression<Func<User, bool>> filter, System.Linq.Expressions.Expression<Func<User, object>> order = null)
        {
            if (order != null)
            {
                return Context.Users.Where(filter).OrderBy(order).ToList();
            }
            return Context.Users.Where(filter).ToList();
        }

        #endregion
    }
}
