using API_Movies.Interfaces;
using API_Movies.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Movies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMovieServices _movieService;

        public MoviesController(ILogger<MoviesController> logger, IMovieServices movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        //[HttpGet("GetMovies")]
        [HttpGet]
        public async Task<ActionResult<List<MovieModel>>> Get()
        {
            try
            {
                var data = await _movieService.ListAsync();
                if (data == null) return NotFound();
                return data.ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error: " + ex.Message);
            }
        }

        //[HttpGet("GetMovie/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieModel>> Get(int id)
        {
            try
            {
                if (id == 0) return BadRequest();

                var data = await _movieService.ByIdAsync(id);
                if (data == null) return NotFound();
                return data;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error: " + ex.Message);
            }
        }

        //[HttpPost("AddNewMovie")]
        [HttpPost]
        public async Task<ActionResult<MovieModel>> Add(MovieModel prm)
        {
            try
            {
                var data = await _movieService.AddAsync(prm);
                if (data == null) return NotFound();
                return CreatedAtAction(nameof(Get), new { id = data.Id }, data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error: " + ex.Message);
            }
        }

        //[HttpPatch("UpdateMovie/{id}")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]List<PatchModel> patch)
        {
            try
            {
                if (id == 0) return BadRequest();

                var movie = await _movieService.ByIdAsync(id);
                if (movie == null) return NotFound();
                
                var data = await _movieService.ApplyPatchAsync(movie, patch);
                if (data)
                    return Ok($"Movie with Id = {id} has been updated");
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error: On patch");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error: " + ex.Message);
            }
        }

        //[HttpDelete("DeleteMovie/{id}")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieModel>> Delete(int id)
        {
            try
            {
                var data = await _movieService.DeleteByIdAsync(id);
                if (data)
                    return Ok($"Movie with Id = {id} has been deleted");
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error: On deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error: " + ex.Message);
            }
        }
    }
}
