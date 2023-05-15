using Library.models;
using System.Data;
using System.Data.SqlClient;

namespace Library.repositories
{
    public class BookRepository
    {
        readonly string _connectionString;

        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BindingSource getBooksData()
        {
            using SqlConnection connection = new(_connectionString);
            string queryBooks = "SELECT ID, Name, Author FROM Book";
            SqlCommand commandGetBooks = new(queryBooks, connection);
            SqlDataAdapter sqlDataAdapterBooks = new(commandGetBooks);
            DataTable dataTableBooks = new();
            sqlDataAdapterBooks.Fill(dataTableBooks);
            BindingSource bindingSourceBooks = new()
            {
                DataSource = dataTableBooks
            };
            return bindingSourceBooks;
        }

        public List<BookResponse> getBooksId()
        {
            List<BookResponse> ids = new();
            using SqlConnection connection = new(_connectionString);
            string queryBooksId = "SELECT ID FROM Book";
            SqlCommand commandBooksId = new(queryBooksId, connection);
            connection.Open();
            SqlDataReader readerBookId = commandBooksId.ExecuteReader();
            while (readerBookId.Read())
            {
                BookResponse bookResponse = new()
                {
                    Id = readerBookId[0].ToString()
                };
                ids.Add(bookResponse);
            }

            return ids;
        }

        public void addBook(BookRequest bookRequest)
        {
            using SqlConnection connection = new(_connectionString);
            string queryInsert = "INSERT INTO Book (Name, Author) VALUES (@name, @author)";
            SqlCommand commandInsert = new(queryInsert, connection);
            commandInsert.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = bookRequest.Name;
            commandInsert.Parameters.Add("@author", SqlDbType.VarChar, 50).Value = bookRequest.Author;
            connection.Open();
            commandInsert.ExecuteNonQuery();
        }

        public void modifyBook(BookModel book)
        {
            using SqlConnection connection = new(_connectionString);
            string queryMod = "UPDATE Book SET Name=@name, Author=@author WHERE ID = @id";
            SqlCommand commandMod = new(queryMod, connection);
            commandMod.Parameters.Add("@id", SqlDbType.Int).Value = book.Id;
            commandMod.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = book.Name;
            commandMod.Parameters.Add("@author", SqlDbType.VarChar, 50).Value = book.Author;
            connection.Open();
            commandMod.ExecuteNonQuery();
        }

        public void deleteBook(BookResponse bookResponse)
        {
            using SqlConnection connection = new(_connectionString);
            string queryDelete = "DELETE FROM Book WHERE ID = @id";
            SqlCommand commandDelete = new(queryDelete, connection);
            commandDelete.Parameters.Add("@id", SqlDbType.Int).Value = bookResponse.Id;
            connection.Open();
            commandDelete.ExecuteNonQuery();
        }
    }
}
