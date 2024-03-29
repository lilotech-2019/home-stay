﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Outsourcing.Data.Models
{
    public class HotelCategory : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        /// <summary>
        /// URL  SEO friendly
        /// </summary>
        public string Slug { get; set; }

        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}