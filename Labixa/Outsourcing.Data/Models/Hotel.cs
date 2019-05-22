using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Outsourcing.Data.Models
{
    public class Hotel : BaseEntity
    {
        public Hotel()
        {
            DateCreated = DateTime.Now;
            LastModify = DateTime.Now;
        }

        public string MetaKeywords { get; set; }
        public string MetaTitle { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [AllowHtml]
        [DataType(DataType.Html)]
        public string Content { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string Ward { get; set; }

        public string ManagerName { get; set; }
        public string ManagerPhone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string ManagerEmail { get; set; }
        public string HostName { get; set; }
        public string HostPhone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string HostEmail { get; set; }
        public string HostAddress { get; set; }
        public string Hotline { get; set; }
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

        public string Noted { get; set; }

        public int HotelCategoryId { get; set; }

        [ForeignKey("HotelCategoryId")]
        public virtual HotelCategory HotelCategory { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }

        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }
    }
}