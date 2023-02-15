using ShowMe.Models;

namespace ShowMe.Interface;

public interface IShowRepository {
	// Get
	ICollection<Show> GetShows();
	Show GetShow(Guid id);

	// Create
	bool CreateShow(Show show);

	bool Save();
}