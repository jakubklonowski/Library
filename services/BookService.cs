using Library.models;
using Library.repositories;
using Library.services.validators;

namespace Library.services
{
    public class BookService
    {
        readonly BookRepository _repository;

        public BookService(BookRepository repository)
        {
            _repository = repository;
        }

        public BindingSource getBooksData()
        {
            BindingSource bsource = new();
            try
            {
                bsource = _repository.getBooksData();

                if (bsource.Count == 0)
                {
                    MessageBox.Show("No results!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bsource;
        }

        public List<BookResponse> getBooksId()
        {
            List<BookResponse> list = new();
            try
            {
                list = _repository.getBooksId();
                if (list.Count == 0)
                {
                    MessageBox.Show("No results!");
                }
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return list;
        }

        public void addBook(BookRequest bookRequest)
        {
            try
            {
                string name = bookRequest.Name;
                string author = bookRequest.Author;

                Validators.StringShortOrNull(5, name, author);

                _repository.addBook(bookRequest);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void modifyBook(BookModel book)
        {
            try
            {
                string id = book.Id;
                string name = book.Name;
                string author = book.Author;

                Validators.StringShortOrNull(5, name, author);
                Validators.StringShortOrNull(1, id);
                Validators.Atoi(id);
                _ = int.TryParse(id, out int intid);
                Validators.IsNaturalNumber(intid);

                _repository.modifyBook(book);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void deleteBook(BookResponse bookResponse)
        {
            try
            {
                string id = bookResponse.Id;

                Validators.Atoi(id);
                _ = int.TryParse(id, out int intid);
                Validators.IsNaturalNumber(intid);

                _repository.deleteBook(bookResponse);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
