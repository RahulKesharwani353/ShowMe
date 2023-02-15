using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShowMe.Dto;
using ShowMe.Interface;
using ShowMe.Models;

namespace ShowMe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShowController : Controller {
	private readonly IShowRepository _showRepository;
	private readonly IMovieRepository _movieRepository;
	private readonly IScreenRepository _screenRepository;
	private readonly IMapper _mapper;

	public ShowController(
		IShowRepository showRepository,
		IMovieRepository movieRepository,
		IScreenRepository screenRepository,
		IMapper mapper
	) {
		_showRepository = showRepository;
		_movieRepository = movieRepository;
		_screenRepository = screenRepository;
		_mapper = mapper;
	}

	[HttpGet]
	public IActionResult GetAllShows() {
		var shows = _showRepository.GetShows();
		if (!ModelState.IsValid) {
			return BadRequest(ModelState);
		}
		return Ok(shows);
	}

	[HttpPost]
	public IActionResult CreateShow([FromBody] ShowDto showDto, [FromQuery] Guid MovieId, [FromQuery] Guid ScreenId) {
		var show = _mapper.Map<Show>(showDto);
		show.Movie = _movieRepository.GetMovie(MovieId);
		show.Screen = _screenRepository.GetScreen(ScreenId);
		if (!_showRepository.CreateShow(show)) {
			ModelState.AddModelError("", "Something went wrong while savin");
			return StatusCode(500, ModelState);
		}

		var resp = new {
			message = "Created successfully"
		};

		return Ok(resp);
	}
}