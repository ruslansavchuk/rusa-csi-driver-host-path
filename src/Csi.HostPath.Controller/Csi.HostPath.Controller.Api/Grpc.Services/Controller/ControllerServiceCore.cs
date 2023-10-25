using MediatR;

namespace Csi.HostPath.Controller.Api.Grpc.Services.Controller;

public partial class ControllerService : Csi.V1.Controller.ControllerBase
{
    #region Fields
    
    private readonly ISender _sender;
    
    #endregion

    #region Constructors
    
    public ControllerService(IMediator sender)
    {
        _sender = sender;
    }
    
    #endregion
}