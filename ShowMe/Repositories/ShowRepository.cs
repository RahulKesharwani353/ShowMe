using System;
using Microsoft.EntityFrameworkCore;
using ShowMe.Data;
using ShowMe.Interface;
using ShowMe.Models;

namespace ShowMe.Repositories
{
	public class ShowRepository : IShowRepository
	{
        private readonly DataContext context;

        public ShowRepository(DataContext context)
		{
            this.context = context;
        }

        public bool CreateShow(Show show)
        {
             context.Add(show);
            return save();
        }

        public Show GetShow(Guid id)
        {
            return context.Shows
                .FirstOrDefault(p => p.Id == id);
        }

        public ICollection<Show> GetShows()
        {
            return context.Shows
                .Include(p => p.Movie)
                .Include(p => p.Screen)
                .ThenInclude(p=> p.Theater)
                .ToList();
        }
        private bool save()
        {
            return context.SaveChanges() > 0;
        }
    }
}

