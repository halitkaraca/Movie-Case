using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Entity
{
    public class Movie_Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=MSI;database=MovieCase;user id=sa;password=123123;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;");

        }
       
        public DbSet<MovieList> MovieList { get; set; }
        public DbSet<Movie_Production_Companies> Movie_Production_Companies { get; set; }
        public DbSet<Movie_Production_Countries> Movie_Production_Countries { get; set; }
        public DbSet<Movie_Spoken_Languages> Movie_Spoken_Languages { get; set; }
        public DbSet<Movie_Genres> Movie_Genres { get; set; }
        public DbSet<Movie_Belongs_To_Collection> Movie_Belongs_To_Collection { get; set; }
        public DbSet<Movie_Note_Point> Movie_Note_Point { get; set; }
    
    }
}
