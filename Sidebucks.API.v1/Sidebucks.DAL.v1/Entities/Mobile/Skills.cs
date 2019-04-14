using System;
using System.Collections.Generic;

namespace Sidebucks.DAL.v1.Entities.Mobile
{
    public partial class Skills
    {
        public long Id { get; set; }
        public string Skill { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
