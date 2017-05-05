using HomeLibrary.Api;
using Xunit;

namespace HomeLibrary.Tests
{
    public class WhenIsValidCalledOnBookRequest
    {
        [Fact]
        public void ShouldReturnTrueWhenAllFieldsAreNotEmpty()
        {
            var bookRequest = new BookRequest
            {
                Author = "Author",
                ISBN = "0123456789012",
                Localisation = "Gliwice",
                Title = "Test Book"
            };

            var isValid = bookRequest.IsValid();

            Assert.True(isValid);
        }

        [Theory]
        [InlineData(null, null, null, null)]
        [InlineData("", "", "", "")]
        [InlineData("Author", "", "", "")]
        [InlineData("", "01234567890", "", "")]
        [InlineData("", "", "Gliwice", "")]
        [InlineData("", "", "", "Test Book")]
        public void ShouldReturnFalseWhenOneOfFieldsIsEmpty(string author, string isbn, string localisation, string title)
        {
            var bookRequest = new BookRequest
            {
                Author = author,
                ISBN = isbn,
                Localisation = localisation,
                Title = title
            };

            var isValid = bookRequest.IsValid();

            Assert.False(isValid);
        }
    }
}