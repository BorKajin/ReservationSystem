using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using ReservationSystem.Models;

namespace ReservationSystem.Data;

class DbInitializer
{
    public static void Initialize(ReservationContext context){
        context.Database.EnsureCreated();
        // Look for any users.
        if (context.Users.Any())
        {
            return;   // DB has been seeded
        }
        var user = new ApplicationUser
        {
            FirstName = "Bob",
            LastName = "Dilon",
            Email = "bob@example.com",
            NormalizedEmail = "XXXX@EXAMPLE.COM",
            UserName = "bob@example.com",
            NormalizedUserName = "bob@example.com",
            PhoneNumber = "+111111111111",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };
        if (!context.Users.Any(u => u.UserName == user.UserName))
        {
            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(user,"Testni123!");
            user.PasswordHash = hashed;
            context.Users.Add(user);
            
        }
        context.SaveChanges();
        var roles = new IdentityRole[] {
            new IdentityRole{Id="1", Name="Administrator"},
            new IdentityRole{Id="2", Name="Manager"},
            new IdentityRole{Id="3", Name="Staff"}
        };
        foreach (IdentityRole r in roles)
        {
            context.Roles.Add(r);
        }
        var UserRoles = new IdentityUserRole<string>[]
        {
            new IdentityUserRole<string>{RoleId = roles[0].Id, UserId=user.Id},
            new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user.Id},
        };
        foreach (IdentityUserRole<string> r in UserRoles)
        {
            context.UserRoles.Add(r);
        }
        context.SaveChanges();

        var sportObjects = new SportObject[]{
            new SportObject{Name="Dvorana 1", Capacity= 1, Location="Naslov"},
            new SportObject{Name="Dvorana 2", Capacity= 2, Location="Naslov"},
            new SportObject{Name="Dvorana 3", Capacity= 3, Location="Naslov"}
        };
        foreach (SportObject s in sportObjects){
            context.SportObjects.Add(s);
        }
        context.SaveChanges();

        var reservations = new Reservation[]{
            new Reservation{ User=user, Date=DateTime.Parse("2024-11-11"), ReservationDate=DateTime.Parse("2024-12-3 13:00:00"), DurationInHours=1}
        };
        foreach (Reservation r in reservations){
            context.Reservations.Add(r);
        }
        context.SaveChanges();

    }
}