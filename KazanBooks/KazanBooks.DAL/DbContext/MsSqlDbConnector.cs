using System;
using System.Configuration;
using System.Data.SqlClient;
using KazanBooks.DAL.Interfaces;

namespace KazanBooks.DAL.DbContext
{
    class MsSqlDbConnector : IDbConnector<MsSqlDbConnector>
    {
        private string ConnectionString;

        public MsSqlDbConnector()
        {
            //ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=KazanBook;User Id=sa;Password=Jktcz2002.)";
        }

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
