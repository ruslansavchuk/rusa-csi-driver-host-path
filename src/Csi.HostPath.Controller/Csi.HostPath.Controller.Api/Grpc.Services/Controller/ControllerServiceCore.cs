using MediatR;

namespace Csi.HostPath.Controller.Api.Grpc.Services.Controller;

public partial class ControllerService : Csi.V1.Controller.ControllerBase
{
    #region Fields
    
    private readonly IMediator _mediator;
    
    #endregion

    #region Constructors
    
    public ControllerService(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    #endregion
}