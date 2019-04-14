using System;
using System.Collections.Generic;

namespace Sidebucks.DAL.v1.Models {
    public class GetSkillsResponseModel {

        public List<GetSkillsModel> Skills { get; set; }
    }

    public class GetSkillsModel {

        public long Id { get; set; }

        public string Skill { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
