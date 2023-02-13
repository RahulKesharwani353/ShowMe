using System;
using ShowMe.Models;

namespace ShowMe.Migrations
{
	public interface ITheaterRepository
	{
        ICollection<Theater> GetTheaters();
        bool CreateTheaters(Theater theater);
    }
}

