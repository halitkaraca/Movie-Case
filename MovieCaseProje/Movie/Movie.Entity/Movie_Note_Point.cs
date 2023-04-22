using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Entity
{
    public class Movie_Note_Point
    {
        [Key]
        public int movie_id { get; set; }
        public int userId { get; set; }
        public int moviePoint { get; set; }
        public string movieNote { get; set; }
    }
}
