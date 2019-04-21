using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal.Base;

namespace Outsourcing.Service.Portal
{
    public interface IBlogService : IServiceBase<Blog>
    {
    }

    public class BlogService : ServiceBase<Blog>, IBlogService
    {
        public BlogService(IRepository<Blog> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}