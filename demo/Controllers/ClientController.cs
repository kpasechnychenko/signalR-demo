using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Model.Interfaces.Domain;
using Model.Model;

namespace demo.Controllers
{
    public class ClientController : Controller
    {
        public IGroupModel GroupModel { get; set; }
        public IUserModel UserModel { get; set; }

        [Authorize]
        [HttpGet]
        public ActionResult Events()
        {
            return View(GroupModel.GetEventsForUser(User.Identity.Name));
        }

        [Authorize]
        [HttpGet]
        public ActionResult ApplyGroup()
        {
            return View(GroupModel.GetGroups(User.Identity.Name));
        }


        [Authorize]
        [ActionName("ApplyGroup"), HttpPost]
        public ActionResult ApplyGroupSave(List<ViewGroup> groups)
        {
            foreach (var gr in groups)
            {
                if (gr.Selected)
                {
                    GroupModel.AddGroupForUser(User.Identity.Name, gr.Id);
                }
            }
            return RedirectToAction("Events");
        }
    }
}
