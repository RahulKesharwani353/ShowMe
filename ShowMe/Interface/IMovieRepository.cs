using System;
using ShowMe.Models;

namespace ShowMe.Interface
{
	public interface IMovieRepository
	{
		ICollection<Movie> GetMovies();
		Movie GetMovie(Guid id);
		object getMovieDetails(Guid id);
        ICollection<object> getMovieShows(Guid MovieId, Guid TheaterId);
        ICollection<object> GetMovieByName(String Name);
		bool CreateMovie(Movie movie);
	}
}

