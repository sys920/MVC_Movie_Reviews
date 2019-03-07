using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_EX1_MovieReviews.Models.ViewModels
{
    public class IndexMovieViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Rating { get; set; }
        public string Category { get; set; }
    }
}