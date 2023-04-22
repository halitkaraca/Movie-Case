using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Entity
{
    public class Movie_Spoken_Languages
    {
        [Key]
        public int movie_id { get; set; }
        public string english_name { get; set; }
        public string iso_639_1 { get; set; }
        public string name { get; set; }
    }
}
