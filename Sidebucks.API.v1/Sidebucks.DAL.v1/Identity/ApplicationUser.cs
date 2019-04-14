using Microsoft.AspNetCore.Identity;
using System;

namespace Sidebucks.DAL.v1.Identity {

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser {

        public string Fullname { get; set; }

        public string MobileNumber { get; set; }

        public string BackgroundSummary { get; set; }

        public long SkillId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
