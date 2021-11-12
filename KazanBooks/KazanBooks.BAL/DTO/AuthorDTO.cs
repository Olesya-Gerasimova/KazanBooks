using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KazanBooks.BAL.DTO
{
    public class AuthorDTO
    {
		public Guid id { get; set; }
		public string surName { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public DateTime birthDate { get; set; }
		public DateTime deathDate { get; set; }
	}
}
