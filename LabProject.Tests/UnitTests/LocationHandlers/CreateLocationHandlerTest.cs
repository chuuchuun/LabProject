using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LabProject.Application.Dtos.LocationDtos;
using LabProject.Application.Features.Locations.Commands;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using LabProject.Tests;
using Moq;
using Xunit;

namespace LabProject.Tests.UnitTests.LocationHandlers
{
    public class CreateLocationHandlerTest
    {
        private readonly Mock<IRepository<Location>> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateLocationHandler _handler;

        public CreateLocationHandlerTest()
        {
            _mockRepo = new Mock<IRepository<Location>>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateLocationHandler(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsCreatedId()
        {
            var dto = TestHelpers.CorrectLocationCreateDto();
            var location = TestHelpers.BasicLocation();

            _mockMapper.Setup(m => m.Map<Location>(dto)).Returns(location);
            _mockRepo.Setup(r => r.AddAsync(location)).ReturnsAsync(location.Id);

            var command = new CreateLocationCommand(dto);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Equal(location.Id, result);
            _mockMapper.Verify(m => m.Map<Location>(dto), Times.Once);
            _mockRepo.Verify(r => r.AddAsync(location), Times.Once);
        }
    }
}
