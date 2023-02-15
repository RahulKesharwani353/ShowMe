using ShowMe.Models;

namespace ShowMe.Interface;

public interface IMovieRepository {
	// Get
	ICollection<Movie> GetMovies();
	Movie GetMovie(Guid id);
	ICollection<object> GetMovieTheaters(Guid id);
	ICollection<object> GetMovieShows(Guid MovieId, Guid TheaterId);
	ICollection<object> GetMoviesByName(string Name);

	// Create
	bool CreateMovie(Movie movie);

	bool Save();
}
