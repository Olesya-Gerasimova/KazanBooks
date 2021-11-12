using System;
using System.Collections.Generic;
using KazanBooks.DAL.Interfaces;
using KazanBooks.DAL.Models;
using System.Data.SqlClient;
using KazanBooks.DAL.DbContext;

namespace KazanBooks.DAL.Repositories
{
    public class BooksRepository : IRepository<Book>
    {

        private SqlConnection connection;

        private readonly static string INSERT_BOOK_SQL = "INSERT INTO " +
            "Book (authorId, publishDate, pagesNumber, country, title) " +
            "VALUES (@authorId, @publishDate, @pagesNumber, @country, @title)";

        private readonly static string DELETE_BOOK_BY_GUID = "DELETE FROM Book WHERE id=@guid";

        private readonly static string GET_BOOK_BY_GUID = "SELECT * FROM Book WHERE id=@guid";

        private readonly static string GET_BOOKS_BY_AUTHOR_GUID = "SELECT * FROM Book WHERE authorId=@guid";

        private readonly static string GET_ALL_BOOK = "SELECT * FROM Book";

        private readonly static string UPDATE_BOOK_SQL = "UPDATE Book SET " +
            "authorId = '@authorId', publishDate = '@publishDate', pagesNumber = '@pagesNumber', " +
            "country = '@country', title = '@title'  " +
            "WHERE id = @guid;";


        public BooksRepository()
        {
            connection = new MsSqlDbConnector().GetSqlConnection();
        }

        public void Create(Book book)
        {
            using (connection)
            {
                connection.Open();
                Console.WriteLine("Подключение открыто");
                string sqlExpression = INSERT_BOOK_SQL;
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // создаем параметры
                SqlParameter authorIdParam = new SqlParameter("@authorId", book.authorId);
                SqlParameter publishDateParam = new SqlParameter("@publishDate", book.publishDate);
                SqlParameter pagesNumberParam = new SqlParameter("@pagesNumber", book.pagesNumber);
                SqlParameter countryParam = new SqlParameter("@country", book.country);
                SqlParameter titleParam = new SqlParameter("@title", book.title);
                // добавляем параметр к команде
                command.Parameters.Add(authorIdParam);
                command.Parameters.Add(publishDateParam);
                command.Parameters.Add(pagesNumberParam);
                command.Parameters.Add(countryParam);
                command.Parameters.Add(titleParam);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Добавлено объектов: {0}", number);
            }
            Console.WriteLine("Подключение закрыто...");
        }

        public void Delete(Guid guid)
        {
            using (connection)
            {
                connection.Open();
                Console.WriteLine("Подключение открыто");
                string sqlExpression = DELETE_BOOK_BY_GUID;
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // создаем параметры
                SqlParameter guidParam = new SqlParameter("@guid", guid);

                // добавляем параметр к команде
                command.Parameters.Add(guidParam);

                int number = command.ExecuteNonQuery();
                Console.WriteLine("Удалено объектов: {0}", number);
            }
            Console.WriteLine("Подключение закрыто...");
        }

        public IEnumerable<Book> Find(Func<Book, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Book Get(Guid guid)
        {
            using (connection)
            {
                connection.Open();
                Console.WriteLine("Подключение открыто");
                string sqlExpression = GET_BOOK_BY_GUID;
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // создаем параметры
                SqlParameter guidParam = new SqlParameter("@guid", guid);
                SqlDataReader reader = command.ExecuteReader();
                Book foundBook = new Book();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        foundBook = BuildBookObjectFromRow(reader);
                    }
                }
                reader.Close();
                Console.WriteLine("Подключение закрыто...");
                return foundBook;
            }
        }

        public IEnumerable<Book> GetAll()
        {
            List<Book> books = new List<Book>();
            using (connection)
            {
                connection.Open();
                Console.WriteLine("Подключение открыто");
                string sqlExpression = GET_ALL_BOOK;
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // создаем параметры
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Book foundBook = BuildBookObjectFromRow(reader);
                        books.Add(foundBook);
                    }
                }
                reader.Close();
                Console.WriteLine("Подключение закрыто...");
                return books;
            }
        }

        public void Update(Book book)
        {
            using (connection)
            {
                connection.Open();
                Console.WriteLine("Подключение открыто");
                string sqlExpression = UPDATE_BOOK_SQL;
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // создаем параметры
                SqlParameter guidParam = new SqlParameter("@guid", book.id);
                SqlParameter authorIdParam = new SqlParameter("@authorId", book.authorId);
                SqlParameter publishDateParam = new SqlParameter("@publishDate", book.publishDate);
                SqlParameter pagesNumberParam = new SqlParameter("@pagesNumber", book.pagesNumber);
                SqlParameter countryParam = new SqlParameter("@country", book.country);
                SqlParameter titleParam = new SqlParameter("@title", book.title);

                // добавляем параметр к команде
                command.Parameters.Add(guidParam);
                command.Parameters.Add(authorIdParam);
                command.Parameters.Add(publishDateParam);
                command.Parameters.Add(pagesNumberParam);
                command.Parameters.Add(countryParam);
                command.Parameters.Add(titleParam);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Обновлено объектов: {0}", number);
            }
        }

        public IEnumerable<Book> GetAllBooksByAuthor(Author author)
        {
            List<Book> books = new List<Book>();
            using (connection)
            {
                connection.Open();
                Console.WriteLine("Подключение открыто");
                string sqlExpression = GET_BOOKS_BY_AUTHOR_GUID;
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // создаем параметры
                SqlParameter guidParam = new SqlParameter("@guid", author.id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Book foundBook = BuildBookObjectFromRow(reader);
                        books.Add(foundBook);
                    }
                }
                reader.Close();
                Console.WriteLine("Подключение закрыто...");
                return books;
            }
        }

        private Book BuildBookObjectFromRow(SqlDataReader reader)
        {
            Book book = new Book();
            book.id= reader.GetGuid(0);
            book.authorId = reader.GetGuid(1);
            book.publishDate= reader.GetDateTime(2);
            book.pagesNumber = reader.GetInt32(3);
            book.country = reader.GetString(4);
            book.title = reader.GetString(5);
            return book;
        }
    }
}
