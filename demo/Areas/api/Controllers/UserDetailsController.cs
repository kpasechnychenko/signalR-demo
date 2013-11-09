using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Model.Interfaces.Domain;
using Model.Model;

namespace demo.Areas.api.Controllers
{
    public class UserDetailsController : Controller
    {
        //
        // GET: /api/UserDetails/

        public IUserModel UserModel { get; set; }
        public IGroupModel GroupModel { get; set; }

        [Authorize]
        public JsonResult GetUserTrack(int userId, int groupid)
        {
            var ev = GroupModel.GetEventByGroupId(groupid).FirstOrDefault();
            return Json(UserModel.GetCoordinatesForUser(userId, ev.Id), JsonRequestBehavior.AllowGet);
        }

    }
}
