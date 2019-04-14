using Newtonsoft.Json;

namespace Sidebucks.DAL.v1.Models {
    public class SendEmailConfirmation {
        public string Email { get; set; }

        [JsonIgnore]
        public string Uri { get; set; }
    }

    public class ConfirmEmailModel {
        public string UserId { get; set; }

        public string Token { get; set; }
    }
}
