﻿using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Data.Repository.HMS
{
  
    public class CategoryHotelRepository : RepositoryBase<HotelCategory>, ICategoryHotelRepository
    {
        public CategoryHotelRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ICategoryHotelRepository : IRepository<HotelCategory>
    {

    }
}
