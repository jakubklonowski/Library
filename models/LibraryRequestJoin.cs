namespace Library.models
{
    public class LibraryRequestJoin
    {
        public LibraryRequest Library { get; set; }
        public BookModel Book { get; set; }
        public ClientModel Client { get; set; }
    }
}
