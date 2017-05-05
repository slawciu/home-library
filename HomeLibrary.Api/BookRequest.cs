namespace HomeLibrary.Api
{
    public class BookRequest : IRequest
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Localisation { get; set; }

        public virtual bool IsValid()
        {
            return !string.IsNullOrEmpty(ISBN) &&
                   !string.IsNullOrEmpty(Title) &&
                   !string.IsNullOrEmpty(Author) &&
                   !string.IsNullOrEmpty(Localisation);
        }
    }
}