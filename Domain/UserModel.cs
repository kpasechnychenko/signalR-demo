using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Model.Interfaces.Domain;
using Model.Repository;
using DataAccess;
using Model.Model;
using System.Data.Objects.DataClasses;

namespace Domain
{
    public class UserModel : IUserModel
    {
        #region Properties

        public IRepositoryBase<User> UserRepository { get; set; }
        public IRepositoryBase<TrackLog> TrackLogRepository { get; set; }
        public IRepositoryBase<Role> ReleRepository { get; set; }
        public IRepositoryBase<Event> EventRepository { get; set; }
        public IRepositoryBase<Group> GroupRepository { get; set; }
        public IRepositoryBase<UsersInGroups> UsersInGroupsRepository { get; set; }

        #endregion

        #region IUserModel Members

        public void CreateUser(ViewRegisterUser user)
        {
            if (UserRepository.Filter(x=> x.UserName == user.UserName).Count > 0)
            {
                throw new ApplicationException("User already exists");
            }
            var tmp_user = new User();
            tmp_user.FirstName = user.FirstName;
            tmp_user.Email = user.Email;
            tmp_user.IsActive = true;
            tmp_user.LastName = user.LastName;
            tmp_user.Password = user.Password;
            tmp_user.UserName = user.UserName;
            tmp_user.Role = ReleRepository.Get(1);

            UserRepository.Save(tmp_user);
        }

        public void UpdateUser(ViewRegisterUser user)
        {
            var tmp_user = (UserRepository as IRepositoryBase<User>).Get(user.Id);
            tmp_user.FirstName = user.FirstName;
            tmp_user.Email = user.Email;
            tmp_user.IsActive = true;
            tmp_user.LastName = user.LastName;
            tmp_user.Password = user.Password;
            tmp_user.UserName = user.UserName;

            UserRepository.Save(tmp_user);
        }

        public bool LoginUser(ViewLogin credetails)
        {
            var tmp_user = UserRepository.Filter(x => x.UserName == credetails.UserName).FirstOrDefault();
            return tmp_user != null && tmp_user.Password == credetails.Password;
        }

        public ViewRegisterUser GetUserByName(string name)
        {
            var tmp = UserRepository.Filter(x => x.UserName == name).FirstOrDefault();
            return new ViewRegisterUser
            {
                Email = tmp.Email,
                FirstName = tmp.FirstName,
                Id = tmp.Id,
                IsActive = tmp.IsActive,
                LastName = tmp.LastName,
                Password = tmp.Password,
                UserName = tmp.UserName
            };
        }

        public List<ViewUserCoordinates> GetCoordinatesForUser(int userId, int eventId, DateTime dateFrom = default(DateTime), DateTime dateTo = default(DateTime))
        {
            if (dateFrom != default(DateTime) && dateTo != default(DateTime))
            {
                return TrackLogRepository.Filter(x =>
                    x.User.Id == userId
                    && x.Event.Id == eventId
                    && DateTime.Compare(x.LogDateTime, dateFrom) >= 0
                    && DateTime.Compare(x.LogDateTime, dateTo) <= 0)
                    .Select(x => new ViewUserCoordinates
                    {
                        DateTime = x.LogDateTime,
                        EventId = eventId,
                        Lat = x.Lat,
                        Lon = x.Lon,
                        UserId = userId,
                        UserName = x.User.UserName
                    }).ToList();
            }
            return TrackLogRepository.Filter(x =>
                   x.User.Id == userId
                   && x.Event.Id == eventId)
                   .Select(x => new ViewUserCoordinates
                   {
                       DateTime = x.LogDateTime,
                       EventId = eventId,
                       Lat = x.Lat,
                       Lon = x.Lon,
                       UserId = userId,
                       UserName = x.User.UserName
                   }).ToList();
        }

        public void SetUserCoordinates(Model.Model.ViewUserCoordinates data)
        {
            var tmp_user = UserRepository.Filter(x => x.UserName == data.UserName).FirstOrDefault();
            var tmp_event = EventRepository.Get(data.EventId);
            var tmp_track = new TrackLog
            {
                Event = tmp_event,
                Lat = data.Lat,
                Lon = data.Lon,
                User = tmp_user,
                LogDateTime = data.DateTime
            };
            TrackLogRepository.Save(tmp_track);
        }
        #endregion
    }
}
