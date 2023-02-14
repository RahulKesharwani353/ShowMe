using System;
using Microsoft.EntityFrameworkCore;
using ShowMe.Data;
using ShowMe.Interface;
using ShowMe.Migrations;
using ShowMe.Models;

namespace ShowMe.Repositories
{
	public class TheaterRepository : ITheaterRepository
	{
        private readonly DataContext dataContext;

        public TheaterRepository(DataContext dataContext) 
		{
            this.dataContext = dataContext;
        }

        public bool CreateTheaters(Theater theater)
        {
            dataContext.Add(theater);
            return save();
        }

        public Theater GetTheater(Guid id)
        {
            var theater = dataContext.Theaters.FirstOrDefault(p => p.Id == id);
            return theater;
        }

        public ICollection<Theater> GetTheaters()
        {
            var theaters =
                dataContext
                .Theaters
                .Include(p => p.Screens)
                .OrderBy(p => p.Name)
                .ToList();
            return theaters;
        }

        private bool save()
        {
            return dataContext.SaveChanges() > 0;
        }
    }
}

