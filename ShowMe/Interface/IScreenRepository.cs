using ShowMe.Models;

namespace ShowMe.Interface;

public interface IScreenRepository {
	// Get
	ICollection<Screen> GetScreens();
	Screen GetScreen(Guid id);

	// Create
	bool CreateScreen(Guid theaterId, Screen screen);

	bool Save();
}
