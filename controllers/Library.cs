using Library.models;
using Library.repositories;
using Library.services;

namespace Library
{
    public partial class Library : Form
    {
        readonly LibraryService _service;
        readonly BookService _serviceB;
        readonly ClientService _serviceC;
        private readonly string connString = "Data Source=;Initial Catalog=;User Id=;Password=";

        public Library()
        {
            InitializeComponent();
            _service = new(new LibraryRepository(connString));
            _serviceB = new(new BookRepository(connString));
            _serviceC = new(new ClientRepository(connString));
            fetchData();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string bookId = comboBoxBook.SelectedItem.ToString();
            string clientId = comboBoxClient.SelectedItem.ToString();
            LibraryRequestJoin library = new()
            {
                Library = new()
                {
                    Date = DateTime.Now.ToString(),
                    Active = true
                },
                Book = new()
                {
                    Id = bookId,
                    Name = "",
                    Author = ""
                },
                Client = new()
                {
                    Id = clientId,
                    Name = ""
                }
            };
            addBorrow(library);
            clearForms();
            fetchData();
        }

        private void buttonMod_Click(object sender, EventArgs e)
        {
            string id = comboBoxId.SelectedItem.ToString();
            string idbook = comboBoxBook.SelectedItem.ToString();
            string idclient = comboBoxClient.SelectedItem.ToString();
            string date = dateTimePicker0.Value.ToString();
            LibraryModelJoin library = new()
            {
                Library = new()
                {
                    Id = id,
                    Date = date,
                    Active = true // TODO
                },
                Book = new()
                {
                    Id = idbook,
                    Name = "",
                    Author = ""
                },
                Client = new()
                {
                    Id = idclient,
                    Name = ""
                }
            };
            modBorrow(library);
            clearForms();
            fetchData();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            string id = (string)comboBoxId.SelectedItem;
            LibraryResponse libraryResponse = new()
            {
                Id = id,
            };
            delBorrow(libraryResponse);
            clearForms();
            fetchData();
        }

        // button change status
        private void button4_Click(object sender, EventArgs e)
        {
            string id = (string)comboBoxId.SelectedItem;
            LibraryResponse libraryResponse = new() { Id = id };
            LibraryRequestJoin libraryRequest = _service.getSingleBorrow(libraryResponse);
            LibraryModelJoin libraryModel = new()
            {
                Library = new()
                {
                    Id = id,
                    Active = !libraryRequest.Library.Active,
                    Date = libraryRequest.Library.Date
                },
                Book = new()
                {
                    Id = libraryRequest.Book.Id,
                    Name = libraryRequest.Book.Name,
                    Author = libraryRequest.Book.Author
                },
                Client = new()
                {
                    Id = libraryRequest.Client.Id,
                    Name = libraryRequest.Client.Name
                }
            };
            modBorrow(libraryModel);

            clearForms();
            fetchData();
        }

        private void fetchData()
        {
            var ds = _service.getLibraryData();
            dataGridViewLibraries.DataSource = ds;
            comboBoxId.DataSource = ds;
            comboBoxId.ValueMember = "id";
            comboBoxId.DisplayMember = "id";

            var books = _serviceB.getBooksData();
            comboBoxBook.DataSource = books;
            comboBoxBook.ValueMember = "id";
            comboBoxBook.DisplayMember = "name";

            var clients = _serviceC.getClientsData();
            comboBoxClient.DataSource = clients;
            comboBoxClient.ValueMember = "id";
            comboBoxClient.DisplayMember = "name";
        }

        private void addBorrow(LibraryRequestJoin libraryRequestJoin)
        {
            _service.addBorrow(libraryRequestJoin);
        }

        private void modBorrow(LibraryModelJoin libraryModelJoin)
        {
            _service.modifyBorrow(libraryModelJoin);
        }

        private void delBorrow(LibraryResponse libraryResponse)
        {
            _service.deleteBorrow(libraryResponse);
        }

        private void clearForms()
        {
            comboBoxId.Items.Clear();
            comboBoxBook.Items.Clear();
            comboBoxClient.Items.Clear();
        }
    }
}
