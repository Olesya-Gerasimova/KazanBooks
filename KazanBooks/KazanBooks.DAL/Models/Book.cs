using System;

namespace KazanBooks.DAL.Models
{
    public class Book
    {
		public Guid id { get; set; }
		public Guid authorId { get; set; }
		public string title { get; set; }
		public DateTime publishDate { get; set; }
		public int pagesNumber { get; set; }
		public string country { get; set; }
	}
}
