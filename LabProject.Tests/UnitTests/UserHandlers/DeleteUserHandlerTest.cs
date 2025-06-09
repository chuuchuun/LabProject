using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using LabProject.Application.Features.Users.Commands;
using LabProject.Domain.Interfaces;
using Moq;
using Xunit;

namespace LabProject.Tests.UnitTests.UserHandlers
{
    public class DeleteUserHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepoMock = new();

        [Fact]
        public async Task Handle_ValidId_ReturnsTrue()
        {
            long userId = 1;
            var command = new DeleteUserCommand(userId);

            _userRepoMock.Setup(r => r.DeleteAsync(userId)).ReturnsAsync(true);

            var handler = new DeleteUserHandler(_userRepoMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeTrue();
            _userRepoMock.Verify(r => r.DeleteAsync(userId), Times.Once);
        }

        [Fact]
        public async Task Handle_UserNotFound_ReturnsFalse()
        {
            long userId = 999;
            var command = new DeleteUserCommand(userId);

            _userRepoMock.Setup(r => r.DeleteAsync(userId)).ReturnsAsync(false);

            var handler = new DeleteUserHandler(_userRepoMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeFalse();
            _userRepoMock.Verify(r => r.DeleteAsync(userId), Times.Once);
        }
    }
}
