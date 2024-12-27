using Microsoft.EntityFrameworkCore;
using SpaceReservation.Application.Data;
using SpaceReservation.Application.Services;
using SpaceReservation.Domain.Entities;

namespace SpaceReservation.UnitTests.Services;

public class ReservationServiceTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly ReservationService _service;
    private readonly List<Reservation> _reservations;

    public ReservationServiceTests()
    {
        _reservations = new List<Reservation>
        {
            new() { Id = 1, SpaceId = 1, UserId = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) },
            new() { Id = 2, SpaceId = 2, UserId = 2, StartTime = DateTime.Now.AddHours(2), EndTime = DateTime.Now.AddHours(3) }
        };

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _context.Reservations.AddRange(_reservations);
        _context.SaveChanges();

        _service = new ReservationService(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task GetAllReservationsAsync_ShouldReturnAllReservations()
    {
        // Act
        var result = await _service.GetAllReservationsAsync();

        // Assert
        Assert.Equal(_reservations.Count, result.Count());
    }

    [Fact]
    public async Task GetReservationByIdAsync_ShouldReturnReservation_WhenExists()
    {
        // Arrange
        var reservationId = 1;

        // Act
        var result = await _service.GetReservationByIdAsync(reservationId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(reservationId, result.Id);
    }

    [Fact]
    public async Task CreateReservationAsync_ShouldAddReservation()
    {
        // Arrange
        var newReservation = new Reservation
        {
            SpaceId = 3,
            UserId = 3,
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(1).AddHours(1)
        };

        // Act
        var result = await _service.CreateReservationAsync(newReservation);

        // Assert
        Assert.NotEqual(0, result.Id);
        var savedReservation = await _context.Reservations.FindAsync(result.Id);
        Assert.NotNull(savedReservation);
    }

    [Fact]
    public async Task UpdateReservationAsync_ShouldReturnTrue_WhenReservationExists()
    {
        // Arrange
        var reservation = await _context.Reservations.FirstAsync();
        reservation.StartTime = DateTime.Now.AddDays(2);

        // Act
        var result = await _service.UpdateReservationAsync(reservation);

        // Assert
        Assert.True(result);
        var updatedReservation = await _context.Reservations.FindAsync(reservation.Id);
        Assert.NotNull(updatedReservation);
        Assert.Equal(reservation.StartTime, updatedReservation!.StartTime);
    }

    [Fact]
    public async Task DeleteReservationAsync_ShouldReturnTrue_WhenReservationExists()
    {
        // Arrange
        var reservationId = 1;

        // Act
        var result = await _service.DeleteReservationAsync(reservationId);

        // Assert
        Assert.True(result);
        var deletedReservation = await _context.Reservations.FindAsync(reservationId);
        Assert.Null(deletedReservation);
    }
}