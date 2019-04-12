namespace Outsourcing.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory _databaseFactory;
        private ApplicationDbContext _dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        protected ApplicationDbContext DataContext => _dataContext ?? (_dataContext = _databaseFactory.Get());

        public void Commit()
        {
            DataContext.Commit();
        }
    }
}