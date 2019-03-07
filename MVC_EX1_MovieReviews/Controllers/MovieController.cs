using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_EX1_MovieReviews.Models.Domain;
using MVC_EX1_MovieReviews.Models.ViewModels;

namespace MVC_EX1_MovieReviews.Controllers
{
    public class MovieController : Controller
    {
        public static List<Movie> MovieInMemoryDatabase { get; set; }
           = new List<Movie>();

   
        public ActionResult Index()
        {
            var viewModel = MovieInMemoryDatabase.Select(movie => new IndexMovieViewModel
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Rating = movie.Rating,
                    Category = movie.Category

                }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            PopulateViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateMovieViewModel inputFormData)
        {
            if (!ModelState.IsValid)
            {
                PopulateViewBag();
                return View();
            }

            if (MovieInMemoryDatabase.Any(p => p.Name == inputFormData.Name))
            {
                ModelState.AddModelError(nameof(CreateMovieViewModel.Name), "Message for error");
            }

            var newMovie = new Movie();
            Random random = new Random();
            int randomNumber = random.Next();
            newMovie.Id = randomNumber;
            newMovie.Name = inputFormData.Name;
            newMovie.Rating = inputFormData.Rating;
            newMovie.Category = inputFormData.Category;

            //Saving to the List

            MovieInMemoryDatabase.Add(newMovie);



            return Redirect(nameof(MovieController.Index));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var movie = (from Movie in MovieInMemoryDatabase
                              where Movie.Id == id

                         select Movie).FirstOrDefault();

            PopulateViewBag();

            var model = new IndexMovieViewModel();

            model.Id = movie.Id;
            model.Name = movie.Name;
            model.Rating = movie.Rating;
            model.Category = movie.Category;


            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(IndexMovieViewModel editFormData)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var movie = (from Movie in MovieInMemoryDatabase
                         where Movie.Id == editFormData.Id
                         select Movie).FirstOrDefault();

            movie.Name = editFormData.Name;
            movie.Rating = editFormData.Rating;
            movie.Category = editFormData.Category;

           


            return RedirectToAction (nameof(MovieController.Index));
        }


        public ActionResult Delete(int id)
        {
            var MovieQuery = (from Movie in MovieInMemoryDatabase
                              where Movie.Id == id
                              select Movie).FirstOrDefault();

            MovieInMemoryDatabase.Remove(MovieQuery);

            return RedirectToAction(nameof(MovieController.Index));
        }

        private void PopulateViewBag()
        {
            var categories = new SelectList(
                                  new List<string>
                                  {
                                      "Drama",
                                      "Comedy",
                                      "Horror",
                                      "Romance",
                                      "Sci-fi",
                                      "Adventure"
                                  });

            ViewBag.Categories = categories;


        }
    
    }
}