﻿using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Data.Repository
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