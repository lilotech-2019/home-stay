using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.Portal.Base;

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