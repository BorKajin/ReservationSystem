using Microsoft.AspNetCore.Identity;

namespace ReservationSystem.Models;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public ICollection<Reservation> Reservations {get; set;}

    public ApplicationUser()
    {
        Reservations = [];
    }
}