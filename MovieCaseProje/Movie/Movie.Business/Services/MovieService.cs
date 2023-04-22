using Microsoft.EntityFrameworkCore;
using Movie.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using System.Runtime.InteropServices.JavaScript;

namespace Movie.Business.Services
{
    public class MovieService
    { 
        static Movie_Context db = new Movie_Context();


        public static string MoviePagination(int page)
        {
            int pageSize = 2;
            if(page<=1){ page = 0; }

            int toplamKayit = (page * pageSize) - 2;
            if (toplamKayit == -2) { toplamKayit = 0; }

            var movieList = db.MovieList.Skip(toplamKayit).Take(pageSize).ToList();
         
            List<string> getMoviePaginationJson = new List<string>();
            Movie_Json mj = new Movie_Json();
            foreach (var item in movieList)
            {
                mj.id = item.id;
                mj.adult = item.adult;
                mj.backdrop_path = item.backdrop_path;
                mj.belongs_to_collection = db.Movie_Belongs_To_Collection.AsNoTracking().Where(x => x.movie_id == item.id).ToList();
                mj.budget = item.budget;
                mj.genres = db.Movie_Genres.AsNoTracking().Where(x => x.movie_id == item.id).ToList();
                mj.homepage = item.homepage;
                mj.imdb_id = item.imdb_id;
                mj.original_language = item.original_language;
                mj.original_title = item.original_title;
                mj.overview = item.overview;
                mj.popularity = item.popularity;
                mj.poster_path = item.poster_path;
                mj.production_companies = db.Movie_Production_Companies.AsNoTracking().Where(x => x.movie_id == item.id).ToList();
                mj.production_countries = db.Movie_Production_Countries.AsNoTracking().Where(x => x.movie_id == item.id).ToList();
                mj.release_date = item.release_date;
                mj.revenue = item.revenue;
                mj.runtime = item.runtime;
                mj.spoken_languages = db.Movie_Spoken_Languages.AsNoTracking().Where(x => x.movie_id == item.id).ToList();
                mj.status = item.status;
                mj.tagline = item.tagline;
                mj.title = item.title;
                mj.video = item.video;
                mj.vote_average = item.vote_average;
                mj.vote_count = item.vote_count;
                mj.note_point = db.Movie_Note_Point.AsNoTracking().Where(x => x.movie_id == item.id).ToList();
                mj.note_average = Convert.ToDecimal(db.Movie_Note_Point.AsNoTracking().Where(x => x.movie_id == item.id).Average(y => y.moviePoint));
                getMoviePaginationJson.Add(JsonConvert.SerializeObject(mj));
            }
          
            var gmpjList = getMoviePaginationJson.ToList();
            string getMoviePaginationJsonLast = "{\"movies\": [" + gmpjList[0] + "," + gmpjList[1] + "]}";
            return getMoviePaginationJsonLast;
        }


        public static string MovieJson(int movieId)
        {            
            var movieList = db.MovieList.Where(x => x.id == movieId).FirstOrDefault();
            Movie_Json mj = new Movie_Json();
            mj.id = movieId;
            mj.adult = movieList.adult;
            mj.backdrop_path = movieList.backdrop_path;
            mj.belongs_to_collection = db.Movie_Belongs_To_Collection.AsNoTracking().Where(x => x.movie_id == movieId).ToList();
            mj.budget = movieList.budget;
            mj.genres = db.Movie_Genres.AsNoTracking().Where(x => x.movie_id == movieId).ToList();
            mj.homepage = movieList.homepage;
            mj.imdb_id = movieList.imdb_id;
            mj.original_language = movieList.original_language;
            mj.original_title = movieList.original_title;
            mj.overview = movieList.overview;
            mj.popularity = movieList.popularity;
            mj.poster_path = movieList.poster_path;
            mj.production_companies = db.Movie_Production_Companies.AsNoTracking().Where(x => x.movie_id == movieId).ToList();
            mj.production_countries = db.Movie_Production_Countries.AsNoTracking().Where(x => x.movie_id == movieId).ToList();
            mj.release_date = movieList.release_date;
            mj.revenue = movieList.revenue;
            mj.runtime = movieList.runtime;
            mj.spoken_languages = db.Movie_Spoken_Languages.AsNoTracking().Where(x => x.movie_id == movieId).ToList();
            mj.status = movieList.status;
            mj.tagline = movieList.tagline;
            mj.title = movieList.title;
            mj.video = movieList.video;
            mj.vote_average = movieList.vote_average;
            mj.vote_count = movieList.vote_count;
            mj.note_point = db.Movie_Note_Point.AsNoTracking().Where(x => x.movie_id == movieId).ToList();
            mj.note_average = Convert.ToDecimal(db.Movie_Note_Point.AsNoTracking().Where(x => x.movie_id == movieId).Average(y => y.moviePoint));

            string getMovieJson = JsonConvert.SerializeObject(mj);
            return getMovieJson;            
        }

        public static int IdControl()
        {
            var lastMovie = db.MovieList.OrderByDescending(x=>x.id).FirstOrDefault();
            int lastMovieId = lastMovie.id;

            return lastMovieId;
        }

        public static int MovieNotePointAdd(int movie_id, int userId, string movieNote, int moviePoint)
        {
            if ((moviePoint >= 1) && (moviePoint <= 10))
            {
                Movie_Note_Point mnp = new Movie_Note_Point();
                mnp.movie_id = movie_id;
                mnp.userId = userId;
                mnp.movieNote = movieNote;
                mnp.moviePoint = moviePoint;
                db.Movie_Note_Point.Add(mnp);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }        
          
        }
         

        public static string MovieMailSend(int movieId, string movieName, string userMail)
        {  
            string MailTablo = "";

            MailTablo = "<table border='0' width='80%'>" +
                        "<tr>" +
                        "<td><Movie Id: /td><td>"+ movieId + "</td>" +
                        "<td>Movie Name:</td><td>"+ movieName + "</td>" +
                        "</tr>" +
                        "</table>";             

            MailMessage movieMail = new MailMessage();
            movieMail.To.Add(userMail);
            movieMail.CC.Add("movie@moviecase.com"); //mail adresi örnektir
            movieMail.From = new MailAddress("movie@gmail.com", "Movie Case"); //mail adresi örnektir
            movieMail.Subject = "Film Tavsiyesi";
            movieMail.Body = MailTablo;
            movieMail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("movie@gmail.com", "fbsnfvtlfcahbrfz"); //mail adresi örnektir         
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(movieMail);
                return "Mail Gönderildi.";
            }
            catch(Exception ex)
            {
                return "Mail Gönderilmedi ! " + ex.Message;
            }                    
        }
    }
}
