namespace BusinessLogic
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : class;
        void Save();
        void Dispose();
    }
}