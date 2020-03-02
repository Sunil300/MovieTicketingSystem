using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketingSystem.Models.ViewModels
{
    public class MovieKeywordViewModel
    {

        public string KeyWord { get; set; }

        public List<Movie> Movies;

    }
}
