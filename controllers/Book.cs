using Library.models;
using Library.services;

namespace Library
{
    public partial class Book : Form
    {
        readonly BookService service;
        private readonly string connString = "Data Source=;Initial Catalog=;User Id=;Password=";

        public Book()
        {
            InitializeComponent();
            service = new(new(connString));
            fetchData();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string author = textBoxAuthor.Text;
            BookRequest bookRequest = new()
            {
                Name = name,
                Author = author
            };
            addBook(bookRequest);
            clearForms();
            fetchData();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            string id = comboBoxId.Text.ToString();
            string name = textBoxName.Text;
            string author = textBoxAuthor.Text;
            BookModel book = new()
            {
                Id = id,
                Name = name,
                Author = author
            };
            modifyBookData(book);
            clearForms();
            fetchData();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string id = comboBoxId.Text.ToString();
            BookResponse bookResponse = new()
            {
                Id = id
            };
            deleteBook(bookResponse);
            clearForms();
            fetchData();
        }

        private void fetchData()
        {
            // populates dataGridViewBooks from database
            dataGridViewBooks.DataSource = service.getBooksData();

            // clears & populates comboBoxId from database
            comboBoxId.Items.Clear();
            List<BookResponse> list = service.getBooksId();
            foreach (BookResponse item in list)
            {
                comboBoxId.Items.Add(item.Id.ToString());
            }
        }

        private void addBook(BookRequest bookRequest)
        {
            service.addBook(bookRequest);
        }

        private void modifyBookData(BookModel book)
        {
            service.modifyBook(book);
        }

        private void deleteBook(BookResponse bookResponse)
        {
            service.deleteBook(bookResponse);
        }

        private void clearForms()
        {
            comboBoxId.SelectedItem = null;
            textBoxName.Clear();
            textBoxAuthor.Clear();
        }
    }
}
