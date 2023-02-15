using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShowMe.Dto;
using ShowMe.Interface;
using ShowMe.Models;

namespace ShowMe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScreenController : Controller {
	private readonly IScreenRepository _screenRepository;
	private readonly ITheaterRepository _theaterRepository;
	private readonly IMapper _mapper;

	public ScreenController(IScreenRepository screenRepository, ITheaterRepository theaterRepository, IMapper mapper) {
		_screenRepository = screenRepository;
		_theaterRepository = theaterRepository;
		_mapper = mapper;
	}

	[HttpGet]
	[ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
	public IActionResult GetAllScreenLists() {
		var screens = _screenRepository.GetScreens();

		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		return Ok(screens);
	}

	[HttpGet("{ScreenId}")]
	[ProducesResponseType(200, Type = typeof(Movie))]
	public IActionResult GetScreen([FromRoute] Guid ScreenId) {
		var screens = _screenRepository.GetScreen(ScreenId);

		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		return Ok(screens);
	}

	[HttpPost]
	public IActionResult CreateScreen([FromBody] ScreenDto screenDto, [FromQuery] Guid TheaterId) {

		var screen = _mapper.Map<Screen>(screenDto);
		screen.Theater = _theaterRepository.GetTheater(TheaterId);

		if (!_screenRepository.CreateScreen(screen.Id, screen)) {
			ModelState.AddModelError("", "Something went wrong while savin");
			return StatusCode(500, ModelState);
		}
		return Ok("Successfully created");
	}

}