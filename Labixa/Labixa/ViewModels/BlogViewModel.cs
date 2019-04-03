using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Outsourcing.Data.Models;

namespace Labixa.ViewModels
{
    public class BlogViewModel
    {
        public BlogViewModel()
        {
            RelatedBlogs = new List<Blogs>();
        }
        public PagedList.IPagedList<Outsourcing.Data.Models.Blogs> ListBlogs { get; set; } //danh sách tin tuc
        public IEnumerable<Blogs> RelatedBlogs { get; set; } //relateBlog

        public Blogs listBlogNew { get; set; }

    }
}