using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Service
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