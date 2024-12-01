
namespace ReservationSystem.Models;

public class SportObject
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Location {get; set;}
    public int Capacity {get; set;}

    public ICollection<Reservation> Reservations {get; set;}

    public SportObject(){
        Reservations = [];
    }
}