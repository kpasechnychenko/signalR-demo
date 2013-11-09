using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Model.Interfaces.Domain
{
    public interface IGroupModel
    {
        void CreateGroup(ViewGroup group);
        void UpdateGroup(ViewGroup group);
        void DeleteGroup(int id);

        List<ViewEvent> GetEventsForUser(string userName);
        ViewGroup GetGroup(int id);
        ViewGroup GetGroupByName(string name);
        List<ViewGroup> GetGroups(string userName);
        List<ViewGroup> GetGroupsForUser(string userName);

        void CreateEvent(ViewEvent evnt);
        void UpdateEvent(ViewEvent evnt);
        void DeleteEvent(int evntId);
        List<ViewEvent> GetEventByGroupId(int groupId);
        List<ViewEvent> GetEvents(string userName);

        List<ViewUser> GetUsersForEvent(int eventId);

        void AddGroupForUser(string userName, int groupId);
    }
}
