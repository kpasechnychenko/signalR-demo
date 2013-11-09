using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Model.Interfaces.Domain;

namespace demo.Controllers
{
    public class AdminPanelController : Controller
    {

        public IGroupModel GroupModel { get; set; }

        [Authorize]
        public ActionResult Groups()
        {
            return View(GroupModel.GetGroupsForUser(User.Identity.Name));
        }

    }
}
