﻿using System;
using System.Collections.Generic;

namespace Sidebucks.DAL.v1.Entities.Mobile
{
    public partial class Files
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string CodeName { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }
        public int? Type { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? IsCurrentProfilePicture { get; set; }
        public string FeedId { get; set; }
    }
}
