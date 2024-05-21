using Application.Client.Create;
using Common.Logging;
using Domain.Dto;
using Domain.Entities;
using FluentValidation;
using LanguageExt.Common;
using Mapster;

namespace Application.Commands
{
    public record CreateUserCommand(string Name, string Email,string Password,string Address) : ICommand<UserDto>;

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email Address not valid");
        }
    }
    public class CreateNewUserCommandHandler : ICommandHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoggerAdapter<CreateNewUserCommandHandler> _loggerAdapter;

        public CreateNewUserCommandHandler(IUserRepository userRepository, ILoggerAdapter<CreateNewUserCommandHandler> loggerAdapter)
        {
            this._userRepository = userRepository;
            this._loggerAdapter = loggerAdapter;
        }
        public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                var users = Users.MapFrom(request);
                users.Id = Guid.NewGuid().ToString();
                await _userRepository.AddAsync(users, cancellationToken);
                await _userRepository.SaveChangesAsync();

                return request.Adapt<UserDto>();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
