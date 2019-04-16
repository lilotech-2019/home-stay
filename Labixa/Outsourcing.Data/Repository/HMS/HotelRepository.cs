﻿using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Data.Repository.HMS
{
   
    public class HotelRepository : RepositoryBase<Hotels>, IHotelRepository
    {
        public HotelRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IHotelRepository : IRepository<Hotels>
    {

    }
}
