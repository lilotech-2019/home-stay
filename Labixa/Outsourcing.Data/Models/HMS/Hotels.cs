using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsourcing.Data.Models.HMS
{

    public class Hotels : BaseEntity
    {
        public Hotels()
        {
            DateCreated = DateTime.Now.Date;
            LastEditedTime = DateTime.Now.Date;
        }
        public string MetaKeywords { get; set; }
        public string MetaTitle { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SharePercent { get; set; }
        public bool Status { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string Street { get; set; }
        public string ManagerName { get; set; }
        public string ManagerPhone { get; set; }
        public string ManagerEmail { get; set; }
        public string HostName { get; set; }
        public string HostPhone { get; set; }
        public string HostEmail { get; set; }
        public string HostAddress { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? ContractExpire { get; set; }
        public string UrlImage1 { get; set; }
        public string UrlImage2 { get; set; }
        public string UrlImage3 { get; set; }
   
        /// <summary>
        /// URL  SEO friendly
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Type of Layout
        /// </summary>
        public int? Layout { get; set; }

        /// <summary>
        /// Position Display
        /// </summary>
        public int? DisplayOrder { get; set; }

        /// <summary>
        /// Is this blog is static Page
        /// </summary>
        public bool IsStaticPage { get; set; }
        public bool Deleted { get; set; }
        public int Position { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastEditedTime { get; set; }
        public string Noted { get; set; }

        public int CategoryHotelId { get; set; }
        [ForeignKey("CategoryHotelId")]
        public virtual CategoryHotels CategoryHotel { get; set; }
        public virtual ICollection<CostOrder> CostOrders { get; set; }
        public string MetaDescription { get; set; }
    }
}
