using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketingSystem.Models.ViewModels
{
    public class MovieGenreViewModel
    {

        public string MovieGenre { get; set; }
        public string SearchString { get; set; }
        [DataType(DataType.Date)]
        public DateTime SearchDate { get; set; }
        public List<Movie> Movies;
        public SelectList Genres;
    }
}
