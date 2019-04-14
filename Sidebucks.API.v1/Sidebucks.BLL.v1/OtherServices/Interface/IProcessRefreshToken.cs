using Sidebucks.DAL.v1.Models;
using System.Threading.Tasks;

namespace Sidebucks.BLL.v1.OtherServices.Interface {
    public interface IProcessRefreshToken {

        Task<CustomResponse<TokenResource>> GenerateRefreshToken(RefreshTokenGetModel model);
    }
}
