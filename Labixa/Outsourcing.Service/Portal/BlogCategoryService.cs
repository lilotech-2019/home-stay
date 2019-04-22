using System.Linq;
using System.Web.Mvc;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal.Base;

namespace Outsourcing.Service.Portal
{
    public interface IBlogCategoryService : IServiceBase<BlogCategories>
    {
    }

    public class BlogCategoryService : ServiceBase<BlogCategories>, IBlogCategoryService
    {
        public BlogCategoryService(IRepository<BlogCategories> repository, IUnitOfWork unitOfWork) : base(repository,
            unitOfWork)
        {
        }
    }
}