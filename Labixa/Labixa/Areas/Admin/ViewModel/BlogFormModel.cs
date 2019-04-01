using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Labixa;
using Outsourcing.Data.Models;
//using FluentValidation.Mvc;
//using FluentValidation.Validators;
//using FluentValidation;
using Labixa.App_Data;
namespace Labixa.Areas.Admin.ViewModel
{
    //[FluentValidation.Attributes.Validator(typeof(BlogValidator))]
    public class BlogFormModel
    {
        
        public BlogFormModel()
        {
            ListCategory = new List<SelectListItem>();
        }

        [Key]
        public int Id { get; set; }
       // [DisplayName(@"Tiêu đề")]
        [Display(Name = "Title", ResourceType = typeof(Resources))]  
        public string Title { get; set; }
        /// <summary>
        /// URL  SEO friendly
        /// </summary>
        //[DisplayName(@"Đường dẫn")]
        [Display(Name = "Slug", ResourceType = typeof(Resources))]  
        public string Slug { get; set; }

       // [DisplayName(@"Thẻ meta từ khóa")]
        [Display(Name = "MetaKeyWords", ResourceType = typeof(Resources))]  
        public string MetaKeywords { get; set; }

        //[DisplayName(@"Thẻ meta Trang")]
        [Display(Name = "MetaTitle", ResourceType = typeof(Resources))]  
        public string MetaTitle { get; set; }

        //[DisplayName(@"Thẻ meta Mô tả")]
        [Display(Name = "MetaDescription", ResourceType = typeof(Resources))]
        public string MetaDescription { get; set; }

       // [DisplayName(@"Hình ảnh")]
        [Display(Name = "BlogImage", ResourceType = typeof(Resources))]
        public string BlogImage { get; set; }

       // [DisplayName(@"Mô tả ")]
          [Display(Name = "Description", ResourceType = typeof(Resources))]
        public string Description { get; set; }
        //[DisplayName(@"Nội dung")]
          [Display(Name = "Content", ResourceType = typeof(Resources))]
          public string Content { get; set; }

        [DisplayName(@"Hiển thị")]
        [Display(Name = "IsAvailable", ResourceType = typeof(Resources))]
        public bool IsAvailable { get; set; }

        //Get or set the picture of blog
        //Later
        [DisplayName(@"Hình ảnh")]
        //
        public int PictureId { get; set; }

        [DisplayName(@"Vị Trí")]
        public int Position { get; set; }

        [DisplayName(@"Danh mục")]
        [Display(Name = "BlogCategoryId", ResourceType = typeof(Resources))]
        public int BlogCategoryId { get; set; }

        [DisplayName(@"Danh mục")]
        public IEnumerable<SelectListItem> ListCategory { get; set; }
    }
    //public class BlogValidator : AbstractValidator<BlogFormModel>
    //{
    //    public BlogValidator()
    //    {
    //        // RuleFor(x => x.Title).NotNull().WithMessage("fffffff");
    //        RuleFor(x => x.BlogCategoryId).NotNull().WithMessage("Danh Mục Không Được Để Trống");
    //        RuleFor(x => x.Content).NotNull().WithMessage("Nội Dung Không Được Để Trống");
    //        //RuleFor(x => x.Description).NotNull().WithMessage("Mô Tả Không Được Để Trống");

    //    }
    //}
}