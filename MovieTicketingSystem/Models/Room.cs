using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketingSystem.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public String RoomNo { get; set; }
        public int SeatAmount { get; set; }
        public String Comment { get; set; }
        public ICollection<Playing> Playings { get; set; }
        public int rows { get; set; }
        public int columns { get; set; }
    }
}
