﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Sidebucks.DAL.v1.Models {
    public class CreateUserModel {

        public string Fullname { get; set; }

        public string MobileNumber { get; set; }

        public string BackgroundSummary { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Role { get; set; } = "Provider"; //1=Provider, 2=Customer

        public long SkillId { get; set; } = 0;
    }

    public class CreateUserResponseModel {

        public UserResponse User { get; set; }
    }

    public class UpdateUserModel {

        public string UserId { get; set; }

        public string Fullname { get; set; }

        public string MobileNumber { get; set; }

        public string BackgroundSummary { get; set; }
    }

    public class GetUserModel {
        [JsonIgnore]
        public string Email { get; set; }

        public string UserId { get; set; }
    }

    public class UserResponse {

        public UserDetails Detail { get; set; }

        public GetFilesListResponse ProfilePicture { get; set; }

        public PinLocationDetailResponseModel Location { get; set; }
    }

    public class UserDetails {

        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Fullname { get; set; }

        public string Role { get; set; }

        public string MobileNumber { get; set; }

        public string BackgroundSummary { get; set; }

        public string Skill { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }

    public class ChangeProfilePictureModel {

        public string Id { get; set; }

        public ICollection<IFormFile> CoverPhoto { get; set; }
    }

    public class ChangeProfilePictureResponse {

        public List<GetFilesListResponse> ProfilePicture { get; set; }
    }

    #region Get Users

    public class GetUserDataManagerModel {

        public string User { get; set; }
    }

    public class GetUsersDataManagerModel {

        public string Users { get; set; }
    }

    public class GetUsersModel {

        public List<GetUserDetailModel> Users { get; set; }
    }

    public class GetUserDetailModel {

        public string UserId { get; set; }

        public string Email { get; set; }

        public string Fullname { get; set; }

        public string UserName { get; set; }

        public string MobileNumber { get; set; }

        public string Role { get; set; }

        public string Skill { get; set; }

        public string BackgroundSummary { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public List<GetUserFilesModel> ProfilePicture { get; set; }
    }

    public class GetUserFilesModel {

        public string Id { get; set; }
        public string Name { get; set; }
        public string CodeName { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UpdateBackgroundSummaryModel {

        public string BackgroundSummary { get; set; }

        [JsonIgnore]
        public string Email { get; set; }
    }

    #endregion
}
