using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsourcing.Data.Models.HMS
{
    public class Rooms : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public string NameEN { get; set; }
        public string Description { get; set; }
        public string DescriptionENG { get; set; }

        [Range(0, 100)]
        public int SharePercent { get; set; }

        public double Price { get; set; }

        [Range(0, 100)]
        public int DiscountPercent { get; set; }

        /// <summary>
        /// URL  SEO friendly
        /// </summary>
        public string Slug { get; set; }

        public string Noted { get; set; }


        public bool Utility_Tivi { get; set; }
        public bool Utility_TuDo { get; set; }
        public bool Utility_HotWater { get; set; }
        public bool Utility_DryHair { get; set; }
        public bool Utility_Iron { get; set; }
        public bool Utility_Kitchen { get; set; }
        public bool Utility_TeaCoffee { get; set; }
        public bool Utility_Snack { get; set; }
        public bool Utility_WashMachine { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }
        public RoomType Type { get; set; }

        public virtual ICollection<RoomImageMappings> RoomImageMappings { get; set; }
        public virtual ICollection<RoomOrder> RoomOrders { get; set; }
    }

    public enum RoomType
    {
        ShortTemp = 1,
        LongTemp = 2
    }
}