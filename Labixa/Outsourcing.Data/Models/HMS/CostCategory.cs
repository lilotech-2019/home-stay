using System.Collections.Generic;

namespace Outsourcing.Data.Models.HMS
{

    public class CostCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int? CategoryParentId { get; set; }
        public string Noted { get; set; }

        /// <summary>
        /// URL  SEO friendly
        /// </summary>
        public string Slug { get; set; }

        public virtual ICollection<Costs> Costs { get; set; }
    }
}
