using System;
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
            ///Gadbadi haiii bhai........ß
            //var movies = from s in _context.Shows
            //             join m in _context.Movies on s.Movie.Id equals id
            //             join scr in _context.Screens on s.Screen.Id equals scr.Id
            //             join t in _context.Theaters on scr.Theater.Id equals t.Id
            //             select new { m.Id, m.Title, s.Date, t.Name };

            var movies = from t in _context.Theaters
                         join scr in _context.Screens on t.Id equals scr.Theater.Id
                         join s in _context.Shows on scr.Id equals s.Screen.Id
                         join m in _context.Movies on s.Movie.Id equals id
                         select new {m.Id, m.Title, t.Name};

            return movies.Cast<object>().ToList();
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

