using System;
using System.Collections.Generic;
using KazanBooks.DAL.Interfaces;
using KazanBooks.DAL.Models;
using System.Data.SqlClient;
using KazanBooks.DAL.DbContext;

namespace KazanBooks.DAL.Repositories
{
    public class AuthorsRepository : IRepository<Author>
    {

        private SqlConnection connection;

        private readonly static string INSERT_AUTHOR_SQL = "INSERT INTO " +
            "Author (surName, lastName, firstName, birthDate, deathDate) " +
            "VALUES (@surName, @lastName, @firstName, @birthDate, @deathDate)";

        private readonly static string DELETE_AUTHOR_BY_GUID = "DELETE FROM Author WHERE id=@guid";
        
        private readonly static string GET_AUTHOR_BY_GUID = "SELECT * FROM Author WHERE id=@guid";
        
        private readonly static string GET_ALL_AUTHORS = "SELECT * FROM Author";

        private readonly static string UPDATE_AUTHOR_SQL = "UPDATE Author SET " +
            "surName = '@surName', lastName = '@lastName', firstName = '@firstName', " +
            "birthDate = '@birthDate', deathDate = '@deathDate'  " +
            "WHERE id = @guid;";


        public AuthorsRepository()
        {
            connection = new MsSqlDbConnector().GetSqlConnection();
        }

        public void Create(Author author)
        {
            using (connection)
            {
                connection.Open();
                Console.WriteLine("Подключение открыто");
                string sqlExpression = INSERT_AUTHOR_SQL;
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // создаем параметры
                SqlParameter surNameParam = new SqlParameter("@surName", author.surName);
                SqlParameter lastNameParam = new SqlParameter("@lastName", author.lastName);
                SqlParameter firstNameParam = new SqlParameter("@firstName", author.firstName);
                SqlParameter birthDateParam = new SqlParameter("@birthDate", author.birthDate);
                SqlParameter deathDateParam = new SqlParameter("@deathDate", author.deathDate);

                // добавляем параметр к команде
                command.Parameters.Add(surNameParam);
                command.Parameters.Add(lastNameParam);
                command.Parameters.Add(firstNameParam);
                command.Parameters.Add(birthDateParam);
                command.Parameters.Add(deathDateParam);
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
                string sqlExpression = DELETE_AUTHOR_BY_GUID;
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

        public IEnumerable<Author> Find(Func<Author, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Author Get(Guid guid)
        {
            using (connection)
            {
                connection.Open();
                Console.WriteLine("Подключение открыто");
                string sqlExpression = GET_AUTHOR_BY_GUID;
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // создаем параметры
                SqlParameter guidParam = new SqlParameter("@guid", guid);
                SqlDataReader reader = command.ExecuteReader();
                Author foundAuthor = null;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        foundAuthor = BuildAuthorObjectFromRow(reader);
                    }
                }
                reader.Close();
                Console.WriteLine("Подключение закрыто...");
                return foundAuthor;
            }
        }

        public IEnumerable<Author> GetAll()
        {
            List<Author> authors = new List<Author>();
            using (connection)
            {
                connection.Open();
                Console.WriteLine("Подключение открыто");
                string sqlExpression = GET_ALL_AUTHORS;
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // создаем параметры
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Author foundAuthor = BuildAuthorObjectFromRow(reader);
                        authors.Add(foundAuthor);
                    }
                }
                reader.Close();
                Console.WriteLine("Подключение закрыто...");
                return authors;
            }
        }

        public void Update(Author author)
        {
            using (connection)
            {
                connection.Open();
                Console.WriteLine("Подключение открыто");
                string sqlExpression = UPDATE_AUTHOR_SQL;
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // создаем параметры
                SqlParameter guidParam = new SqlParameter("@guid", author.id);
                SqlParameter surNameParam = new SqlParameter("@surName", author.surName);
                SqlParameter lastNameParam = new SqlParameter("@lastName", author.lastName);
                SqlParameter firstNameParam = new SqlParameter("@firstName", author.firstName);
                SqlParameter birthDateParam = new SqlParameter("@birthDate", author.birthDate);
                SqlParameter deathDateParam = new SqlParameter("@deathDate", author.deathDate);

                // добавляем параметр к команде
                command.Parameters.Add(guidParam);
                command.Parameters.Add(surNameParam);
                command.Parameters.Add(lastNameParam);
                command.Parameters.Add(firstNameParam);
                command.Parameters.Add(birthDateParam);
                command.Parameters.Add(deathDateParam);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Обновлено объектов: {0}", number);
            }
        }

        private Author BuildAuthorObjectFromRow(SqlDataReader reader)
        {
            Author author = new Author();
            author.id = reader.GetGuid(0);
            author.surName = reader.GetString(1);
            author.lastName = reader.GetString(2);
            author.firstName = reader.GetString(3);
            author.birthDate = reader.GetDateTime(4);
            author.deathDate = reader.GetDateTime(5);
            return author;
        }
    }
}
