using System;
using ShowMe.Models;

namespace ShowMe.Interface
{
	public interface IMovieRepository
	{
		ICollection<Movie> GetMovies();
		Movie GetMovie(Guid id);
		Movie GetMovieByName(String Name);
		bool CreateMovie(Movie movie);
	}
}

