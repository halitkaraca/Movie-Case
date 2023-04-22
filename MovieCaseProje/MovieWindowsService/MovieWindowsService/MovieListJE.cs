using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWindowsService
{
    public class MovieListJE
    {
        public int id { get; set; }
        public string adult { get; set; }
        public string backdrop_path { get; set; }
        public List<Movie_Nodes> belongs_to_collection { get; set; }
        public Nullable<decimal> budget { get; set; }
        public List<Movie_Nodes> genres { get; set; }
        public string homepage { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public Nullable<decimal> popularity { get; set; }
        public string poster_path { get; set; }
        public List<Movie_Nodes> production_companies { get; set; }
        public List<Movie_Nodes> production_countries { get; set; }
        public Nullable<System.DateTime> release_date { get; set; }
        public Nullable<long> revenue { get; set; }
        public Nullable<int> runtime { get; set; }
        public List<Movie_Nodes> spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public string video { get; set; }
        public Nullable<decimal> vote_average { get; set; }
        public Nullable<int> vote_count { get; set; }
    }
}
