using Microsoft.EntityFrameworkCore;
using ShowMe.Data;
using ShowMe.Interface;
using ShowMe.Models;

namespace ShowMe.Repositories;

public class MovieRepository : IMovieRepository {
	private readonly DataContext _context;

	public MovieRepository(DataContext context) {
		_context = context;
	}

	public bool CreateMovie(Movie movie) {
		_context.Add(movie);
		return Save();
	}

	public Movie GetMovie(Guid id) {
		return (from m in _context.Movies
				  where m.Id == id
				  select m).FirstOrDefault();
	}

	public ICollection<object> GetMovieTheaters(Guid id) {
		return _context.Shows
		.Include(x => x.Screen)
		.ThenInclude(x => x.Theater)
		.Include(x => x.Movie)
		.Where(x => x.Movie.Id == id)
		.Select(x => new {
			Id = x.Screen.Theater.Id,
			Title = x.Movie.Title,
			Theater = x.Screen.Theater.Name
		}).Distinct()
		.Cast<object>().ToList();

		// return (from s in _context.Shows
		// 		  join m in _context.Movies on s.Movie.Id equals m.Id
		// 		  join scr in _context.Screens on s.Screen.Id equals scr.Id
		// 		  join t in _context.Theaters on scr.Theater.Id equals t.Id
		// 		  where m.Id == id
		// 		  select new { m.Id, m.Title, t.Name }).Cast<object>().ToList();
	}

	public ICollection<object> GetMovieShows(Guid MovieId, Guid TheaterId) {
		return _context.Shows
			.Include(p => p.Screen)
			.ThenInclude(p => p.Theater)
			.Include(p => p.Movie)
			.Where(p => p.Movie.Id == MovieId && p.Screen.Theater.Id == TheaterId)
			.Select(p => new {
				Id = p.Id,
				StartTime = p.StartTime,
				Date = p.Date,
				Theater = p.Screen.Theater.Name,
				Screen = new {
					Id = p.Screen.Id,
					Name = p.Screen.Name
				},
				Movie = new {
					Id = p.Movie.Id,
					Title = p.Movie.Title
				}
			}).Cast<object>().ToList();
	}

	public ICollection<object> GetMoviesByName(string searchQuery) {
		return _context.Movies
			.Where(p => p.Title.ToLower().Contains(searchQuery.ToLower()))
			.Select(p => new {
				Id = p.Id,
				Title = p.Title,
				Director = p.Director,
				Description = p.Description
			})
			.OrderBy(p => p.Title).Cast<object>().ToList();
	}

	public ICollection<Movie> GetMovies() {
		return _context.Movies.OrderBy(p => p.Title).ToList();
	}

	public bool Save() {
		return _context.SaveChanges() > 0;
	}
}