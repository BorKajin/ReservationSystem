using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationSystem.Models;

public class Reservation
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Display(Name = "Number")]
    public int ID {get; set;}

    public DateTime Date {get; set;}

    public DateTime ReservationDate {get; set;}

    public int DurationInHours {get; set;}

    public ApplicationUser? User {get; set;}
}