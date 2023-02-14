using System;
using ShowMe.Models;

namespace ShowMe.Interface
{
	public interface IScreenRepository
	{
		ICollection<Screen> GetScreens();
		Screen GetScreen(Guid id);
		bool CreateScreen(Guid theaterId, Screen screen);
	}
}

