using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketingSystem.Models.ViewModels
{
    public class PlayingsViewModel
    {
        [DataType(DataType.Date)]
        public DateTime SearchDate { get; set; }
        public List<Playing> Playings;
    }
}
