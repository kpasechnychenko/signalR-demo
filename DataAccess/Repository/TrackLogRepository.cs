using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Repository;
using System.Web.Configuration;
using System.Data.Objects.DataClasses;

namespace DataAccess.Repository
{
    public class TrackLogRepository : IRepositoryBase<TrackLog>
    {
        public DemoContainer Context { get; set; }

        #region IRepositoryBase<TrackLog> Members

        public TrackLog Get(int id)
        {
            return Context.TrackLogs.Where(x => x.Id == id).FirstOrDefault();
        }

        public IList<TrackLog> GetByIds(int[] ids)
        {
            return Context.TrackLogs.Where(x => ids.Contains(x.Id)).ToList();
        }

        public void Save(TrackLog entity)
        {
            if (!Context.TrackLogs.Any(x => x.Id == entity.Id))
            {
                Context.AddToTrackLogs(entity);
            }
            Context.SaveChanges();
        }

        public void Delete(TrackLog entity)
        {
            Context.TrackLogs.DeleteObject(entity);
            Context.SaveChanges();
        }

        public IList<TrackLog> GetAll()
        {
            return Context.TrackLogs.ToList();
        }

        public IList<TrackLog> Filter(System.Linq.Expressions.Expression<Func<TrackLog, bool>> filter, System.Linq.Expressions.Expression<Func<TrackLog, object>> order = null)
        {
            if (order != null)
            {
                return Context.TrackLogs.Where(filter).OrderBy(order).ToList();
            }
            return Context.TrackLogs.Where(filter).ToList();
        }

        #endregion
    }
}
