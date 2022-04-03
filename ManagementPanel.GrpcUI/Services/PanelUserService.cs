using Grpc.Core;
using ManagementPanel.Core.Enums;
using ManagementPanel.Core.Models;
using ManagementPanel.Core.Services;
using Paneluserpackage;
using static Paneluserpackage.PanelUserService;

namespace ManagementPanel.GrpcUI.Services
{
    public class PanelUserService : PanelUserServiceBase
    {
        private readonly IPanelUserService _service;
        public PanelUserService(IPanelUserService service)
        {
            _service = service;
        }

        #region Take and Save registered User
        public override Task<RegisteredUserResponse> TakeRegisteredUser(RegisteredUserRequest request, ServerCallContext context)
        {
            RegisteredUserResponse response = new();

            // save manager db
            _service.SaveRegisteredUserAsync(new PanelUser()
            {
                Id = request.PanelUserModel.Id,
                FullName = request.PanelUserModel.FullName,
                UserName = request.PanelUserModel.UserName,
                Email = request.PanelUserModel.Email,
                Active = request.PanelUserModel.Active || false,
                UserStatus = UserStatus.Pending,
            });
            
            // send data
            response.Id = request.PanelUserModel?.Id;
            response.UserStatus = UserStatus.Pending.ToString();

            return Task.FromResult(response);
        }
        #endregion
    }
}
