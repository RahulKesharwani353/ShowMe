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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShowMe.Controllers
{
    [Route("/api/[controller]")]
    public class ShowController : Controller
    {
        private readonly IShowRepository showRepository;
        private readonly IMovieRepository movieRepository;
        private readonly IScreenRepository screenRepository;
        private readonly IMapper mapper;

        public ShowController(IShowRepository  showRepository,
            IMovieRepository movieRepository,
            IScreenRepository screenRepository,
            IMapper mapper)
        {
            this.showRepository = showRepository;
            this.movieRepository = movieRepository;
            this.screenRepository = screenRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllShows()
        {
            var shows = showRepository.GetShows();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(shows);
        }

        [HttpPost]
        public IActionResult CreateShow([FromBody] ShowDto showDto  , [FromQuery]Guid MovieId, [FromQuery] Guid ScreenId)
        {
            var show = mapper.Map<Show>(showDto);
            //show.Movie = movieRepository.GetMovie(MovieId);
            //show.Screen = screenRepository.GetScreen(ScreenId);
            if (!showRepository.CreateShow(show))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            var resp = new
            {
                message = "Created successfully"
            };

            return Ok(resp);
        }
    }
}

