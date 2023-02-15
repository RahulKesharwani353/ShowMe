using Microsoft.EntityFrameworkCore;
using ShowMe.Data;
using ShowMe.Interface;
using ShowMe.Models;

namespace ShowMe.Repositories;

public class TheaterRepository : ITheaterRepository {
	private readonly DataContext _context;

	public TheaterRepository(DataContext context) {
		_context = context;
	}

	public bool CreateTheaters(Theater theater) {
		_context.Add(theater);
		return Save();
	}

	public Theater GetTheater(Guid id) {
		return _context.Theaters.FirstOrDefault(p => p.Id == id);
	}

	public ICollection<object> GetTheaterMovies(Guid theaterId) {
		return _context.Shows
			.Include(p => p.Movie)
			.Include(p => p.Screen)
			.ThenInclude(p => p.Theater)
			.Where(p => p.Screen.Theater.Id == theaterId)
			.Select(p => new {
				TheaterId = p.Screen.Theater.Id,
				Theater = p.Screen.Theater.Name,
				Movies = new {
					Id = p.Movie.Id,
					Title = p.Movie.Title,
					Discription = p.Movie.Description,
					Director = p.Movie.Director,
					ReleaseDate = p.Movie.ReleaseDate
				}
			}).Cast<object>().ToList();
	}

	public ICollection<Theater> GetTheaters() {
		return _context.Theaters
			.Include(p => p.Screens)
			.OrderBy(p => p.Name)
			.ToList();
	}

	public ICollection<object> GetTheatersByCity(string city) {
		return _context.Theaters
			.Where(p => p.City.ToLower().Contains(city.ToLower()))
			.Select(p => new {
				Id = p.Id,
				Name = p.Name,
				City = p.City,
				TotalScreens = p.Screens.Count
			}).Cast<object>().ToList();
	}

	public bool Save() {
		return _context.SaveChanges() > 0;
	}
}