using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Service
{
    public interface IHotelCategoryService : IServiceBase<HotelCategory>
    {
    }

    public class HotelCategoryService : ServiceBase<HotelCategory>, IHotelCategoryService
    {
        public HotelCategoryService(IRepository<HotelCategory> repository, IUnitOfWork unitOfWork) : base(repository,
            unitOfWork)
        {
        }
        }
}