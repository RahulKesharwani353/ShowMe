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

        public Movie GetMovieByName(string Name)
        {
            throw new NotImplementedException();
        }

        public ICollection<Movie> GetMovies() => _context.Movies.OrderBy(p => p.Title).ToList();
    }
}

