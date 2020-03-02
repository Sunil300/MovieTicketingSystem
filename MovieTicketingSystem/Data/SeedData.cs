using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MovieTicketingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketingSystem.Data
{
    public class SeedData
    {
        public static async Task SeedingAsync(IServiceProvider serviceProvider)
        {
            
            {
                //initializing custom roles   
                var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string[] roleNames = { "Admin", "User", "HR" };
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await RoleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        //create the roles and seed them to the database: Question 1  
                        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                IdentityUser user = await UserManager.FindByEmailAsync("jignesh@gmail.com");

                if (user == null)
                {
                    user = new IdentityUser()
                    {
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                    };
                    await UserManager.CreateAsync(user, "Test@123");
                }
                await UserManager.AddToRoleAsync(user, "Admin");

                
                IdentityUser user1 = await UserManager.FindByEmailAsync("user@gmail.com");

                if (user1 == null)
                {
                    user1 = new IdentityUser()
                    {
                        UserName = "user@gmail.com",
                        Email = "user@gmail.com",
                    };
                    await UserManager.CreateAsync(user1, "Test@123");
                }
                //await UserManager.AddToRoleAsync(user1, "User");



                // Look for any students.
                var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
                //context.Database.EnsureCreated();
                if (context.Genre.Any())
                {
                    return;   // DB has been seeded
                }

                var genres = new Genre[]
                {
                new Genre {Name ="Action / Thriller"},
                new Genre {Name ="Action / Adventure"}
                };

                foreach (Genre g in genres)
                {
                    context.Genre.Add(g);
                }
                context.SaveChanges();

                if (context.Rating.Any())
                {
                    return;   // DB has been seeded
                }

                var ratings = new Rating[]
                {
                new Rating {Name ="R16"},
                new Rating {Name ="PG"},
                new Rating {Name ="M"}
                };

                foreach (Rating r in ratings)
                {
                    context.Rating.Add(r);
                }
                context.SaveChanges();

                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                var movies = new Movie[]
                {
                new Movie { Title = "JOHN WICK: CHAPTER 3- PARABELLUM",   Director = "Chad Stahelski",
                    Cast = "Keanu Reeves, Ian McShane, Asia Kate Dillon" , Length = 131,
                    GenreId = genres.Single(s => s.Name == "Action / Thriller").GenreId,
                    Poster ="/images/john3wick.png", RatingId =ratings.Single(s => s.Name == "R16").RatingId,
                    Preview ="7v2P3cpPOXY", Release = new DateTime(2019,05,01), StopShowing = new DateTime(2019,05,31),
                    Synopsis ="John Wick (Keanu Reeves) is on the run for two reasons… " +
                    "he’s being hunted for a global $14 million dollar open contract on his life, " +
                    "and for breaking a central rule: taking a life on Continental Hotel grounds. " +
                    "The victim was a member of the High Table who ordered the open contract. ", IsOnCarousel = true },
                new Movie { Title = "POKEMON: DETECTIVE PIKACHU",   Director = "Rob Letterman",
                    Cast = "Ryan Reynolds, Justice Smith, Ken Watanabe, Bill Nighy" , Length = 105 ,
                    GenreId = genres.Single(s => s.Name == "Action / Adventure").GenreId,
                    Poster ="/images/pokemon.png", RatingId =ratings.Single(s => s.Name == "PG").RatingId,
                    Preview ="bILE5BEyhdo", Release = new DateTime(2019,05,01), StopShowing = new DateTime(2019,05,31),
                    Synopsis ="The world of Pokémon comes to life! The first-ever live-action Pokémon movie, " +
                    "“POKÉMON Detective Pikachu” stars Ryan Reynolds as the titular character in the first-ever " +
                    "live-action movie based on the iconic face of the global Pokémon brand—one of the world’s most popular, " +
                    "multi-generation entertainment properties and most successful media franchises of all time.", IsOnCarousel = true},
                new Movie { Title = "AVENGERS: ENDGAME",   Director = "Walt Disney Studios",
                    Cast = "Chris Evans, Robert Downey Jr, Chris Hemsworth, Scarlett Johansson, Tom Holland, Brie Larson, Mark Ruffalo, Jeremy Renner, Josh Brolin" ,
                    Length = 182, GenreId = genres.Single(s => s.Name == "Action / Adventure").GenreId,
                    Poster ="/images/avengers.png", RatingId =ratings.Single(s => s.Name == "M").RatingId,
                    Preview ="7_hP7m86LyY", Release = new DateTime(2019,05,01), StopShowing = new DateTime(2019,05,31),
                    Synopsis ="In the aftermath of Thanos wiping out half of all life in the universe, " +
                    "the Avengers must do what's necessary to undo the Mad Titan's deed." }
                };
                foreach (Movie m in movies)
                {
                    context.Movie.Add(m);
                }
                context.SaveChanges();

                if (context.TimeSlot.Any())
                {
                    return;   // DB has been seeded
                }

                var timeSlots = new TimeSlot[]
                {
                new TimeSlot {TimeFrom = new TimeSpan(09,00,00), TimeTo = new TimeSpan(11,40,00)},
                new TimeSlot {TimeFrom = new TimeSpan(12,00,00), TimeTo = new TimeSpan(14,40,00)},
                new TimeSlot {TimeFrom = new TimeSpan(15,00,00), TimeTo = new TimeSpan(17,40,00)},
                new TimeSlot {TimeFrom = new TimeSpan(18,00,00), TimeTo = new TimeSpan(20,40,00)},
                new TimeSlot {TimeFrom = new TimeSpan(21,00,00), TimeTo = new TimeSpan(23,40,00)}
                };

                foreach (TimeSlot t in timeSlots)
                {
                    context.TimeSlot.Add(t);
                }
                context.SaveChanges();

                if (context.Room.Any())
                {
                    return;   // DB has been seeded
                }

                var rooms = new Room[]
                {
                new Room {RoomNo = "D01", SeatAmount = 25, Comment = "Digital", rows= 5, columns=5},
                new Room {RoomNo = "D02", SeatAmount = 25, Comment = "Digital", rows= 5, columns=5},
                new Room {RoomNo = "D03", SeatAmount = 25, Comment = "Digital", rows= 5, columns=5},
                new Room {RoomNo = "I01", SeatAmount = 100, Comment = "IMAX", rows= 10, columns=10},
                new Room {RoomNo = "K01", SeatAmount = 10, Comment = "4K", rows= 2, columns=5}
                };

                foreach (Room r in rooms)
                {
                    context.Room.Add(r);
                }
                context.SaveChanges();

                if (context.Playing.Any())
                {
                    return;   // DB has been seeded
                }

                var playings = new Playing[]
                {
                new Playing {MovieId = 1, RoomId = 1, TimeSlotId = 1, PlayingDate = new DateTime(2019, 06, 24)},
                new Playing {MovieId = 1, RoomId = 1, TimeSlotId = 3, PlayingDate = new DateTime(2019, 06, 24)},
                new Playing {MovieId = 1, RoomId = 1, TimeSlotId = 5, PlayingDate = new DateTime(2019, 06, 24)},
                new Playing {MovieId = 1, RoomId = 4, TimeSlotId = 3, PlayingDate = new DateTime(2019, 06, 24)},
                new Playing {MovieId = 2, RoomId = 2, TimeSlotId = 2, PlayingDate = new DateTime(2019, 06, 24)},
                new Playing {MovieId = 2, RoomId = 2, TimeSlotId = 4, PlayingDate = new DateTime(2019, 06, 24)},
                new Playing {MovieId = 3, RoomId = 3, TimeSlotId = 2, PlayingDate = new DateTime(2019, 06, 24)},
                new Playing {MovieId = 3, RoomId = 3, TimeSlotId = 4, PlayingDate = new DateTime(2019, 06, 24)},
                new Playing {MovieId = 3, RoomId = 5, TimeSlotId = 3, PlayingDate = new DateTime(2019, 06, 24)},

                new Playing {MovieId = 1, RoomId = 1, TimeSlotId = 1, PlayingDate = new DateTime(2019, 06, 25)},
                new Playing {MovieId = 1, RoomId = 1, TimeSlotId = 3, PlayingDate = new DateTime(2019, 06, 25)},
                new Playing {MovieId = 1, RoomId = 1, TimeSlotId = 5, PlayingDate = new DateTime(2019, 06, 25)},
                new Playing {MovieId = 1, RoomId = 4, TimeSlotId = 3, PlayingDate = new DateTime(2019, 06, 25)},
                new Playing {MovieId = 2, RoomId = 2, TimeSlotId = 2, PlayingDate = new DateTime(2019, 06, 25)},
                new Playing {MovieId = 2, RoomId = 2, TimeSlotId = 4, PlayingDate = new DateTime(2019, 06, 25)},
                new Playing {MovieId = 3, RoomId = 3, TimeSlotId = 2, PlayingDate = new DateTime(2019, 06, 25)},
                new Playing {MovieId = 3, RoomId = 3, TimeSlotId = 4, PlayingDate = new DateTime(2019, 06, 25)},
                new Playing {MovieId = 3, RoomId = 5, TimeSlotId = 3, PlayingDate = new DateTime(2019, 06, 25)}
                };

                foreach (Playing p in playings)
                {
                    context.Playing.Add(p);
                }
                context.SaveChanges();
            }
        }
    }
}
