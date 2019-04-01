using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Outsourcing.Data.Models;
//using FluentValidation.Mvc;
//using FluentValidation.Validators;
//using FluentValidation;

namespace Labixa.Areas.Admin.ViewModel
{
    //[FluentValidation.Attributes.Validator(typeof(ProductValidator))]

    public class ProductFormModel
    {
        public ProductFormModel()
        {
            ListProductCategory = new List<SelectListItem>();
            ListProducts = new List<SelectListItem>();
            Vendor = new List<SelectListItem>();
            Location = new List<SelectListItem>();
            Promotion = new List<SelectListItem>();
            ListColors = new List<SelectListItem>();
        }

        public Product product { get; set; }
        public IEnumerable<SelectListItem> ListProductCategory { get; set; }
        public IEnumerable<SelectListItem> ListProducts { get; set; }
        public IEnumerable<SelectListItem> ListColors { get; set; }
        public IEnumerable<SelectListItem> Vendor { get; set; }
        public IEnumerable<SelectListItem> Location { get; set; }
        public IEnumerable<SelectListItem> Promotion { get; set; }
        public int CategoryId { get; set; }
        public int CategoryId2 { get; set; }
    }
    //public class ProductValidator : AbstractValidator<ProductFormModel>
    //{
    //    public ProductValidator()
    //    {
    //        //RuleFor(x => x.Name).NotNull().WithMessage("Tên Không Được Để Trống");
    //        //RuleFor(x => x.Description).NotNull().WithMessage("Mô Tả Không Được Để Trống");
    //        //RuleFor(x => x.Price).NotNull().WithMessage("Giá Không Được Để Trống");
    //    }
    //}
}