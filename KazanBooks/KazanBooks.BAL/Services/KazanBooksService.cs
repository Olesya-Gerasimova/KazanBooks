using AutoMapper;
using KazanBooks.BAL.DTO;
using KazanBooks.BAL.Interfaces;
using KazanBooks.DAL.Models;
using KazanBooks.DAL.Repositories;

using System;
using System.Collections.Generic;


namespace KazanBooks.BAL.Services
{
    public class KazanBooksService : IKazanBooksService
    {

        private AuthorsRepository authorsRepository;
        private BooksRepository booksRepository;

        public KazanBooksService()
        {
            authorsRepository = new AuthorsRepository();
            booksRepository = new BooksRepository();
        }

        public IEnumerable<BookDTO> GetAllBooksByAuthor(AuthorDTO author)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AuthorDTO, Author>()).CreateMapper();
            Author authorObj = mapper.Map<AuthorDTO, Author>(author);

            var bookMapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
            return bookMapper.Map<IEnumerable<Book>, List<BookDTO>>(booksRepository.GetAllBooksByAuthor(authorObj));
        }

        public AuthorDTO GetAuthor(Guid id)
        {
            var author = authorsRepository.Get(id);
            if (author == null)
                throw new Exception("Автор не найден");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Author, AuthorDTO>()).CreateMapper();
            return mapper.Map<Author, AuthorDTO>(author);
        }

        public IEnumerable<AuthorDTO> GetAuthors()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Author, AuthorDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Author>, List<AuthorDTO>>(authorsRepository.GetAll());
        }

        public BookDTO GetBook(Guid id)
        {
            var book = booksRepository.Get(id);
            if (book == null)
                throw new Exception("Книга не найден");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
            return mapper.Map<Book, BookDTO>(book);
        }

        public IEnumerable<BookDTO> GetBooks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Book>, List<BookDTO>>(booksRepository.GetAll());
        }
    }
}
