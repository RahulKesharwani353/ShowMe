using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShowMe.Interface;
using ShowMe.Models;

namespace ShowMe.Controllers;

[Route("api/movies")]
[ApiController]
public class MovieController : Controller {
	private readonly IMovieRepository _movieRepository;
	private readonly IMapper _mapper;

	public MovieController(IMovieRepository movieRepository, IMapper mapper) {
		_movieRepository = movieRepository;
		_mapper = mapper;
	}

	[HttpGet]
	[ProducesResponseType(200, Type = typeof(IEnumerable<MovieDto>))]
	public IActionResult GetMoviesList() {
		var movies = _mapper.Map<List<MovieDto>>(_movieRepository.GetMovies());

		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		return Ok(movies);
	}

	[HttpGet("search")]
	[ProducesResponseType(200)]
	public IActionResult GetMoviesByName([FromQuery]string search) {


		if (search == null || search == "")
		{
			return BadRequest(new
			{
				message = "Please enter search query"
			});
		}
		else
		{
			var movies = _movieRepository.GetMoviesByName(search);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

			if (movies == null)
			{
				return NotFound(
					new
					{
                        message = "No item Found"
                    });
			}

            return Ok(movies);
        }
	}

	[HttpGet("{MovieId}")]
	[ProducesResponseType(200, Type = typeof(IEnumerable<MovieDto>))]
	public IActionResult GetMovie(Guid MovieId) {
		var movies = _mapper.Map<MovieDto>(_movieRepository.GetMovie(MovieId));

		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		if (movies == null)
			return NotFound();

		return Ok(movies);
	}

	[HttpGet("{MovieId}/theaters")]
	[ProducesResponseType(200)]
	public IActionResult GetMovieTheaters(Guid MovieId) {
		var movies = _movieRepository.GetMovieTheaters(MovieId);

		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		if (movies == null)
			return NotFound();

		return Ok(movies);
	}

	[HttpGet("{MovieId}/shows")]
	[ProducesResponseType(200)]
	public IActionResult GetMovieShows(Guid MovieId, [FromQuery] Guid TheaterId) {
		var movies = _movieRepository.GetMovieShows(MovieId, TheaterId);

		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		if (movies == null)
			return NotFound();

		return Ok(movies);
	}

	[HttpPost]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public IActionResult CreateMovie([FromBody] MovieDto movieCreate) {
		var movieObj = _mapper.Map<Movie>(movieCreate);
		if (!_movieRepository.CreateMovie(movieObj)) {
			ModelState.AddModelError("", "Something went wrong while saving");
			return StatusCode(500, ModelState);
		}

		return Ok("Successfully created");
	}
}