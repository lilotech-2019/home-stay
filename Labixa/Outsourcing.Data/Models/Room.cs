using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Data.Models
{
    public class Room : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string NameEnglish { get; set; }
        public string Slug { get; set; }
        public string SlugEnglish { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string DescriptionEnglish { get; set; }

        [Range(0, 100)]
        public int SharePercent { get; set; }

        public double Price { get; set; }

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
       public virtual ICollection<RoomAsset> RoomAssets { get; set; }
    }

    public enum RoomType
    {
        ShortTempDeposit = 0,
        ShortTemp = 1,
        LongTemp = 2
    }
}