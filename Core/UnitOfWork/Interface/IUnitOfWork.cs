namespace Core.UnitOfWork.Interface
{
    public interface IUnitOfWork:IDisposable
    {
        Task CommitAsync();
        void Commit();
    }
}
