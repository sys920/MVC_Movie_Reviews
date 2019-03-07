using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_EX1_MovieReviews.Models.ViewModels
{
    public class CreateMovieViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public string Category { get; set; }

    }
}