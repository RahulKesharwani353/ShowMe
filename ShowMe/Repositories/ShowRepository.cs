using Microsoft.EntityFrameworkCore;
using ShowMe.Data;
using ShowMe.Interface;
using ShowMe.Models;

namespace ShowMe.Repositories;

public class ShowRepository : IShowRepository {
	private readonly DataContext _context;

	public ShowRepository(DataContext context) {
		_context = context;
	}

	public bool CreateShow(Show show) {
		_context.Add(show);
		return Save();
	}

	public Show GetShow(Guid id) {
		return _context.Shows.FirstOrDefault(p => p.Id == id);
	}

	public ICollection<Show> GetShows() {
		return _context.Shows
			.Include(p => p.Movie)
			.Include(p => p.Screen)
			.ThenInclude(p => p.Theater)
			.ToList();
	}

	public bool Save() {
		return _context.SaveChanges() > 0;
	}
}