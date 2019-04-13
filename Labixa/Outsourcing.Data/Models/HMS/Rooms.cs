using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models.HMS
{

    public class Rooms : BaseEntity
    {

        public string Name { get; set; }
        public string NameENG { get; set; }
        public string Description { get; set; }
        public string DescriptionENG { get; set; }
        public int SharePercent { get; set; }
        public bool Status { get; set; }
        public double Price { get; set; }
        public int? DiscountPercent { get; set; }
        public string Temp_1 { get; set; }
        public string Temp_2 { get; set; }
        public string Temp_3 { get; set; }
        public string Temp_4 { get; set; }
        public string Temp_5 { get; set; }
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
        /// <summary>
        /// Ma phòng
        /// </summary>
        public string string1 { get; set; }
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

        virtual public Hotels Hotel { get; set; }


    public virtual ICollection<RoomImageMappings> RoomImageMappings { get; set; }
    public virtual ICollection<RoomOrder> RoomOrders { get; set; }

    }
}
