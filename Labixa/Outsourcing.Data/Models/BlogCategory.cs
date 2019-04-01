using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsourcing.Data.Models
{
    public class BlogCategory : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public bool Status { get; set; }

        /// <summary>
        /// URL  SEO friendly
        /// </summary>
        public string Slug { get; set; }
        
        /// <summary>
        /// Parent category
        /// </summary>
        public int? CategoryParentId { get; set; }

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
        virtual public ICollection<Blog> Blogs { get; set; }
        
        [ForeignKey("CategoryParentId")]
        virtual public BlogCategory CategoryParent { get; set; }
    }

}
