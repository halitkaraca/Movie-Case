using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Movie.Business.Services;
using Movie.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Movie.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MovieIdControl()
        {
         int getLastMovieId =  MovieService.IdControl();
            return Ok(getLastMovieId);
        }      

        

    }
}
