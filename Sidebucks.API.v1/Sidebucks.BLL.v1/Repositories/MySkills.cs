using AutoMapper;
using Sidebucks.BLL.v1.Repositories.Interface;
using Sidebucks.DAL.v1.Entities.Mobile;
using Sidebucks.DAL.v1.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sidebucks.BLL.v1.Repositories {
    internal class MySkills : IMySkills {

        #region Initialization

        private readonly List<string> _infos = new List<string>();
        
        private readonly IMapper _mapper;
        
        private readonly SidebucksContext _context;

        #endregion

        #region Constructor

        public MySkills(IMapper mapper,
            SidebucksContext context) {
            
            _mapper = mapper;
            _context = context;
        }

        #endregion

        #region Get

        public CustomResponse<GetSkillsResponseModel> GetSkills() {

            var skills = _context.Skills.Where(s => s.IsActive == true).ToList();

            return new CustomResponse<GetSkillsResponseModel> {
                Succeed = true,
                Data = new GetSkillsResponseModel {
                    Skills = _mapper.Map<List<GetSkillsModel>>(skills)
                }
            };
        }

        #endregion
    }
}
