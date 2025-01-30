namespace MovieRental.Models
{
    public class RentalHeader
    {
        public RentalHeader()
        {
            RentalDetails = new List<RentalDetail>();
        }

            public Customers? Customer { get; set; }
         
            public int RentalId { get; set; }
            public DateTime RentalDate { get; set; }
            public DateTime ReturnDate { get; set; }

          
            public ICollection<RentalDetail>? RentalDetails { get; set; }

            public string RentalDateFormatted => RentalDate.ToString("M/d/yyyy");
            public string ReturnDateFormatted => ReturnDate.ToString("M/d/yyyy");
        }
    }

