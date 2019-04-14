﻿namespace Sidebucks.DAL.v1.Models {
    public class SendEmailModel {
        public string Recipient { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }

    public class SendEmailWithTemplateModel {
        public string Recipient { get; set; }

        public string FullName { get; set; }

        public string Subject { get; set; }

        public string Template { get; set; }

        public string Link { get; set; }
    }
}
