using Sidebucks.DAL.v1.Models;

namespace Sidebucks.BLL.v1.DataManagers.Interface {
    public interface IUsersDataManager {

        GetUserDetailModel GetUser(GetUserModel model);

        GetUsersModel GetUsers(GetUserModel model);
    }
}
