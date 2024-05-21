using Application.Commands;
using Irock.POTrackingSolution.Api.Endpoints.Base;
using Domain.Dto;
using MediatR;

namespace Irock.POTrackingSolution.Api.Endpoints.Users
{
    public class CreateUserEndPoint : MyEndpoint<CreateUserCommand, UserDto>
    {
        private readonly IMediator _mediator;
        public CreateUserEndPoint(IMediator mediator) : base(mediator)
        {
            this._mediator = mediator;
        }
        public override void Configure()
        {
            Post("/user");
            AllowAnonymous();
        }
    }
}
