using Library.models;
using Library.repositories;
using Library.services.validators;

namespace Library.services
{
    public class ClientService
    {
        readonly ClientRepository _repository;

        public ClientService(ClientRepository repository)
        {
            _repository = repository;
        }

        public BindingSource getClientsData()
        {
            BindingSource bsource = new();
            try
            {
                bsource = _repository.getClientsData();
                if (bsource == null || bsource.Count == 0)
                {
                    MessageBox.Show("No results!");
                    throw new ArgumentNullException(nameof(bsource));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bsource;
        }

        public List<ClientResponse> getClientsId()
        {
            List<ClientResponse> list = new();
            try
            {
                list = _repository.getClientsId();
                if (list == null || list.Count == 0)
                {
                    MessageBox.Show("No results!");
                    throw new ArgumentNullException(nameof(list));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return list;
        }

        public void addClient(ClientRequest clientRequest)
        {
            try
            {
                string name = clientRequest.Name;

                Validators.StringShortOrNull(5, name);

                _repository.addClient(clientRequest);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void modifyClient(ClientModel client)
        {
            try
            {
                string id = client.Id;
                string name = client.Name;

                Validators.Atoi(id);
                Validators.StringShortOrNull(5, name);
                Validators.StringShortOrNull(1, id);
                _ = int.TryParse(id, out int intid);
                Validators.IsNaturalNumber(intid);

                _repository.modifyClient(client);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void deleteClient(ClientResponse clientResponse)
        {
            try
            {
                string id = clientResponse.Id;

                Validators.Atoi(id);
                _ = int.TryParse(id, out int intid);
                Validators.IsNaturalNumber(intid);

                _repository.deleteClient(clientResponse);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
