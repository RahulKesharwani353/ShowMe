using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShowMe.Dto;
using ShowMe.Interface;
using ShowMe.Models;
using ShowMe.Repositories;
using ShowMe.Migrations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShowMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenController : Controller
    {
        private readonly IScreenRepository screenRepository;
        private readonly ITheaterRepository theaterRepository;
        private readonly IMapper mapper;

        public ScreenController(IScreenRepository screenRepository,ITheaterRepository theaterRepository , IMapper mapper)
        {
            this.screenRepository = screenRepository;
            this.theaterRepository = theaterRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))]
        public IActionResult GetAllScreenLists()
        {
            var screens = screenRepository.GetScreens();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(screens);
        }

        [HttpGet("{ScreenId}")]
        [ProducesResponseType(200, Type = typeof(Movie))]
        public IActionResult GetScreen([FromRoute] Guid ScreenId)
        {
            var screens = screenRepository.GetScreen(ScreenId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(screens);
        }

        [HttpPost]
        public IActionResult CreateScreen([FromBody] ScreenDto screenDto, [FromQuery] Guid TheaterId) {

            var screen = mapper.Map<Screen>(screenDto);
            screen.Theater = theaterRepository.GetTheater(TheaterId);

            if (!screenRepository.CreateScreen(screen.Id, screen))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }
        
    }
}

