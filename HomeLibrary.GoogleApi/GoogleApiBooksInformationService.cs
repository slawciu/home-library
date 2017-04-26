using System.Linq;
using Google.Apis.Books.v1;
using Google.Apis.Services;

namespace HomeLib.BooksInformationService
{
    public class GoogleApiBooksInformationService : IBooksInformationService
    {
        private readonly BooksService _googleBookService;

        public GoogleApiBooksInformationService(string apiKey, string applicationName)
        {
            _googleBookService = new BooksService(new BaseClientService.Initializer
            {
                ApiKey = apiKey,
                ApplicationName = applicationName
            });
        }

        public BookInformation GetByIsbn(string isbn)
        {
            var volumes = _googleBookService.Volumes.List($"isbn={isbn}").Execute();

            var book = volumes.Items?.FirstOrDefault();

            if (book == null)
            {
                return null;
            }

            return new BookInformation
            {
                Title = book.VolumeInfo.Title,
                Publisher = book.VolumeInfo.Publisher,
                PublishedDate = book.VolumeInfo.PublishedDate,
                Author = book.VolumeInfo.Authors.Aggregate("", (current, author) => current + $"{author} "),
                ISBN = isbn,
                Cover = book.VolumeInfo.ImageLinks?.Thumbnail,
                Description = book.VolumeInfo.Description,
                Provider = "Google"
            };
        }
    }
}
