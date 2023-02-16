using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShowMe.Interface;
using ShowMe.Models;
using ShowMe.Dto;

namespace ShowMe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TheaterController : Controller {
	private readonly ITheaterRepository _theaterRepository;
	private readonly IMapper _mapper;

	public TheaterController(ITheaterRepository theaterRepository, IMapper mapper) {
		_theaterRepository = theaterRepository;
		_mapper = mapper;
	}

	[HttpGet]
	public IActionResult GetTheaters() {
		var theaters = _theaterRepository.GetTheaters();
		if (!ModelState.IsValid) {
			return BadRequest(ModelState);
		}
		return Ok(theaters);
	}

	[HttpGet("search")]
	public IActionResult GetTheatersByCity([FromQuery]string city) {

        if (city == null || city == "")
        {
            return BadRequest(new
            {
                message = "Please enter city"
            });
        }
        else
        {
            var theaters = _theaterRepository.GetTheatersByCity(city);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (theaters.Count == 0)
                return NotFound(
                    new
                    {
                        message = "No item Found"
                    });
            return Ok(theaters);
        }
		
	}

	[HttpGet("{theaterId}/movies")]
	public IActionResult GetTheaterMovies(Guid theaterId) {
		var movies = _theaterRepository.GetTheaterMovies(theaterId);

		if (!ModelState.IsValid) {
			return BadRequest(ModelState);
		}

		if (movies.Count == 0)
            return NotFound(
                    new
                    {
                        message = "No item Found"
                    });

        return Ok(movies);
	}

	[HttpPost]
	public IActionResult CreateTheater(TheaterDto theater) {
		var theaterMap = _mapper.Map<Theater>(theater);
		if (!_theaterRepository.CreateTheaters(theaterMap)) {
			ModelState.AddModelError("", "Something went wrong while savin");
			return StatusCode(500, ModelState);
		}

		var resp = new {
			message = "Created successfully"
		};

		return Ok(resp);
	}
}
