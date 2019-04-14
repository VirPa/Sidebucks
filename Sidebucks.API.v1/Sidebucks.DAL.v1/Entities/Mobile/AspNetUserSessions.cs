using System;
using System.Collections.Generic;

namespace Sidebucks.DAL.v1.Entities.Mobile
{
    public partial class AspNetUserSessions
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiredAt { get; set; }
        public bool? Validity { get; set; }
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string AppVersion { get; set; }
        public string ApiVersion { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? LastAccessVerifiedAt { get; set; }
        public string AccessToken { get; set; }
    }
}
