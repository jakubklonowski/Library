using Library.models;
using System.Data;
using System.Data.SqlClient;

namespace Library.repositories
{
    public class LibraryRepository
    {
        readonly string _connectionString;

        public LibraryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BindingSource getBorrowedBooks()
        {
            using SqlConnection connection = new(_connectionString);
            string queryBorrowed = "SELECT library.id, book.name, client.name, active, date " +
                                   "FROM ((library " +
                                   "INNER JOIN book ON library.id_book=book.id)" +
                                   "INNER JOIN client ON library.id_client=client.id)";
            SqlCommand commandBorrowed = new(queryBorrowed, connection);
            SqlDataAdapter sqlDataAdapterBorrowed = new(commandBorrowed);
            DataTable dataTableBorrowed = new();
            sqlDataAdapterBorrowed.Fill(dataTableBorrowed);
            BindingSource bindingSourceBorrowed = new()
            {
                DataSource = dataTableBorrowed
            };
            return bindingSourceBorrowed;
        }

        public LibraryRequestJoin getSingleBorrow(LibraryResponse libraryResponse)
        {
            using SqlConnection connection = new(_connectionString);
            string querySingleRequest = "SELECT id_book, id_client, date, active FROM library WHERE id=@id";
            SqlCommand commandSingleRequest = new(querySingleRequest, connection);
            commandSingleRequest.Parameters.Add("@id", SqlDbType.Int).Value = libraryResponse.Id;
            connection.Open();
            SqlDataReader readerSingleRequest = commandSingleRequest.ExecuteReader();
            readerSingleRequest.Read();
            LibraryRequestJoin lib = new()
            {
                Library =
                {
                    Date = readerSingleRequest["date"].ToString(),
                    Active = (bool)readerSingleRequest["active"]
                },
                Book =
                {
                    Id = readerSingleRequest["id_book"].ToString(),
                    Name = "",
                    Author = ""
                },
                Client =
                {
                    Id = readerSingleRequest["id_client"].ToString(),
                    Name = ""
                }
            };
            return lib;
        }

        public void addBorrow(LibraryRequestJoin libraryRequestJoin)
        {
            using SqlConnection connection = new(_connectionString);
            string queryAdd = "INSERT INTO library (id_book, id_client, date, active) VALUES (@idbook, @idclient, @date, @active)";
            SqlCommand commandAdd = new(queryAdd, connection);
            commandAdd.Parameters.Add("@idbook", SqlDbType.VarChar).Value = libraryRequestJoin.Book.Id;
            commandAdd.Parameters.Add("@idclient", SqlDbType.VarChar).Value = libraryRequestJoin.Client.Id;
            commandAdd.Parameters.Add("@date", SqlDbType.DateTime).Value = libraryRequestJoin.Library.Date;
            commandAdd.Parameters.Add("@active", SqlDbType.Bit).Value = libraryRequestJoin.Library.Active;
            connection.Open();
            commandAdd.ExecuteNonQuery();
        }

        public void modifyBorrow(LibraryModelJoin libraryModelJoin)
        {
            // TODO: todo
        }

        public void deleteBorrow(LibraryResponse libraryResponse)
        {
            using SqlConnection connection = new(_connectionString);
            string queryDelete = "DELETE FROM library WHERE id=@id";
            SqlCommand commandDelete = new(queryDelete, connection);
            commandDelete.Parameters.Add("@id", SqlDbType.Int).Value = libraryResponse.Id;
            connection.Open();
            commandDelete.ExecuteNonQuery();
        }
    }
}
