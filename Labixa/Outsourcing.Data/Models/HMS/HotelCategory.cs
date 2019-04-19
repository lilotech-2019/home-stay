using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Outsourcing.Data.Models.HMS
{
    public class HotelCategory : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        
        [Range(0, 100)]
        public int SharePercent { get; set; }

        /// <summary>
        /// URL  SEO friendly
        /// </summary>
        public string Slug { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }
    }

}