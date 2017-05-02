using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace HomeLib.BooksInformationService
{
    public class KsiazkiOrgInformationService : IBooksInformationService
    {
        public KsiazkiOrgInformationService(string apiKey, string applicationName)
        {

        }
        public BookInformation GetByIsbn(string isbn)
        {
            var html = new HtmlDocument();

            using (
                var reader =
                    new StreamReader(
                        WebRequest.Create($"https://ksiazki.org/wyszukiwarka/szukaj?fraza={isbn}")
                            .GetResponse()
                            .GetResponseStream(), Encoding.UTF8))
            {
                html.Load(reader);
                var root = html.DocumentNode;
                var titles = root.Descendants().Where(n => n.GetAttributeValue("id", "").Contains("lblTytul"));
                var authors = root.Descendants().Where(n => n.GetAttributeValue("itemprop", "").Contains("author"));
                var publishDates = root.Descendants().Where(n => n.GetAttributeValue("itemprop", "").Contains("datePublished"));
                var publishers = root.Descendants().Where(n => n.GetAttributeValue("id", "").Contains("hlnkWydawnictwo"));
                var description = root.Descendants().Where(n => n.GetAttributeValue("itemprop", "").Contains("description"));
                var cover = root.Descendants().Where(n => n.GetAttributeValue("id", "").Contains("imgOkladka"));
                try
                {
                    return new BookInformation
                    {
                        Title = titles.ToList().Last()?.InnerText,
                        Author = authors.First()?.InnerText,
                        PublishedDate = publishDates.First()?.InnerText,
                        Publisher = publishers.First()?.InnerText,
                        Description = description.First()?.InnerText,
                        Cover = cover.ToList().First()?.Attributes.First(x => x.Name == "src")?.Value,
                        Provider = "ksiazki.org",
                        ISBN = isbn
                    };
                }
                catch (Exception e)
                {
                    return null;
                }
            }

        }
    }
}