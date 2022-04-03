using Grpc.Net.Client;
using ManagementPanel.Consumer.Helpers;
using static Paneluserpackage.PanelUserService;

namespace ManagementPanel.Consumer.Services.Grpc
{
    public class GrpcContext
    {
        public GrpcChannel _channel { get; set; }
        public PanelUserServiceClient _panelUserClient { get; set; }

        public GrpcContext(Clients clients)
        {
            _channel = GrpcChannel.ForAddress(clients.ManagementService.ToString());
            _panelUserClient = new PanelUserServiceClient(_channel);
        }

    }
}
