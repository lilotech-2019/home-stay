using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Outsourcing.Data.Models
{

    public class CostCategory : BaseEntity
    {
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }
        public string Icon { get; set; }
        public int? CategoryParentId { get; set; }
        public string Noted { get; set; }

        /// <summary>
        /// URL  SEO friendly
        /// </summary>
        public string Slug { get; set; }

        public virtual ICollection<Cost> Costs { get; set; }
    }
}
