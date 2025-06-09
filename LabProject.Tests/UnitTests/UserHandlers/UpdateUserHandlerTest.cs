using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Application.Features.Users.Commands;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using Moq;
using Xunit;

namespace LabProject.Tests.UnitTests.UserHandlers
{
    public class UpdateUserHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepoMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        [Fact]
        public async Task Handle_UserExists_UpdatesAndReturnsTrue()
        {
            var userId = 1;
            var dto = TestHelpers.CorrectUserUpdateDto();
            var existingUser = TestHelpers.BasicUser();
            _userRepoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(existingUser);
            _userRepoMock.Setup(r => r.UpdateAsync(userId, It.IsAny<User>())).ReturnsAsync(true);

            var handler = new UpdateUserHandler(_userRepoMock.Object, _mapperMock.Object);
            var result = await handler.Handle(new UpdateUserCommand(userId, dto), CancellationToken.None);

            result.Should().BeTrue();
            _userRepoMock.Verify(r => r.GetByIdAsync(userId), Times.Once);
            _mapperMock.Verify(m => m.Map(dto, existingUser), Times.Once);
            _userRepoMock.Verify(r => r.UpdateAsync(userId, existingUser), Times.Once);
            existingUser.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }

        [Fact]
        public async Task Handle_UserNotFound_ReturnsFalse()
        {
            var userId = 999;
            var dto = TestHelpers.CorrectUserUpdateDto();

            _userRepoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User)null);

            var handler = new UpdateUserHandler(_userRepoMock.Object, _mapperMock.Object);

            var result = await handler.Handle(new UpdateUserCommand(userId, dto), CancellationToken.None);

            result.Should().BeFalse();
            _mapperMock.Verify(m => m.Map(It.IsAny<UserUpdateDto>(), It.IsAny<User>()), Times.Never);
            _userRepoMock.Verify(r => r.UpdateAsync(It.IsAny<long>(), It.IsAny<User>()), Times.Never);
        }
    }
}
