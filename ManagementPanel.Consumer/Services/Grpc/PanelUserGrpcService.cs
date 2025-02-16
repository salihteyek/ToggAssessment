﻿using ManagementPanel.Core.Models;
using Paneluserpackage;

namespace ManagementPanel.Consumer.Services.Grpc
{
    public class PanelUserGrpcService
    {
        private readonly GrpcContext _grpcContext;
        public PanelUserGrpcService(GrpcContext grpcContext)
        {
            _grpcContext = grpcContext;
        }

        public async Task TakeRegisteredUser(PanelUser entity)
        {
            RegisteredUserRequest request = new();
            request.PanelUserModel = new PanelUserModel();
            request.PanelUserModel.Id = entity.Id;
            request.PanelUserModel.FullName = entity.FullName;
            request.PanelUserModel.UserName = entity.UserName;
            request.PanelUserModel.Email = entity.Email;
            request.PanelUserModel.Active = entity.Active;
            request.PanelUserModel.UserStatus = entity.UserStatus.ToString();

            await _grpcContext._panelUserClient.TakeRegisteredUserAsync(request);
        }
    }
}
