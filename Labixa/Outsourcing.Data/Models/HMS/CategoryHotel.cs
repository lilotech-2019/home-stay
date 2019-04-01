using System.Collections.Generic;

namespace Outsourcing.Data.Models.HMS
{
    public class CategoryHotel : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
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
        virtual public ICollection<Hotel> Hotels { get; set; }

    }
}
