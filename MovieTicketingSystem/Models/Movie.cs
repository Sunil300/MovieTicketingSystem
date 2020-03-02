using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketingSystem.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public String Title { get; set; }
        public String Director { get; set; }
        public String Cast { get; set; }
        public int Length { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int RatingId { get; set; }
        public Rating Rating { get; set; }
        public String Poster { get; set; }
        public String Preview { get; set; }
        [DataType(DataType.Date)]
        public DateTime Release { get; set; }
        [DataType(DataType.Date)]
        public DateTime StopShowing { get; set; }
        public String Synopsis { get; set; }
        public Boolean IsOnCarousel { get; set; }

        public ICollection<Playing> Playings { get; set; }

    }
}
