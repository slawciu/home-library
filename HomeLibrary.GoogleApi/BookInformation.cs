namespace HomeLib.BooksInformationService
{
    public class BookInformation
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Author { get; set; }
        public string Cover { get; internal set; }
        public string Description { get; internal set; }
        public string PublishedDate { get; internal set; }
        public string Provider { get; set; }
    }
}