using System;
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

public class UpdateLocationHandlerTest
{
    private readonly Mock<IRepository<Location>> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly UpdateLocationHandler _handler;

    public UpdateLocationHandlerTest()
    {
        _mockRepo = new Mock<IRepository<Location>>();
        _mockMapper = new Mock<IMapper>();
        _handler = new UpdateLocationHandler(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_LocationExists_ReturnsTrue()
    {
        var id = 1;
        var dto = TestHelpers.CorrectLocationUpdateDto();
        var location = TestHelpers.BasicLocation();
        _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(location);
        _mockMapper.Setup(m => m.Map(dto, location)); 
        _mockRepo.Setup(r => r.UpdateAsync(id, location)).ReturnsAsync(true);
        var command = new UpdateLocationCommand(id, dto);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.True(result);
        _mockRepo.Verify(r => r.UpdateAsync(id, location), Times.Once);
    }

    [Fact]
    public async Task Handle_LocationDoesNotExist_ReturnsFalse()
    {
        var id = 999;
        var dto = TestHelpers.CorrectLocationUpdateDto();
        _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Location?)null);
        var command = new UpdateLocationCommand(id, dto);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result);
        _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<long>(), It.IsAny<Location>()), Times.Never);
    }
}
