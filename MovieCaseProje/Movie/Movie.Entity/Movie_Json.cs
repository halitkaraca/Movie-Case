using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Entity
{
    public class Movie_Json
    {   
        public int id { get; set; }
        public string adult { get; set; }
        public string backdrop_path { get; set; }
        public List<Movie_Belongs_To_Collection> belongs_to_collection { get; set; }
        public Nullable<decimal> budget { get; set; }
        public List<Movie_Genres> genres { get; set; }      
        public string homepage { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public Nullable<decimal> popularity { get; set; }
        public string poster_path { get; set; }
        public List<Movie_Production_Companies> production_companies { get; set; }
        public List<Movie_Production_Countries> production_countries { get; set; }
        public Nullable<System.DateTime> release_date { get; set; }
        public Nullable<long> revenue { get; set; }
        public Nullable<int> runtime { get; set; }
        public List<Movie_Spoken_Languages> spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public string video { get; set; }
        public Nullable<decimal> vote_average { get; set; }
        public Nullable<int> vote_count { get; set; }
        public List<Movie_Note_Point> note_point { get; set; }
        public Nullable<decimal> note_average { get; set; }
    }

}
