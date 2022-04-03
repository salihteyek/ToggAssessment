namespace ManagementPanel.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
        void Save();
    }
}
