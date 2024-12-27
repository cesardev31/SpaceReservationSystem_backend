using Microsoft.AspNetCore.Mvc;
using Moq;
using SpaceReservation.Application.Services;
using SpaceReservation.Domain.Entities;
using SpaceReservation.API.Controllers;

namespace SpaceReservation.UnitTests.Controllers;

public class ReservationsControllerTests
{
    private readonly Mock<IReservationService> _mockService;
    private readonly ReservationsController _controller;
    private readonly List<Reservation> _reservations;

    public ReservationsControllerTests()
    {
        _mockService = new Mock<IReservationService>();
        _controller = new ReservationsController(_mockService.Object);

        _reservations = new List<Reservation>
        {
            new() { Id = 1, SpaceId = 1, UserId = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) },
            new() { Id = 2, SpaceId = 2, UserId = 2, StartTime = DateTime.Now.AddHours(2), EndTime = DateTime.Now.AddHours(3) }
        };
    }

    [Fact]
    public async Task GetAllReservations_ShouldReturnOkResult()
    {
        // Arrange
        _mockService.Setup(s => s.GetAllReservationsAsync())
            .ReturnsAsync(_reservations);

        // Act
        var result = await _controller.GetAllReservations();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<Reservation>>(okResult.Value);
        Assert.Equal(_reservations.Count, returnValue.Count());
    }

    [Fact]
    public async Task GetReservationById_ShouldReturnOkResult_WhenReservationExists()
    {
        // Arrange
        var reservationId = 1;
        var reservation = _reservations.First(r => r.Id == reservationId);
        _mockService.Setup(s => s.GetReservationByIdAsync(reservationId))
            .ReturnsAsync(reservation);

        // Act
        var result = await _controller.GetReservationById(reservationId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Reservation>(okResult.Value);
        Assert.Equal(reservationId, returnValue.Id);
    }

    [Fact]
    public async Task GetReservationById_ShouldReturnNotFound_WhenReservationDoesNotExist()
    {
        // Arrange
        var reservationId = 999;
        _mockService.Setup(s => s.GetReservationByIdAsync(reservationId))
            .ReturnsAsync((Reservation)null);

        // Act
        var result = await _controller.GetReservationById(reservationId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateReservation_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var newReservation = new Reservation
        {
            SpaceId = 3,
            UserId = 3,
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(1).AddHours(1)
        };

        _mockService.Setup(s => s.CreateReservationAsync(It.IsAny<Reservation>()))
            .ReturnsAsync((Reservation r) => { r.Id = 3; return r; });

        // Act
        var result = await _controller.CreateReservation(newReservation);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(ReservationsController.GetReservationById), createdAtActionResult.ActionName);
        var returnValue = Assert.IsType<Reservation>(createdAtActionResult.Value);
        Assert.Equal(3, returnValue.Id);
    }

    [Fact]
    public async Task UpdateReservation_ShouldReturnNoContent_WhenUpdateSucceeds()
    {
        // Arrange
        var reservation = _reservations[0];
        _mockService.Setup(s => s.UpdateReservationAsync(It.IsAny<Reservation>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.UpdateReservation(reservation.Id, reservation);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateReservation_ShouldReturnNotFound_WhenReservationDoesNotExist()
    {
        // Arrange
        var reservation = new Reservation { Id = 999 };
        _mockService.Setup(s => s.UpdateReservationAsync(It.IsAny<Reservation>()))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.UpdateReservation(reservation.Id, reservation);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateReservation_ShouldReturnBadRequest_WhenIdsDoNotMatch()
    {
        // Arrange
        var reservation = _reservations[0];
        var wrongId = reservation.Id + 1;

        // Act
        var result = await _controller.UpdateReservation(wrongId, reservation);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteReservation_ShouldReturnNoContent_WhenDeleteSucceeds()
    {
        // Arrange
        var reservationId = 1;
        _mockService.Setup(s => s.DeleteReservationAsync(reservationId))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteReservation(reservationId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteReservation_ShouldReturnNotFound_WhenReservationDoesNotExist()
    {
        // Arrange
        var reservationId = 999;
        _mockService.Setup(s => s.DeleteReservationAsync(reservationId))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteReservation(reservationId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}