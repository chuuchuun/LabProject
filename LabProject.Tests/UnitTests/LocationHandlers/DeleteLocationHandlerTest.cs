using System.Threading;
using System.Threading.Tasks;
using LabProject.Application.Features.Locations.Commands;
using LabProject.Domain.Entities;
using LabProject.Domain.Interfaces;
using Moq;
using Xunit;

namespace LabProject.Tests.UnitTests.LocationHandlers
{
    public class DeleteLocationHandlerTests
    {
        private readonly Mock<IRepository<Location>> _mockRepo;
        private readonly DeleteLocationHandler _handler;

        public DeleteLocationHandlerTests()
        {
            _mockRepo = new Mock<IRepository<Location>>();
            _handler = new DeleteLocationHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ValidId_ReturnsTrue()
        {
            long locationId = 1;
            _mockRepo.Setup(r => r.DeleteAsync(locationId)).ReturnsAsync(true);
            var command = new DeleteLocationCommand(locationId);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result);
            _mockRepo.Verify(r => r.DeleteAsync(locationId), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsFalse()
        {
            long locationId = 99;
            _mockRepo.Setup(r => r.DeleteAsync(locationId)).ReturnsAsync(false);
            var command = new DeleteLocationCommand(locationId);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result);
            _mockRepo.Verify(r => r.DeleteAsync(locationId), Times.Once);
        }
    }
}
