using System;

namespace KazanBooks.DAL.Models
{
    public class Author
    {
		public Guid id { get; set; }
		public string surName { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public DateTime birthDate { get; set; }
		public DateTime deathDate { get; set; }
	}
}
