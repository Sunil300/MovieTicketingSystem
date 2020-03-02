using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketingSystem.Models.ViewModels
{
    public class PlayingSeatsViewModel
    {
        [Key]
        public int PlayingId { get; set; }

        public Playing Playing { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

        public List<String> Seats { get; set; }
    }
}
