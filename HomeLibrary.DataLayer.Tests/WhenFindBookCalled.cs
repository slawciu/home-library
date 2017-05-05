using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using Xunit;

namespace HomeLibrary.DataLayer.Tests
{
    public class WhenFindBookCalled
    {
        private readonly Mock<IDbSet<Book>> _booksDbSetMock;
        private readonly LibraryRepository _libraryRepository;

        public WhenFindBookCalled()
        {
            var contextMock = new Mock<IContext>();
            _booksDbSetMock = new Mock<IDbSet<Book>>();

            contextMock.Setup(x => x.Books).Returns(_booksDbSetMock.Object);

            _libraryRepository = new LibraryRepository(contextMock.Object);
        }

        [Fact]
        public void ShouldReturnExistingBookFromRepositoryIfExists()
        {
            var author = new Author { AuthorId = 1, Name = "Orson Scott",  Surname = "Card"};
            var existingBook = new Book {Id = 0, Title = "Gra Endera", Author = author, ISBN = "9788376482514"};
            var booksInLibrary = new List<Book>
            {
                existingBook,
                new Book {Id = 1, Title = "Cieñ Endera", Author = author, ISBN = "9788378397649"}
            };

            _booksDbSetMock.As<IQueryable<Book>>().Setup(x => x.ElementType).Returns(booksInLibrary.AsQueryable().ElementType);
            _booksDbSetMock.As<IQueryable<Book>>().Setup(x => x.Expression).Returns(booksInLibrary.AsQueryable().Expression);
            _booksDbSetMock.As<IQueryable<Book>>().Setup(x => x.Provider).Returns(booksInLibrary.AsQueryable().Provider);
            _booksDbSetMock.As<IQueryable<Book>>().Setup(x => x.GetEnumerator()).Returns(booksInLibrary.GetEnumerator());


            var bookWithGivenIsbn = _libraryRepository.FindBookWithGivenIsbn("9788376482514");

            Assert.Equal(existingBook, bookWithGivenIsbn);
        }
    }
}