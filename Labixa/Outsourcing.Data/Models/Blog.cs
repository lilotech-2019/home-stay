using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class Blog : BaseEntity
    {
        public Blog()
        {
            this.DateCreated = DateTime.Now;
            this.LastEditedTime = DateTime.Now;
        }
        public string Title { get; set; }
        public string TitleENG{ get; set; }

        /// <summary>
        /// URL  SEO friendly
        /// </summary>
        public string Slug { get; set; }

        public string BlogImage { get; set; }

        public string Description { get; set; }
        public string DescriptionENG { get; set; }
        public string Temp_1 { get; set; }
        public string Temp_2 { get; set; }
        public string Temp_3 { get; set; }
        public string Temp_4 { get; set; }
        public string Temp_5 { get; set; }
        public string Content { get; set; }
        public string ContentENG { get; set; }

        public string MetaKeywords { get; set; }
        public string MetaTitle { get; set; }
        public string MetaTitleEN { get; set; }
        public string MetaDescription { get; set; }
        public string MetaDescriptionEN { get; set; }


        public bool IsAvailable { get; set; }
        public bool IsHomePage { get; set; }
        public bool Deleted { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime LastEditedTime { get; set; }

        //Get or set the picture of blog
        public int PictureId { get; set; }
        public int BlogCategoryId { get; set; }
        public int Position { get; set; }

        [ForeignKey("BlogCategoryId")]
        virtual public BlogCategories BlogCategory { get; set; }

    }

}
