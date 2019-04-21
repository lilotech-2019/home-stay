using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsourcing.Data.Models
{
    public class Blog : BaseEntity
    {
        public Blog()
        {
            DateCreated = DateTime.Now;
            LastEditedTime = DateTime.Now;
        }

        public string Title { get; set; }
        public string TitleEnglish { get; set; }

        /// <summary>
        /// URL  SEO friendly
        /// </summary>
        public string Slug { get; set; }

        public string BlogImage { get; set; }

        public string Description { get; set; }
        public string DescriptionEnglish { get; set; }
        public string Content { get; set; }
        public string ContentEnglish { get; set; }

        public string MetaKeywords { get; set; }
        public string MetaTitle { get; set; }
        public string MetaTitleEnglish { get; set; }
        public string MetaDescription { get; set; }
        public string MetaDescriptionEnglish { get; set; }


        public bool IsAvailable { get; set; }
        public bool IsHomePage { get; set; }
        public DateTime LastEditedTime { get; set; }

        //Get or set the picture of blog
        public int PictureId { get; set; }

        public int BlogCategoryId { get; set; }

        [ForeignKey("BlogCategoryId")]
        public virtual BlogCategories BlogCategory { get; set; }
    }
}