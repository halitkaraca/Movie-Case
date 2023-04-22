using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Movie.Business.Services;
using Movie.JWT.Security;

namespace Movie.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet("[action]")]
        public IActionResult Login()
        {
            return Created("", new Token().CreateToken());
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult MovieJsonPagination(int page)
        {
           string getMoviePaginationJson =  MovieService.MoviePagination(page);
          
            return Ok(getMoviePaginationJson);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult MovieNotePoint(int movie_id, int userId, string movieNote, int moviePoint)
        {
            int movieNotePoint = MovieService.MovieNotePointAdd(movie_id, userId, movieNote, moviePoint);
            string movieNotePointResult = "";
            if (movieNotePoint == 1) 
                { 
                    movieNotePointResult = "İşlem Başarılı"; 
                }
            else
                {
                    movieNotePointResult = "Lütfen 1 ile 10 arası puanlama yapınız"; 
                }

            return Ok(movieNotePointResult);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult MovieIdJson(int movieId)
        {
           string getMovieJsonList =  MovieService.MovieJson(movieId);
          
            return Ok(getMovieJsonList);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult MovieRecommendMailSend(int movieId, string movieName, string userMail)
        {
            string movieMail = MovieService.MovieMailSend(movieId, movieName, userMail);
            return Ok(movieMail);
        }

    }
}
