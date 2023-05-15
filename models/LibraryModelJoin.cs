namespace Library.models
{
    public class LibraryModelJoin
    {
        public LibraryModel Library { get; set; }
        public BookModel Book { get; set; }
        public ClientModel Client { get; set; }
    }
}
