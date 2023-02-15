using System;
using Microsoft.EntityFrameworkCore;
using ShowMe.Data;
using ShowMe.Interface;
using ShowMe.Models;

namespace ShowMe.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        private readonly DataContext _context;

        public MovieRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateMovie(Movie movie)
        {
            _context.Add(movie);
            return save();
        }

        private bool save()
        {
            return _context.SaveChanges() > 0;
        }

        public Movie GetMovie(Guid id)
        {
            var movie = _context.Movies
                .FirstOrDefault(p => p.Id == id);
            return movie;
     
        }

        public object getMovieDetails(Guid id)
        {

            var movies = from s in _context.Shows
                         join m in _context.Movies on s.Movie.Id equals m.Id
                         join scr in _context.Screens on s.Screen.Id equals scr.Id
                         join t in _context.Theaters on scr.Theater.Id equals t.Id
                         where m.Id == id
                         select new { m.Id, m.Title, t.Name };

            return movies.Cast<object>().ToList();
        }

        public ICollection<object> getMovieShows(Guid MovieId, Guid TheaterId)
        {

            var shows = _context.Shows
                .Include(p => p.Screen)
                .ThenInclude(p => p.Theater)
                .Include(p => p.Movie)
                .Where(p => p.Movie.Id == MovieId && p.Screen.Theater.Id==TheaterId)
                .Select(p => new
                {
                    Id = p.Id,
                    StartTime = p.StartTime,
                    Date = p.Date,
                    Theater = p.Screen.Theater.Name,
                    Screen = new
                    {
                        Id = p.Screen.Id,
                        Name= p.Screen.Name
                    },
                    Movie = new
                    {
                        Id = p.Movie.Id,
                        Title = p.Movie.Title
                    }
                    
                })
                .ToList();

            return shows.Cast<object>().ToList();
        }

        public ICollection<object> GetMovieByName(string searchQuery)
        {
            var movieList = _context.Movies
                .Where(p => p.Title.ToLower().Contains(searchQuery.ToLower()))
                //.Where(p => p.Title.Contains(searchQuery))
                .Select(p => new
                {
                    Id = p.Id,
                    Title = p.Title,
                    Director = p.Director,
                    Description = p.Description
                })
                .OrderBy(p => p.Title)
                .ToList();
            return movieList.Cast<object>().ToList();
        }

        public ICollection<Movie> GetMovies() => _context.Movies.OrderBy(p => p.Title).ToList();
    }
}

