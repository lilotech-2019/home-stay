using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;
using Outsourcing.Service.Portal.@base;

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