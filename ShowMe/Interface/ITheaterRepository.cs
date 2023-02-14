using System;
using ShowMe.Models;

namespace ShowMe.Interface
{
	public interface ITheaterRepository
	{
        ICollection<Theater> GetTheaters();
        Theater GetTheater(Guid id);
        bool CreateTheaters(Theater theater);
    }
}

