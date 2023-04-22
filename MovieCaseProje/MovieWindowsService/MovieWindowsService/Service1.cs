using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MovieWindowsService
{
    public partial class Service1 : ServiceBase
    {
        private System.Timers.Timer timer;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (timer == null)
            {
                timer = new System.Timers.Timer();
            }
            timer.Interval = 3600000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            Kayit();
            timer.Start();
        }
        protected override void OnStop()
        {
        }
        public void Kayit()
        {
            WebClient clientLastId = new WebClient();
            int movieLastId = Convert.ToInt32(clientLastId.DownloadString("https://localhost:44388/Movie/MovieIdControl"));
            int sonrakiId = movieLastId + 1;
            string pushMovieId = sonrakiId.ToString();

            string apiUrl = "https://api.themoviedb.org/3/movie/" + pushMovieId + "?api_key=db877f8ab298e9dc8318a56f170d02d9";
            WebClient client = new WebClient();
            string jsonMovie = client.DownloadString(apiUrl);


            int btcControl = jsonMovie.IndexOf("\"belongs_to_collection\":null,");

            MovieListJE jsonList;
            List<Movie_Nodes> belongs_to_collection;
            if (btcControl != -1)
            {
                jsonList = JsonConvert.DeserializeObject<MovieListJE>(jsonMovie);
                belongs_to_collection = null;
            }
            else
            {
                // Api Json Düzeltme - belongs_to_collection null olmadığı durumda - Köşeli parantez ekleme
                string btcSB = jsonMovie.Replace("\"belongs_to_collection\":", "\"belongs_to_collection\":[");
                string btcSBEnd = btcSB.Replace("},\"budget\"", "}],\"budget\"");
                jsonList = JsonConvert.DeserializeObject<MovieListJE>(btcSBEnd);
                belongs_to_collection = jsonList.belongs_to_collection.ToList();
            }


            MovieCaseEntities db = new MovieCaseEntities();
            int id = Convert.ToInt32(jsonList.id);
            string adult = jsonList.adult;
            string backdrop_path = jsonList.backdrop_path;

            decimal budget = Convert.ToDecimal(jsonList.budget);
            var genres = jsonList.genres.ToList();
            string homepage = jsonList.homepage;
            string imdb_id = jsonList.imdb_id;
            string original_language = jsonList.original_language;
            string original_title = jsonList.original_title;
            string overview = jsonList.overview;
            decimal popularity = Convert.ToDecimal(jsonList.popularity);
            string poster_path = jsonList.poster_path;
            var production_companies = jsonList.production_companies.ToList();
            var production_countries = jsonList.production_countries.ToList();
            DateTime release_date = Convert.ToDateTime(jsonList.release_date);
            long revenue = Convert.ToInt64(jsonList.revenue);
            int runtime = Convert.ToInt32(jsonList.runtime);
            var spoken_languages = jsonList.spoken_languages.ToList();
            string status = jsonList.status;
            string tagline = jsonList.tagline;
            string title = jsonList.title;
            string video = jsonList.video;
            decimal vote_average = Convert.ToDecimal(jsonList.vote_average);
            int vote_count = Convert.ToInt32(jsonList.vote_count);

            MovieList mv = new MovieList();
            mv.id = id;
            mv.adult = adult;
            mv.backdrop_path = backdrop_path;
            mv.budget = budget;
            mv.homepage = homepage;
            mv.imdb_id = imdb_id;
            mv.original_language = original_language;
            mv.original_title = original_title;
            mv.overview = overview;
            mv.popularity = popularity;
            mv.poster_path = poster_path;
            mv.release_date = release_date;
            mv.revenue = revenue;
            mv.runtime = runtime;
            mv.status = status;
            mv.tagline = tagline;
            mv.title = title;
            mv.video = video;
            mv.vote_average = vote_average;
            mv.vote_count = vote_count;
            db.MovieList.Add(mv);
            db.SaveChanges();

            Movie_Genres mg = new Movie_Genres();
            foreach (var item in genres)
            {
                mg.movie_id = id;
                mg.id = item.id;
                mg.name = item.name;
                db.Movie_Genres.Add(mg);
                db.SaveChanges();
            }

            Movie_Production_Companies mpcp = new Movie_Production_Companies();
            foreach (var item in production_companies)
            {
                mpcp.movie_id = id;
                mpcp.id = item.id;
                mpcp.logo_path = item.logo_path;
                mpcp.name = item.name;
                mpcp.origin_country = item.origin_country;
                db.Movie_Production_Companies.Add(mpcp);
                db.SaveChanges();
            }

            Movie_Production_Countries mpct = new Movie_Production_Countries();
            foreach (var item in production_countries)
            {
                mpct.movie_id = id;
                mpct.iso_3166_1 = item.iso_3166_1;
                mpct.name = item.name;
                db.Movie_Production_Countries.Add(mpct);
                db.SaveChanges();
            }

            Movie_Spoken_Languages msl = new Movie_Spoken_Languages();
            foreach (var item in spoken_languages)
            {
                msl.movie_id = id;
                msl.english_name = item.english_name;
                msl.iso_639_1 = item.iso_639_1;
                msl.name = item.name;
                db.Movie_Spoken_Languages.Add(msl);
                db.SaveChanges();
            }

            if (belongs_to_collection != null)
            {
                Movie_Belongs_To_Collection mbtc = new Movie_Belongs_To_Collection();
                foreach (var item in belongs_to_collection)
                {
                    mbtc.movie_id = id;
                    mbtc.id = item.id;
                    mbtc.name = item.name;
                    mbtc.poster_path = item.poster_path;
                    mbtc.backdrop_path = item.backdrop_path;
                    db.Movie_Belongs_To_Collection.Add(mbtc);
                    db.SaveChanges();
                }
            }


        }
    }
}
