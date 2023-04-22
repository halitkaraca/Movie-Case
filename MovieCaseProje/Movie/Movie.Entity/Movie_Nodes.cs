using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Entity
{
    public class Movie_Nodes
    {
        public int id { get; set; }
        public string name { get; set; }
        public string logo_path { get; set; }
        public string origin_country { get; set; }
        public string iso_3166_1 { get; set; }
        public string english_name { get; set; }
        public string iso_639_1 { get; set; }
        public string poster_path { get; set; }
        public string backdrop_path { get; set; }
    }
}
