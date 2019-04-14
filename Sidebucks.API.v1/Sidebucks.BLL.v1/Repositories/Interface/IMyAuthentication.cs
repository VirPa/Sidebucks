using Sidebucks.DAL.v1.Models;
using System.Threading.Tasks;

namespace Sidebucks.BLL.v1.Repositories.Interface {
    public interface IMyAuthentication {

        Task<CustomResponse<SignInReponseModel>> SignInTheReturnUser(SignInModel model);

        Task<CustomResponse<string>> SignOut(SignOutModel model);

        Task<CustomResponse<GenerateTokenResponseModel>> GenerateToken(GenerateTokenModel model);
    }
}
