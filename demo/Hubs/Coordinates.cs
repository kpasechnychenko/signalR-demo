using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Model.Model;
using Model.Model.Interfaces.Domain;

namespace demo.Hubs
{
    [Authorize]
    public class Coordinates: Hub
    {
        private class UserMapping
        {
            public string ConnectionId { get; set; }
            public string UserName { get; set; }
            public int ViewedGroup { get; set; }
        }

        private static readonly ConcurrentDictionary<string, UserMapping> Users = new ConcurrentDictionary<string, UserMapping>();

        public IGroupModel GroupModel { get; set; }
        public IUserModel UserModel { get; set; }

        public override Task OnConnected()
        {
            var vg = -1;
            if (Context.QueryString.AllKeys.Contains("groupId"))
            {
                vg = int.Parse(Context.QueryString["groupId"]);
            }
            var um = new UserMapping 
            {
                ConnectionId = Context.ConnectionId,
                UserName = Context.User.Identity.Name,
                ViewedGroup = vg
            };

            Users.GetOrAdd(string.Format("{0}_{1}", Context.QueryString["activationType"], Context.User.Identity.Name), um);
            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            UserMapping value = null;
            Users.TryRemove(string.Format("{0}_{1}", Context.QueryString["activationType"], Context.User.Identity.Name), out value);
            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            var vg = -1;
            if (Context.QueryString.AllKeys.Contains("groupId"))
            {
                vg = int.Parse(Context.QueryString["groupId"]);
            }

            var um = new UserMapping
            {
                ConnectionId = Context.ConnectionId,
                UserName = Context.User.Identity.Name,
                ViewedGroup = vg
            };

            Users.TryUpdate(string.Format("{0}_{1}", Context.QueryString["activationType"], Context.User.Identity.Name), um, um);
            return base.OnReconnected();
        }

        public void Send(string message)
        {
            var messageValues = message.Split('~');
            var trackRecord = new ViewUserCoordinates
            {
                 DateTime = DateTime.Now,
                 UserName = Context.User.Identity.Name,
                 EventId = int.Parse(messageValues[0]),
                 Lat = double.Parse(messageValues[1]),
                 Lon = double.Parse(messageValues[2])
            };

            UserModel.SetUserCoordinates(trackRecord);

            var groupId = GroupModel.GetEventsForUser(Context.User.Identity.Name).Where(x => x.Id == trackRecord.EventId).FirstOrDefault().GroupId;
            var userName = GroupModel.GetGroup(groupId).UserName;
            UserMapping um = null;
            Users.TryGetValue(string.Format("admin_{0}", userName), out um);
            if (um != null && um.ViewedGroup == groupId)
            {
                lock (um.ConnectionId)
                {
                    var usr = UserModel.GetUserByName(Context.User.Identity.Name);
                    var msg = string.Format("{0}~{1}~{2}~{3}~{4}", usr.Id, usr.FirstName, usr.LastName, messageValues[1], messageValues[2]);
                    Clients.Client(um.ConnectionId).addMessage(msg);
                }
            }
        }
    }
}