using ManagementPanel.Core.Enums;
using ManagementPanel.Core.Models;
using ManagementPanel.Core.Repositories;
using ManagementPanel.Core.Services;
using ManagementPanel.Core.Services.RabbitMQ;
using ManagementPanel.Core.UnitOfWork;
using RabbitMQ.Client;

namespace ManagementPanel.Service.Services
{
    public class PanelUserService : IPanelUserService
    {
        private readonly IRepository<PanelUser> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRabbitManager _rabbitManager;

        public string ExchangeName = "EditedSendPanelUser";
        public string RoutingName = "route-edited-user";
        public string QueueName = "queue-edited-user";

        public PanelUserService(IRepository<PanelUser> repository, IUnitOfWork unitOfWork, IRabbitManager rabbitManager)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _rabbitManager = rabbitManager;
        }

        public async Task SaveRegisteredUserAsync(PanelUser entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditPanelUser(PanelUser entity)
        {
            var user = _repository.Where(x => x.Id == entity.Id).FirstOrDefault();
            user.Active = entity.Active;
            user.UserStatus = entity.UserStatus.ToString() == "Accept" ? UserStatus.Accept : UserStatus.Decline;
            await _unitOfWork.SaveAsync();
        }

        public async Task SendEditedPanelUser(PanelUser user)
        {
            await _rabbitManager.Publish(user, ExchangeName, ExchangeType.Headers, RoutingName, QueueName);
        }

        public async Task<PanelUser> GetUserByIdAsync(string id)
        {
            var user = _repository.Where(x => x.Id == id).FirstOrDefault();
            if (user == null) return null;
            return user;
        }

        public async Task<IEnumerable<PanelUser>> GetUsersAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
