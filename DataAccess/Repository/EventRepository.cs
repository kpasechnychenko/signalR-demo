using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Repository;
using System.Web.Configuration;

namespace DataAccess.Repository
{
    public class EventRepository : IRepositoryBase<Event>
    {
        public DemoContainer Context { get; set; }

        #region IRepositoryBase<Event> Members

        public Event Get(int id)
        {
            return Context.Events.Where(x => x.Id == id).FirstOrDefault();
        }

        public IList<Event> GetByIds(int[] ids)
        {
            return Context.Events.Where(x => ids.Contains(x.Id)).ToList();
        }

        public void Save(Event entity)
        {
            if (!Context.Events.Any(x => x.Id == entity.Id))
            {
                Context.AddToEvents(entity);
            }

            Context.SaveChanges();
        }

        public void Delete(Event entity)
        {
            Context.Events.DeleteObject(entity);
            Context.SaveChanges();
        }

        public IList<Event> GetAll()
        {
            return Context.Events.ToList();
        }

        public IList<Event> Filter(System.Linq.Expressions.Expression<Func<Event, bool>> filter, System.Linq.Expressions.Expression<Func<Event, object>> order = null)
        {
            if(order != null)
            {
                return Context.Events.Where(filter).OrderBy(order).ToList();
            }
            return Context.Events.Where(filter).ToList();
        }

        #endregion
    }
}
