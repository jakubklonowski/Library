using Library.models;
using Library.repositories;
using Library.services.validators;

namespace Library.services
{
    public class LibraryService
    {
        readonly LibraryRepository _repository;

        public LibraryService(LibraryRepository repository)
        {
            _repository = repository;
        }

        public BindingSource getLibraryData()
        {
            BindingSource bsource = new();
            try
            {
                bsource = _repository.getBorrowedBooks();
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

        public LibraryRequestJoin getSingleBorrow(LibraryResponse libraryResponse)
        {
            LibraryRequestJoin lib = _repository.getSingleBorrow(libraryResponse);
            return lib;
        }

        public void addBorrow(LibraryRequestJoin libraryRequestJoin)
        {
            try
            {
                string date = libraryRequestJoin.Library.Date;
                bool active = libraryRequestJoin.Library.Active;
                string idbook = libraryRequestJoin.Book.Id;
                string idclient = libraryRequestJoin.Client.Id;

                Validators.StringShortOrNull(1, active.ToString(), idbook, idclient);
                Validators.StringShortOrNull(5, date);
                Validators.Atoi(idbook, idclient);

                _repository.addBorrow(libraryRequestJoin);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void modifyBorrow(LibraryModelJoin libraryModelJoin)
        {
            try
            {
                string id = libraryModelJoin.Library.Id;
                string date = libraryModelJoin.Library.Date;
                bool active = libraryModelJoin.Library.Active;
                string idbook = libraryModelJoin.Book.Id;
                string idclient = libraryModelJoin.Client.Id;

                Validators.StringShortOrNull(1, id, active.ToString(), idbook, idclient);
                Validators.StringShortOrNull(5, date);
                Validators.Atoi(id, idbook, idclient);
                _ = int.TryParse(id, out int intid);
                Validators.IsNaturalNumber(intid);

                _repository.modifyBorrow(libraryModelJoin);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void deleteBorrow(LibraryResponse libraryResponse)
        {
            string id = libraryResponse.Id;

            Validators.StringShortOrNull(1, id);
            Validators.Atoi(id);
            _ = int.TryParse(id, out int intid);
            Validators.IsNaturalNumber(intid);

            _repository.deleteBorrow(libraryResponse);
        }
    }
}
