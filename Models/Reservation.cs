using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationSystem.Models;

public class Reservation
{
    [Display(Name = "Number")]
    public int ID {get; set;}

    public DateTime Date {get; set;}

    public DateTime ReservationDate {get; set;}

    public bool Aproved {get; set;} = false;

    public int DurationInHours {get; set;}

    public String UserId {get; set;}
    public ApplicationUser? User {get; set;}

    public int SportObjectID {get; set;}
    public SportObject? SportObject {get; set;}
}