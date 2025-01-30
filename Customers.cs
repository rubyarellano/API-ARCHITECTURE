using System.ComponentModel.DataAnnotations;

namespace MovieRental.Models
{
    public class Customers
    {

        public int CustomerId { get; set; }
        public string? CustomerName { get; set; } 

        public string? Address { get; set; }
        
        public int Number { get; set; }

      
    }
}
