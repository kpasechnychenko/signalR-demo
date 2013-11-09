using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Model.Interfaces.Domain;
using Model.Repository;
using DataAccess;
using Model.Model;

namespace Domain
{
    public class GroupModel : IGroupModel
    {
        public IRepositoryBase<Group> GroupRepository { get; set; }
        public IRepositoryBase<User> UserRepository { get; set; }
        public IRepositoryBase<UsersInGroups> UsersInGroupsRepository { get; set; }
        public IRepositoryBase<Event> EventRepository { get; set; }

        #region IGroupModel Members

        public void CreateGroup(ViewGroup group)
        {
            GroupRepository.Save(new Group 
            {
                 Description = group.Description,
                 Name = group.Name,
                 Owner = UserRepository.Get(group.UserId)
            });
        }

        public void UpdateGroup(ViewGroup group)
        {
            var grp = GroupRepository.Get(group.Id);
            grp.Description = group.Description;
            grp.Name = group.Name;
            GroupRepository.Save(grp);
        }

        public void DeleteGroup(int id)
        {
            var grp = GroupRepository.Get(id);
            if (grp != null)
            {
                var tmp = grp.UsersInGroups;
                foreach (var u in tmp)
                {
                    UsersInGroupsRepository.Delete(u);
                }
                GroupRepository.Delete(grp);
            }
        }

        public List<ViewEvent> GetEventsForUser(string userName)
        {
            return
                UserRepository.Filter(y => y.UserName == userName)
                .FirstOrDefault()
                .UsersInGroups.Where(x => x.IsActive)
                .Select(x => x.Group)
                .SelectMany(x => x.Events)
                .Select(x => new ViewEvent {
                 Description = x.Description,
                 GroupId = x.Group.Id,
                 Id = x.Id,
                 Lat = x.BaseLat,
                 Lon = x.BaseLon,
                 Name = x.Name,
                 Radius = x.BaseRadius
                }).ToList();
        }

        public ViewGroup GetGroup(int id)
        {
            var tmp = GroupRepository.Get(id);
            return new ViewGroup 
            {
                Description = tmp.Description,
                Id = tmp.Id,
                Name = tmp.Name,
                UserId = tmp.Owner.Id,
                UserName = tmp.Owner.UserName
            };
        }

        public void CreateEvent(ViewEvent evnt)
        {
            EventRepository.Save(new Event 
            {
                BaseLat = evnt.Lat,
                BaseLon = evnt.Lon,
                BaseRadius = evnt.Radius,
                Description = evnt.Description,
                Name = evnt.Name,
                Group = GroupRepository.Get(evnt.GroupId)
            });
        }

        public void UpdateEvent(ViewEvent evnt)
        {
            var tmp = EventRepository.Get(evnt.Id);
            tmp.BaseLat = evnt.Lat;
            tmp.BaseLon = evnt.Lon;
            tmp.BaseRadius = evnt.Radius;
            tmp.Description = evnt.Description;
            tmp.Name = evnt.Name;
            EventRepository.Save(tmp);
        }

        public void DeleteEvent(int evntId)
        {
            var tmp = EventRepository.Get(evntId);
            if (tmp != null)
            {
                EventRepository.Delete(tmp);
            }
        }

        public List<ViewEvent> GetEvents(string userName)
        {
            var ids = UserRepository.Filter(y => y.UserName == userName)
                .FirstOrDefault()
                .UsersInGroups.Where(x => x.IsActive)
                .Select(x => x.Group)
                .SelectMany(x => x.Events)
                .Select(x => x.Id);

            return EventRepository.Filter(x => !ids.Contains(x.Id))
                .Select(x => new ViewEvent 
            {
                 Description = x.Description,
                 GroupId = x.Group.Id,
                 Id = x.Id,
                 Lat = x.BaseLat,
                 Lon = x.BaseLon,
                 Name = x.Name,
                 Radius = x.BaseRadius
            }).ToList();
        }

        public List<ViewUser> GetUsersForEvent(int eventId)
        {
             return EventRepository.Get(eventId).Group
                 .UsersInGroups.Select(x => new ViewUser 
                 {
                     Email = x.User.Email,
                     FirstName = x.User.FirstName,
                     Id = x.User.Id,
                     IsActive = x.User.IsActive,
                     LastName = x.User.LastName,
                     UserName = x.User.UserName
                 }).ToList();
            throw new NotImplementedException();
        }

        public void AddGroupForUser(string userName, int groupId)
        {
            UsersInGroupsRepository.Save(new UsersInGroups 
            {
             IsActive = true,
              User = UserRepository.Filter(x => x.UserName == userName).FirstOrDefault(),
              Group = GroupRepository.Get(groupId)
            });
        }

        public List<ViewGroup> GetGroups(string userName)
        {
            var ids = UserRepository.Filter(y => y.UserName == userName)
                .FirstOrDefault()
                .UsersInGroups.Where(x => x.IsActive)
                .Select(x => x.Group.Id);

            return GroupRepository.Filter(x => !ids.Contains(x.Id))
                .Select(x => new ViewGroup
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    UserId = x.Owner.Id,
                    UserName = x.Owner.UserName
                }).ToList();
        }

        public List<ViewGroup> GetGroupsForUser(string userName)
        {

            return GroupRepository
                .Filter(x => x.Owner.UserName == userName)
                .Select(x =>
                new ViewGroup
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    UserId = x.Owner.Id,
                    UserName = x.Owner.UserName
                }).ToList();
        }

        public ViewGroup GetGroupByName(string name)
        {
            var tmp = GroupRepository.Filter(x => x.Name == name).FirstOrDefault();
            return new ViewGroup
            {
                Description = tmp.Description,
                Id = tmp.Id,
                Name = tmp.Name,
                UserId = tmp.Owner.Id,
                UserName = tmp.Owner.UserName
            };
        }

        public List<ViewEvent> GetEventByGroupId(int groupId)
        {
            return
                GroupRepository
                .Get(groupId)
                .Events
                .Select(x => new ViewEvent
                {
                    Description = x.Description,
                    GroupId = x.Group.Id,
                    Id = x.Id,
                    Lat = x.BaseLat,
                    Lon = x.BaseLon,
                    Name = x.Name,
                    Radius = x.BaseRadius
                }).ToList();
        }

        #endregion
    }
}
