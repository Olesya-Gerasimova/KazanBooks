using KazanBooks.BAL.DTO;
using System;
using System.Collections.Generic;

namespace KazanBooks.BAL.Interfaces
{
    public interface IKazanBooksService
    {
        AuthorDTO GetAuthor(Guid id);
        BookDTO GetBook(Guid id);
        
        IEnumerable<AuthorDTO> GetAuthors();
        IEnumerable<BookDTO> GetBooks();
        IEnumerable<BookDTO> GetAllBooksByAuthor(AuthorDTO author);

    }
}
