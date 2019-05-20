using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Service.Portal
{
    public interface IHotelService : IServiceBase<Hotel>
    {
    }

    public class HotelService : ServiceBase<Hotel>, IHotelService
    {
        public HotelService(IRepository<Hotel> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}