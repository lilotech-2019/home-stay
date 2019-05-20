using System.Linq;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal.Base;

namespace Outsourcing.Service
{
    public interface IBlogService : IServiceBase<Blog>
    {
        Blog FindBySlug(string slug);
    }

    public class BlogService : ServiceBase<Blog>, IBlogService
    {
        public BlogService(IRepository<Blog> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public Blog FindBySlug(string slug)
        {
            return Repository.FindBy(w => w.Slug == slug).FirstOrDefault();
        }
    }
}