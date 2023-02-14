using System;
using AutoMapper;
using ShowMe.Dto;
using ShowMe.Models;

namespace ShowMe.Helper
{
	public class MapProfile : Profile
    {
		public MapProfile()
		{
            CreateMap<ScreenDto, Screen>();
        }
	}
}

