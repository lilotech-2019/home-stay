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
            RelatedBlogs = new List<Blog>();
        }
        public PagedList.IPagedList<Outsourcing.Data.Models.Blog> ListBlogs { get; set; } //danh sách tin tuc
        public IEnumerable<Blog> RelatedBlogs { get; set; } //relateBlog

        public Blog listBlogNew { get; set; }

    }
}