using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Model;
using Model.Model.Interfaces.Domain;


namespace demo.Controllers
{
    public class GroupController : Controller
    {
        public IGroupModel GroupModel { get; set; }
        public IUserModel UserModel { get; set; }
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(ViewEventGroup info)
        {
            if (ModelState.IsValid)
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                    info.Group.UserId = UserModel.GetUserByName(User.Identity.Name).Id;
                    GroupModel.CreateGroup(info.Group);
                    info.Event.GroupId = GroupModel.GetGroupByName(info.Group.Name).Id;
                    GroupModel.CreateEvent(info.Event);
                //scope.Complete();
                //}
            }
            return RedirectToAction("Groups", "AdminPanel");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int groupId)
        {
            var grp = GroupModel.GetGroup(groupId);
            if (grp.UserId != UserModel.GetUserByName(User.Identity.Name).Id)
            {
                //handle exception there
                return RedirectToAction("Groups", "AdminPanel");
            }
            return View(new ViewEventGroup 
            {
                Group = grp,
                Event = GroupModel.GetEventByGroupId(groupId).FirstOrDefault()
            });
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(ViewEventGroup info)
        {
            if (ModelState.IsValid && info.Group.UserId == UserModel.GetUserByName(User.Identity.Name).Id)
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                    GroupModel.UpdateEvent(info.Event);
                    GroupModel.UpdateGroup(info.Group);
                //scope.Complete();
                //}
            }
            return RedirectToAction("Groups", "AdminPanel");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete(int groupId)
        {
            var grp = GroupModel.GetGroup(groupId);
            if (grp.UserId != UserModel.GetUserByName(User.Identity.Name).Id)
            {
                //handle exception there
                return RedirectToAction("Groups", "AdminPanel");
            }

            
            var evnt = GroupModel.GetEventByGroupId(groupId).FirstOrDefault();
            //using (TransactionScope scope = new TransactionScope())
            //{
                if (evnt != null)
                {
                    GroupModel.DeleteEvent(evnt.Id);
                }
                GroupModel.DeleteGroup(groupId);
                //scope.Complete();
            //}

            return RedirectToAction("Groups", "AdminPanel");
        }


        [Authorize]
        [HttpGet]
        public ActionResult View(int groupId)
        {
            var grp = GroupModel.GetGroup(groupId);
            if (grp.UserId != UserModel.GetUserByName(User.Identity.Name).Id)
            {
                //handle exception there
                return RedirectToAction("Groups", "AdminPanel");
            }

            return View(new ViewGroupWithUsers
                            {
                                Group = grp,
                                Users = GroupModel.GetUsersForEvent(GroupModel.GetEventByGroupId(groupId).FirstOrDefault().Id)
                            });
        }

    }
}
