using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Entity
{
    public class Movie_Production_Companies
    {
        public int movie_id { get; set; }
        public int id { get; set; }
        public string? logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }
}
