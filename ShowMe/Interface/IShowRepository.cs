using System;
using ShowMe.Models;

namespace ShowMe.Interface
{
	public interface IShowRepository
	{
		ICollection<Show> GetShows();
		Show GetShow(Guid id);
		bool CreateShow(Show show);
	}
}

