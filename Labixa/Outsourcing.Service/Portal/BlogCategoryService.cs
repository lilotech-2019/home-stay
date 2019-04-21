using System.Linq;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal.@base;

namespace Outsourcing.Service.Portal
{
    public interface IBlogCategoryService : IServiceBase<BlogCategories>
    {
        IQueryable<BlogCategories> FindSelectList(int? id);
    }

    public class BlogCategoryService : ServiceBase<BlogCategories>, IBlogCategoryService
    {
        public IQueryable<BlogCategories> FindSelectList(int? id)
        {
            var list = Repository.FindBy(r => r.Deleted == false);
            if (id != null)
            {
                list = list.Where(w => w.Id == id);
            }
            return list;
        }

        public BlogCategoryService(IRepository<BlogCategories> repository, IUnitOfWork unitOfWork) : base(repository,
            unitOfWork)
        {
        }
    }
}