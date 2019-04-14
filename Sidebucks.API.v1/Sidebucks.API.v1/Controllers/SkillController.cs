using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sidebucks.BLL.v1.Repositories.Interface;
using System.Collections.Generic;

namespace Sidebucks.API.v1.Controllers {
    [Route("skill")]
    [ApiVersion("1.0")]
    public class SkillController : BaseController {

        #region Initialization

        private readonly List<string> _infos = new List<string>();

        private readonly IMySkills _skill;

        public SkillController(IMySkills skill) {

            _skill = skill;
        }

        #endregion

        #region Get Skills

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("list")]
        public IActionResult GetSkills() {

            var fetchedSkills = _skill.GetSkills();

            return Ok(fetchedSkills);
        }

        #endregion
    }
}
