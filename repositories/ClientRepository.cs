using Library.models;
using System.Data;
using System.Data.SqlClient;

namespace Library.repositories
{
    public class ClientRepository
    {
        readonly string _connectionString;

        public ClientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BindingSource getClientsData()
        {
            using SqlConnection connection = new(_connectionString);
            string queryClients = "SELECT ID, Name FROM Client";
            SqlCommand commandGetClients = new(queryClients, connection);
            SqlDataAdapter sqlDataAdapterClients = new(commandGetClients);
            DataTable dataTableClients = new();
            sqlDataAdapterClients.Fill(dataTableClients);
            BindingSource bindingSourceClients = new()
            {
                DataSource = dataTableClients
            };
            return bindingSourceClients;
        }

        public List<ClientResponse> getClientsId()
        {
            List<ClientResponse> ids = new();
            using SqlConnection connection = new(_connectionString);
            string queryClientsId = "SELECT ID FROM Client";
            SqlCommand commandClientsId = new(queryClientsId, connection);
            connection.Open();
            SqlDataReader readerClientId = commandClientsId.ExecuteReader();
            while (readerClientId.Read())
            {
                ClientResponse clientResponse = new()
                {
                    Id = readerClientId[0].ToString()
                };
                ids.Add(clientResponse);
            }

            return ids;
        }

        public void addClient(ClientRequest clientRequest)
        {
            using SqlConnection connection = new(_connectionString);
            string queryInsert = "INSERT INTO Client (Name) VALUES (@name)";
            SqlCommand commandInsert = new(queryInsert, connection);
            commandInsert.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = clientRequest.Name;
            connection.Open();
            commandInsert.ExecuteNonQuery();
        }

        public void modifyClient(ClientModel client)
        {
            using SqlConnection connection = new(_connectionString);
            string queryMod = "UPDATE Client SET Name=@name WHERE ID = @id";
            SqlCommand commandMod = new(queryMod, connection);
            commandMod.Parameters.Add("@id", SqlDbType.Int).Value = client.Id;
            commandMod.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = client.Name;
            connection.Open();
            commandMod.ExecuteNonQuery();
        }

        public void deleteClient(ClientResponse clientResponse)
        {
            using SqlConnection connection = new(_connectionString);
            string queryDelete = "DELETE FROM Client WHERE ID = @id";
            SqlCommand commandDelete = new(queryDelete, connection);
            commandDelete.Parameters.Add("@id", SqlDbType.Int).Value = clientResponse.Id;
            connection.Open();
            commandDelete.ExecuteNonQuery();
        }
    }
}
