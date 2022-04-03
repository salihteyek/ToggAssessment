using ManagementPanel.Core.Models;
using ManagementPanel.Core.Repositories;
using ManagementPanel.Core.Services;
using ManagementPanel.Core.UnitOfWork;

namespace ManagementPanel.Service.Services
{
    public class PanelUserService : IPanelUserService
    {
        private readonly IRepository<PanelUser> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public PanelUserService(IRepository<PanelUser> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task SaveRegisteredUserAsync(PanelUser entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }
    }
}
