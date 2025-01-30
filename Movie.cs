using System.ComponentModel.DataAnnotations;

namespace MovieRental.Models
{
    public class Movie
    {
        public Movie()
        {
            RentalDetails = new List<RentalDetail>();
        }

       
        public int MovieId { get; set; }

        
        public string? MovieName { get; set; }

      
        public string?  Type { get; set; }

        public ICollection<RentalDetail> RentalDetails { get; set; }

    }
}
