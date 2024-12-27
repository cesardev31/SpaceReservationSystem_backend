using Microsoft.EntityFrameworkCore;
using SpaceReservation.Application.Data;
using SpaceReservation.Domain.Entities;

namespace SpaceReservation.Application.Services;

public interface IReservationService
{
    Task<IEnumerable<Reservation>> GetAllReservationsAsync();
    Task<Reservation?> GetReservationByIdAsync(int id);
    Task<Reservation> CreateReservationAsync(Reservation reservation);
    Task<bool> UpdateReservationAsync(Reservation reservation);
    Task<bool> DeleteReservationAsync(int id);
}

public class ReservationService : IReservationService
{
    private readonly ApplicationDbContext _context;

    public ReservationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
    {
        return await _context.Reservations.ToListAsync();
    }

    public async Task<Reservation?> GetReservationByIdAsync(int id)
    {
        return await _context.Reservations.FindAsync(id);
    }

    public async Task<Reservation> CreateReservationAsync(Reservation reservation)
    {
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
        return reservation;
    }

    public async Task<bool> UpdateReservationAsync(Reservation reservation)
    {
        var exists = await _context.Reservations.AnyAsync(r => r.Id == reservation.Id);
        if (!exists) return false;

        _context.Entry(reservation).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteReservationAsync(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null) return false;

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
        return true;
    }
}