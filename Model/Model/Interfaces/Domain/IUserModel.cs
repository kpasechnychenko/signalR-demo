using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Model.Interfaces.Domain
{
    public interface IUserModel
    {
        void CreateUser(ViewRegisterUser user);
        void UpdateUser(ViewRegisterUser user);
        bool LoginUser(ViewLogin credetails);
        ViewRegisterUser GetUserByName(string name);

        List<ViewUserCoordinates> GetCoordinatesForUser(int userId, int eventId, DateTime dateFrom = default(DateTime), DateTime dateTo = default(DateTime));
        void SetUserCoordinates(ViewUserCoordinates data);
    }
}
