using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Service.Portal.Base;

namespace Outsourcing.Service.Portal
{
    public interface IMessageService : IServiceBase<Message>
    {
    }

    public class MessageService: ServiceBase<Message>, IMessageService
    {
        public MessageService(IRepository<Message> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
