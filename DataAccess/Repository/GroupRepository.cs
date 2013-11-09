using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Repository;
using System.Web.Configuration;

namespace DataAccess.Repository
{
    public class GroupRepository : IRepositoryBase<Group>
    {

        public DemoContainer Context { get; set; }

        #region IRepositoryBase<Group> Members

        public Group Get(int id)
        {
            return Context.Groups.Where(x => x.Id == id).FirstOrDefault();
        }

        public IList<Group> GetByIds(int[] ids)
        {
            return Context.Groups.Where(x => ids.Contains(x.Id)).ToList();
        }

        public void Save(Group entity)
        {
            if (!Context.Groups.Any(x => x.Id == entity.Id))
            {
                Context.AddToGroups(entity);
            }
            Context.SaveChanges();
        }

        public void Delete(Group entity)
        {
            Context.Groups.DeleteObject(entity);
            Context.SaveChanges();
        }

        public IList<Group> GetAll()
        {
            return Context.Groups.ToList();
        }

        public IList<Group> Filter(System.Linq.Expressions.Expression<Func<Group, bool>> filter, System.Linq.Expressions.Expression<Func<Group, object>> order = null)
        {
            if (order != null)
            {
                return Context.Groups.Where(filter).OrderBy(order).ToList();
            }
            return Context.Groups.Where(filter).ToList();
        }

        #endregion
    }
}
