using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Outsourcing.Data.Models.HMS
{
    public class CategoryHotels : BaseEntity
    {

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [Range(0, 100)]
        public int SharePercent { get; set; }
        public bool Status { get; set; }

        /// <summary>
        /// URL  SEO friendly
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Type of Layout
        /// </summary>
        public int Layout { get; set; }

        /// <summary>
        /// Position Display
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Is this blog is static Page
        /// </summary>
        public bool IsStaticPage { get; set; }
        public string Noted { get; set; }
        public string string1 { get; set; }
        public virtual ICollection<Hotels> Hotels { get; set; }

        public bool IsDelete { get; set; }
    }
}
