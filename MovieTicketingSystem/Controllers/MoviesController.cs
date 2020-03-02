using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTicketingSystem.Data;
using MovieTicketingSystem.Models;
using MovieTicketingSystem.Models.ViewModels;

namespace MovieTicketingSystem.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString, DateTime searchDate)
        {
            //var applicationDbContext = _context.Movie.Include(m => m.Genre).Include(m => m.Rating);
            //return View(await applicationDbContext.ToListAsync());

            // Use LINQ to get list of genres.
            IQueryable<String> genreQuery = from m in _context.Movie
                                            orderby m.Genre.Name
                                            select m.Genre.Name;

            var movies = from m in _context.Movie.Include(m => m.Genre).Include(m => m.Rating) select m;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre.Name == movieGenre);
            }

            if(searchDate != DateTime.MinValue)
            {
                movies = movies.Where(x => x.Release <= searchDate).Where(x => x.StopShowing >= searchDate);
            }

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync(),
                SearchDate = DateTime.Today
            };

            return View(movieGenreVM);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Genre)
                .Include(m => m.Rating)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "Name");
            ViewData["RatingId"] = new SelectList(_context.Rating, "RatingId", "Name");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title,Director,Cast,Length,GenreId,RatingId,Poster,Preview,Release,StopShowing,Synopsis,IsOnCarousel")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "GenreId", movie.GenreId);
            ViewData["RatingId"] = new SelectList(_context.Rating, "RatingId", "RatingId", movie.RatingId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "Name", movie.GenreId);
            ViewData["RatingId"] = new SelectList(_context.Rating, "RatingId", "Name", movie.RatingId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title,Director,Cast,Length,GenreId,RatingId,Poster,Preview,Release,StopShowing,Synopsis,IsOnCarousel")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "GenreId", movie.GenreId);
            ViewData["RatingId"] = new SelectList(_context.Rating, "RatingId", "RatingId", movie.RatingId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Genre)
                .Include(m => m.Rating)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.MovieId == id);
        }

        public async Task<IActionResult> Search(string KeyWord)
        {


            var movies = from m in _context.Movie.Include(m => m.Genre).Include(m => m.Rating) select m;

            if (!String.IsNullOrEmpty(KeyWord))
            {
                movies = movies.Where(s => s.Title.Contains(KeyWord)||s.Cast.Contains(KeyWord) || s.Director.Contains(KeyWord)||
                s.Synopsis.Contains(KeyWord));
            }

            var movieGenreVM = new MovieKeywordViewModel
            {
                KeyWord = KeyWord,
                Movies = await movies.ToListAsync()

            };

            return View(movieGenreVM);
        }

     
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                TextReader reader = new StreamReader(file.OpenReadStream());
                var csvReader = new CsvReader(reader);
                csvReader.Configuration.HeaderValidated = null;
                csvReader.Configuration.MissingFieldFound = null;
                List<Movie> records = csvReader.GetRecords<Movie>().ToList();
                foreach(Movie item in records) {
                    if (ModelState.IsValid)
                    {
                        item.GenreId = _context.Genre.Single(s => s.Name == "Action / Thriller").GenreId;
                        item.RatingId = _context.Rating.Single(s => s.Name == "M").RatingId;
                        _context.Add(item);
                    }
                    await _context.SaveChangesAsync();
                }
               

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
