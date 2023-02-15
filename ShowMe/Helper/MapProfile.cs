using AutoMapper;
using ShowMe.Dto;
using ShowMe.Models;

namespace ShowMe.Helper;

public class MapProfile : Profile {
	public MapProfile() {
		CreateMap<ScreenDto, Screen>();
		CreateMap<ShowDto, Show>();
		CreateMap<MovieDto, Movie>().ReverseMap();
		CreateMap<TheaterDto, Theater>().ReverseMap();
	}
}