using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using LabProject.Application.Dtos.UserDtos;
using LabProject.Application.Features.Users.Queries;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using Moq;
using Xunit;

namespace LabProject.Tests.UnitTests.UserHandlers
{
    public class GetProviderBySpecialtyIdHandlerTest
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly IMapper _mapper;
        private readonly GetProviderBySpecialtyIdQueryHandler _handler;

        public GetProviderBySpecialtyIdHandlerTest()
        {
            _userRepoMock = new Mock<IUserRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserProviderDto>();
            });

            _mapper = config.CreateMapper();

            _handler = new GetProviderBySpecialtyIdQueryHandler(_userRepoMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnMappedProviders_WhenSpecialtyHasProviders()
        {
            long specialtyId = 1;
            var users = new List<User>
            {
                TestHelpers.BasicUserWithId(1),
                TestHelpers.BasicUserWithId(2),
            };
            _userRepoMock.Setup(repo => repo.GetProvidersBySpecialtyAsync(specialtyId))
                         .ReturnsAsync(users);
            var query = new GetProviderBySpecialtyId(specialtyId);

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull().And.HaveCount(2);
            result.Should().AllBeAssignableTo<UserProviderDto>();
            result.Should().Contain(r => r!.Name == "Anna");
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoProvidersFound()
        {
            long specialtyId = 99;
            _userRepoMock.Setup(repo => repo.GetProvidersBySpecialtyAsync(specialtyId))
                         .ReturnsAsync(new List<User>());
            var query = new GetProviderBySpecialtyId(specialtyId);

            var result = await _handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull().And.BeEmpty();
        }
    }
}
