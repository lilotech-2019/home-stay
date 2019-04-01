using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin.ViewModels
{
    public class HotelModel
    {
        public HotelModel()
        {
            DateCreated = DateTime.Now.Date;
            LastEditedTime = DateTime.Now.Date;
            Id = 0;
        }
        public int Id { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SharePercent { get; set; }
        public bool Status { get; set; }
        public string Address { get; set; }
        //[DisplayName(@"Quận")]
        //public string District { get; set; }
        //[DisplayName(@"Phường")]
        //public string Ward { get; set; }
        //[DisplayName(@"Đường")]
        //public string Street { get; set; }
        //[DisplayName(@"Tên Quản lý")]
        //public string ManagerName { get; set; }
        //[DisplayName(@"Phần trăm chiết khấu")]
        //public string ManagerPhone { get; set; }
        //[DisplayName(@"Phần trăm chiết khấu")]
        //public string ManagerEmail { get; set; }
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

        ///// <summary>
        ///// Type of Layout
        ///// </summary>
        //public int? Layout { get; set; }

        ///// <summary>
        ///// Position Display
        ///// </summary>
        //public int? DisplayOrder { get; set; }

        /// <summary>
        /// Is this blog is static Page
        /// </summary>
        //public bool IsStaticPage { get; set; }
        //[DisplayName(@"Phần trăm chiết khấu")]
        //public bool Deleted { get; set; }
        //[DisplayName(@"Phần trăm chiết khấu")]
        //public int Position { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastEditedTime { get; set; }
        public int CategoryHotelId { get; set; }
        public IEnumerable<SelectListItem> ListCategoryHotel { get; set; }
    }
}