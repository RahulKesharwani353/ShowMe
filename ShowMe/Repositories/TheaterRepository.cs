using System;
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

        public ICollection<Theater> GetTheaters()
        {
            ICollection<Theater> theaters = dataContext.Theaters.OrderBy(p => p.Name).ToList();
            return theaters;
        }

        private bool save()
        {
            return dataContext.SaveChanges() > 0;
        }
    }
}

