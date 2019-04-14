using Sidebucks.DAL.v1.Models;

namespace Sidebucks.BLL.v1.Repositories.Interface {
    public interface IMySkills {

        CustomResponse<GetSkillsResponseModel> GetSkills();
    }
}
