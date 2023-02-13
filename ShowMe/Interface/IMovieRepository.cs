using System;
using ShowMe.Models;

namespace ShowMe.Interface
{
	public interface IMovieRepository
	{
		ICollection<Movie> GetMovies();
		Movie GetMovie(int id);
		Movie GetMovie(String Name);
		bool CreateMovie(Movie movie);
	}
}

