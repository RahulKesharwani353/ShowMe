using Microsoft.EntityFrameworkCore;
using ShowMe.Data;
using ShowMe.Interface;
using ShowMe.Models;

namespace ShowMe.Repositories;

public class ScreenRepository : IScreenRepository {
	private readonly DataContext _context;

	public ScreenRepository(DataContext context) {
		_context = context;
	}

	public bool CreateScreen(Guid theaterId, Screen screen) {
		_context.Add(screen);
		return Save();
	}

	public Screen GetScreen(Guid id) {
		return _context.Screens.Include(p => p.Theater).FirstOrDefault(p => p.Id == id);
	}

	public ICollection<Screen> GetScreens() {
		return _context.Screens.Include(p => p.Theater).ToList();
	}

	public bool Save() {
		return _context.SaveChanges() > 0;
	}
}