﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class WebsiteAttributes : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ControlType { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool IsPublic { get; set; }
        public string Title { get; set; }
        public string TitleEnglish { get; set; }
        public string Caption { get; set; }
        public string CaptionEnglish { get; set; }
        
        

    }
}