namespace HomeLib.BooksInformationService
{
    public interface IBooksInformationService
    {
        BookInformation GetByIsbn(string isbn);
    }
}