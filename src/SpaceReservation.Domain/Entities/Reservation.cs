namespace SpaceReservation.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int SpaceId { get; set; }
    public int UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}