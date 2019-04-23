using System.Linq;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.Portal.Base;

namespace Outsourcing.Service.Portal
{
    public interface IRoomOrderService : IServiceBase<RoomOrder>
    {
    }

    public class RoomOrderService : ServiceBase<RoomOrder>, IRoomOrderService
    {
        #region Ctor

        public RoomOrderService(IRepository<RoomOrder> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        #endregion

        #region BaseMethod

        #endregion
    }
}