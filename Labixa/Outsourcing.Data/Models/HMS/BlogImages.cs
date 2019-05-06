﻿using System.Collections.Generic;

namespace Outsourcing.Data.Models.HMS
{
    public class BlogImages : BaseEntity
    {
        public string Binary { get; set; }
        public string MimeType { get; set; }
        public string Url { get; set; }

        public string Description { get; set; }

        public virtual ICollection<BlogImageMappings> BlogImageMappings { get; set; }
    }
}
