using System.Linq;
using System.Web.Mvc;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal.@base;

namespace Outsourcing.Service.Portal
{
    public interface IBlogCategoryService : IServiceBase<BlogCategories>
    {
        IQueryable<BlogCategories> FindSelectList(int? id = null);
    }

    public class BlogCategoryService : ServiceBase<BlogCategories>, IBlogCategoryService
    {
        public IQueryable<BlogCategories> FindSelectList(int? id)
        {
            var data = Repository.FindBy(r => r.Deleted == false);
            if (id != null)
            {
                data = data.Where(w => w.Id == id);
            }
            return data;
        }

        public BlogCategoryService(IRepository<BlogCategories> repository, IUnitOfWork unitOfWork) : base(repository,
            unitOfWork)
        {
        }
    }
}