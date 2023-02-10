using System;
namespace ShowMe.Models
{
	public class Screen
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public bool Is3D { get; set; }
    }
}

