using Dapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sidebucks.BLL.v1.DataManagers.Interface;
using Sidebucks.DAL.v1.Models;
using System.Data;
using System.Linq;

namespace Sidebucks.BLL.v1.DataManagers {
    public class UsersDataManager : BaseDataManager, IUsersDataManager {

        private readonly IOptions<Manifest> _options;

        private const string SpGetUser = "sp_GetUser";
        private const string SpGetUsers = "sp_GetUsers";

        public UsersDataManager(IOptions<Manifest> options) {
            _options = options;
        }

        public GetUserDetailModel GetUser(GetUserModel model) {

            using (var conn = GetSysDbConnection(_options.Value.DefaultConnection)) {

                var getUsers = conn.Query<GetUserDetailModel>(SpGetUser, new {
                        model.UserId
                    },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                return getUsers;
            }
        }

        public GetUsersModel GetUsers(GetUserModel model) {

            using (var conn = GetSysDbConnection(_options.Value.DefaultConnection)) {

                var getUsersJson = conn.Query<GetUsersDataManagerModel>(SpGetUsers, new {
                        model.UserId
                    },
                    commandType: CommandType.StoredProcedure).ToList();

                var mappedUsers = JsonConvert.DeserializeObject<GetUsersModel>(getUsersJson.FirstOrDefault()?.Users);

                return mappedUsers;
            }
        }
    }
}
