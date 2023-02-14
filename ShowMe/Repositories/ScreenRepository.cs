using System;
using Microsoft.EntityFrameworkCore;
using ShowMe.Data;
using ShowMe.Interface;
using ShowMe.Models;

namespace ShowMe.Repositories
{
	public class ScreenRepository : IScreenRepository
	{
        private readonly DataContext context;

        public ScreenRepository(DataContext context)
		{
            this.context = context;
        }

        public bool CreateScreen(Guid theaterId, Screen screen)
        {
            context.Add(screen);
            return save();
        }

        public Screen GetScreen(Guid id)
        {
            return context.Screens
                .Include(p => p.Theater)
            .FirstOrDefault(p=> p.Id == id);
        }

        public ICollection<Screen> GetScreens()
        {
            return context.Screens
                .Include(p => p.Theater)
            .ToList();
            return context.Screens.ToList();
        }

        private bool save()
        {
            return context.SaveChanges() > 0;
        }
    }
}

