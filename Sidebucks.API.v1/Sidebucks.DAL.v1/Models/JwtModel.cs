﻿using System;

namespace Sidebucks.DAL.v1.Models {

    public class JwtModel {
        public string Sub { get; set; }
    }

    public class RefreshTokenGetModel {
        public string UserId { get; set; }

        public string UserAgent { get; set; }

        public string AppVersion { get; set; }

        public string ApiVersion { get; set; }

        public string Token { get; set; }
    }

    public class TokenResource {
        public string Token { get; set; }

        public DateTime? ExpiredAt { get; set; }
    }
}
