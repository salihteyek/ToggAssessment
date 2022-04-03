using Grpc.Core;
using ManagementPanel.Core.Enums;
using ManagementPanel.Core.Models;
using ManagementPanel.Core.Services;
using ManagementPanel.Service.Mapping;
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
        public override Task<EditUserResponse> EditUser(EditUserRequest request, ServerCallContext context)
        {
            EditUserResponse response = new();
            
            var user = _service.GetUserByIdAsync(request.Id);
            var panelUser = new PanelUser()
            {
                Id = request.Id,
                Active = request.Active,
                UserStatus = (request.UserStatus == "Accept" ? UserStatus.Accept : UserStatus.Decline),
                Email = user.Result.Email,
                UserName = user.Result.UserName,
                FullName = user.Result.FullName
            };
            _service.EditPanelUser(panelUser);
            _service.SendEditedPanelUser(panelUser);

            response.Id = request.Id;
            response.Active = request.Active;
            response.UserStatus = request.UserStatus;
            return Task.FromResult(response);
        }
        #endregion

        public override async Task<UsersResponse> GetUsers(Empty request, ServerCallContext context)
        {
            UsersResponse response = new();

            var userList = await _service.GetUsersAsync();

            var responseList = new Google.Protobuf.Collections.RepeatedField<PanelUserModel>();
            userList.ToList().ForEach(item => {
                responseList.Add(new PanelUserModel() { Id = item.Id, FullName = item.FullName, UserName = item.UserName, Email = item.Email, Active = item.Active, UserStatus = item.UserStatus.ToString() });
            });
            response.PanelUserModel.Add(responseList);

            return await Task.FromResult(response);
        }
    }
}
