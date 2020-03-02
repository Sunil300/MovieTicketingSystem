using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketingSystem.Models
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }
        public string Name { get; set; }
    }
}
