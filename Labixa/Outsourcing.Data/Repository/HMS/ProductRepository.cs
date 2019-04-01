﻿using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models.HMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Repository.HMS
{

    public class ProductRepository : RepositoryBase<HMSProduct>, IProductRepository
    {
        public ProductRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IProductRepository : IRepository<HMSProduct>
    {

    }
}
