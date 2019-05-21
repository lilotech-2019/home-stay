using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Service
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
