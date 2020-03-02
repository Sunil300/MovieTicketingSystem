using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketingSystem.Models
{
    public class Ticket
    {
        [Key]
       
        public int TicketId { get; set; }
        
        public int PlayingId { get; set; }
       
        public String SeatNo { get; set; }

        public Playing Playing { get; set; }

        public String PhoneNum { get; set; }

        public String Email { get; set; }

    } 
}
