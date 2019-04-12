using System;

namespace Outsourcing.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        ApplicationDbContext Get();
    }
}
