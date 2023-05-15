using Library.models;
using Library.repositories;
using Library.services;

namespace Library
{
    public partial class Client : Form
    {
        readonly ClientService service;
        private readonly string connString = "Data Source=;Initial Catalog=;User Id=;Password=";

        public Client()
        {
            InitializeComponent();
            service = new ClientService(new ClientRepository(connString));
            fetchData();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string clientName = textBoxClientName.Text;
            ClientRequest clientRequest = new()
            {
                Name = clientName
            };
            addClient(clientRequest);
            clearForms();
            fetchData();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            string clientId = "";

            try
            {
                if (comboBoxClientId.SelectedItem == null)
                {
                    throw new ArgumentException();
                }
                else
                {
                    clientId = comboBoxClientId.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string clientName = textBoxClientName.Text;
            ClientModel client = new()
            {
                Id = clientId,
                Name = clientName
            };
            modifyClientData(client);
            clearForms();
            fetchData();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string clientId = "";

            try
            {
                if (comboBoxClientId.SelectedItem == null)
                {
                    throw new ArgumentException();
                }
                else
                {
                    clientId = comboBoxClientId.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ClientResponse clientResponse = new()
            {
                Id = clientId
            };
            deleteClient(clientResponse);
            clearForms();
            fetchData();
        }

        private void fetchData()
        {
            // populates dataGridViewClients from database
            dataGridViewClients.DataSource = service.getClientsData();

            // clears & populates comboBoxClientId from database
            comboBoxClientId.Items.Clear();
            List<ClientResponse> list = service.getClientsId();
            foreach (ClientResponse item in list)
            {
                comboBoxClientId.Items.Add(item.Id.ToString());
            }
        }

        private void addClient(ClientRequest clientRequest)
        {
            service.addClient(clientRequest);
        }

        private void modifyClientData(ClientModel client)
        {
            service.modifyClient(client);
        }

        private void deleteClient(ClientResponse clientResponse)
        {
            service.deleteClient(clientResponse);
        }

        private void clearForms()
        {
            comboBoxClientId.SelectedItem = null;
            textBoxClientName.Clear();
        }
    }
}
