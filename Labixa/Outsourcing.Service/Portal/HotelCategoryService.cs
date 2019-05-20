using System.Linq;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Service.Portal
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