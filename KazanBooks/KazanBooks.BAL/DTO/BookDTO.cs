using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KazanBooks.BAL.DTO
{
    public class BookDTO
    {
		public Guid id { get; set; }
		public Guid authorId { get; set; }
		public string title { get; set; }
		public DateTime publishDate { get; set; }
		public int pagesNumber { get; set; }
		public string country { get; set; }
	}
}
