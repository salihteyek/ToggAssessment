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

        #region Save Registered User
        public override async Task<RegisteredUserResponse> TakeRegisteredUser(RegisteredUserRequest request, ServerCallContext context)
        {
            RegisteredUserResponse response = new();

            PanelUser user = new PanelUser()
            {
                Id = request.PanelUserModel.Id,
                FullName = request.PanelUserModel.FullName,
                UserName = request.PanelUserModel.UserName,
                Email = request.PanelUserModel.Email,
                Active = request.PanelUserModel.Active,
                UserStatus = UserStatus.Pending,
            };

            await _service.SaveRegisteredUserAsync(user);
            response.Id = request.PanelUserModel.Id;
            response.UserStatus = UserStatus.Pending.ToString();
            return await Task.FromResult(response);
        }

        #endregion

        #region Take and Save registered User
        public override async Task<EditUserResponse> EditUser(EditUserRequest request, ServerCallContext context)
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
            await _service.EditPanelUser(panelUser);
            await _service.SendEditedPanelUser(panelUser);

            response.Id = request.Id;
            response.Active = request.Active;
            response.UserStatus = request.UserStatus;
            return await Task.FromResult(response);
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
