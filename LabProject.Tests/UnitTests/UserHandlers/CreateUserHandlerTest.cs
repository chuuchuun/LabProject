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
    public class CreateLocationHandlerTest
    {
        private readonly Mock<IUserRepository> _userRepoMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IRoleRepository> _roleRepoMock = new();
        [Fact]
        public async Task Handle_ValidDto_ReturnsNewUserId()
        {
            var dto = TestHelpers.CorrectUserCreateDto();
            var command = new CreateUserCommand(dto);
            var user = TestHelpers.BasicUser();
            _mapperMock.Setup(m => m.Map<User>(dto)).Returns(user);
            _userRepoMock.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync(123L);
            var handler = new CreateUserHandler(_userRepoMock.Object, _mapperMock.Object, _roleRepoMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().Be(123);
            _mapperMock.Verify(m => m.Map<User>(dto), Times.Once);
            _userRepoMock.Verify(r => r.AddAsync(It.Is<User>(u =>
                u.PasswordHash != null &&
                u.CreatedAt != default &&
                u.UpdatedAt != default
            )), Times.Once);
        }

        [Fact]
        public async Task Handle_EmptyPassword_ThrowsArgumentException()
        {
            var dto = TestHelpers.IncorrectUserCreateDtoWithNoPassword();
            var command = new CreateUserCommand(dto);
            var handler = new CreateUserHandler(_userRepoMock.Object, _mapperMock.Object, _roleRepoMock.Object);

            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            await act.Should()
                .ThrowAsync<ArgumentException>()
                .WithMessage("Password cannot be null or empty");

            _mapperMock.Verify(m => m.Map<User>(It.IsAny<UserCreateDto>()), Times.Never);
            _userRepoMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
        }
    }

}
