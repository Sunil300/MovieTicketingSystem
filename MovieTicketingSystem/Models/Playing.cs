using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketingSystem.Models
{
    public class Playing
    {
        [Key]
        public int PlayingId { get; set; }

        public int TimeSlotId { get; set; }
        [DataType(DataType.Date)]
        public DateTime PlayingDate { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }

        public TimeSlot TimeSlot { get; set; }
        public Movie Movie { get; set; }
        public Room Room { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
