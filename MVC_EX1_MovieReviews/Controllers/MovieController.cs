using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MVC_EX1_MovieReviews.Models;
using MVC_EX1_MovieReviews.Models.Domain;
using MVC_EX1_MovieReviews.Models.ViewModels;

namespace MVC_EX1_MovieReviews.Controllers
{
    public class MovieController : Controller
    {

        private ApplicationDbContext DbContext;

        public MovieController()
        {
            DbContext = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var model = DbContext.Movies
                .Where(p => p.UserId == userId)
                .Select(p => new IndexMovieViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Rating = p.Rating,
                    Category = p.Category

                }).ToList();

            return View(model);
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
            return SaveMovie(null, inputFormData);
        }

        [HttpPost]
        public ActionResult SaveMovie(int? id, CreateMovieViewModel inputFormData)
        {
            if (!ModelState.IsValid)
            {
                PopulateViewBag();
                return View();
            }

            var userId = User.Identity.GetUserId();

            

            //if (DbContext.Movies.Any(p => p.UserId == userId &&
            //      p.Name == inputFormData.Name &&
            //      (!id.HasValue || p.Id != id.Value)))
            //{
            //    ModelState.AddModelError(nameof(CreateMovieViewModel.Name),
            //        "Movie name should be unique");

            //    PopulateViewBag();
            //    return View();
            //}

            Movie movie;

            if (!id.HasValue)
            {
                movie = new Movie();
                movie.UserId = userId;
                DbContext.Movies.Add(movie);
            }
            else
            {
                movie = DbContext.Movies.FirstOrDefault(
               p => p.Id == id);

                if (movie == null)
                {
                    return RedirectToAction(nameof(MovieController.Index));
                }
            }



            movie.Name = inputFormData.Name;
            movie.Rating = inputFormData.Rating;
            movie.Category = inputFormData.Category;
            
            //Saving to the List

            DbContext.SaveChanges();



            return RedirectToAction(nameof(MovieController.Index));
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(MovieController.Index));
            }

            var userId = User.Identity.GetUserId();


            var movie = DbContext.Movies.FirstOrDefault(
                p => p.Id == id && p.UserId == userId);


            if (movie == null)
            {
                return RedirectToAction(nameof(MovieController.Index));
            }


            PopulateViewBag();

            var model = new CreateMovieViewModel();
            model.Name = movie.Name;
            model.Rating = movie.Rating;
            model.Category = movie.Category;

            
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, CreateMovieViewModel inputFormData)
        {
            return SaveMovie(id, inputFormData);
        }


        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(MovieController.Index));
            }

            var userId = User.Identity.GetUserId();

            var movie = DbContext.Movies.FirstOrDefault(p => p.Id == id && p.UserId == userId);

            if (movie != null)
            {
                DbContext.Movies.Remove(movie);
                DbContext.SaveChanges();
            }


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