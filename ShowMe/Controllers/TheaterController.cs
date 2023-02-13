using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShowMe.Migrations;
using ShowMe.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShowMe.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TheaterController : Controller
    {
        private readonly ITheaterRepository theaterRepository;

        public TheaterController(ITheaterRepository theaterRepository)
        {
            this.theaterRepository = theaterRepository;
        }

        [HttpGet]
        public IActionResult GetTheaters()
        {
            var theaters = theaterRepository.GetTheaters();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(theaters);
        }

        [HttpPost]
        public IActionResult CreateTheater(Theater theater)
        {
            if (!theaterRepository.CreateTheaters(theater))
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

