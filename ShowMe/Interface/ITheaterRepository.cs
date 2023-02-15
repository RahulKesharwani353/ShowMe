using ShowMe.Models;

namespace ShowMe.Interface;

public interface ITheaterRepository {
	// Get
	ICollection<Theater> GetTheaters();
	Theater GetTheater(Guid theaterId);
	ICollection<object> GetTheaterMovies(Guid theaterId);
	ICollection<object> GetTheatersByCity(string city);

	// Create
	bool CreateTheaters(Theater theater);

	bool Save();
}
