using Sidebucks.DAL.v1.Models;
using System.Threading.Tasks;

namespace Sidebucks.BLL.v1.Repositories.Interface {
    public interface IMyFiles {

        Task<CustomResponse<GetFilesResponse>> GetFiles(GetFiles model);

        Task<CustomResponse<GetFilesResponse>> SaveFiles(FileBase64Model model);

        Task<CustomResponse<GetFilesResponse>> DeleteFiles(DeleteFiles model);
    }
}
