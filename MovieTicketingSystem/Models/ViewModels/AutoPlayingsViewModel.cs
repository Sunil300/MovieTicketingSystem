using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketingSystem.Models.ViewModels
{
    public class AutoPlayingsViewModel
    {
        [DataType(DataType.Date)]
        public DateTime PlayingDateFrom { get; set; }
        [DataType(DataType.Date)]
        public DateTime PlayingDateTo { get; set; }
        public List<int> MovieIds { get; set; }
    }
}
