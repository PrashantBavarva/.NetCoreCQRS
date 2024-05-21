using Application.Commands;
using Application.Exceptions;
using Application.Integration.Tests.Commands.Base;
using Application.Integration.Tests.Factories;
using Common.Extensions;
using FluentAssertions;
namespace Application.Integration.Tests.Commands.Providers.Update
{
    public class UpdateCommandTest : CommandBase
    {
        public UpdateCommandTest(BHFPOTrackingSolutionApiFactory factory) : base(factory)
        {

        }
        [Fact]
        public async Task CreateUser_ShouldSuccess_WhenRequestIsValid()
        {
            //Arrange
            var command = new CreateUserCommand("Test", "Test@gmail.com", "Test", "Test");
            //Act
            var result = await Sender.Send(command, CancellationToken.None);
            //Assert
           
            result.IsSuccess.Should().BeTrue();
            result.Value().Name.Should().Be("Test");
        }

        [Fact]
        public async Task CreateUser_ShouldFaild_WhenProviderNotExist()
        {
            //Arrange
            var command = new CreateUserCommand("", "", "", "");
            //Act
            var result = await Sender.Send(command, CancellationToken.None);
            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error().Should().BeOfType<ApiException>();
        }

        [Fact]
        public async Task CreateUser_ShouldFail_WhenEmailIsInValid()
        {
            //Arrange
            var command = new CreateUserCommand("Test", "test", "Test", "Test@321123");
            //Act
            var result = await Sender.Send(command, CancellationToken.None);
            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error().Should().BeOfType<ApiException>();
        }

    }
}
