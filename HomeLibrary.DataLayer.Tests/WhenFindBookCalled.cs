using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using Xunit;

namespace HomeLibrary.DataLayer.Tests
{
    public class WhenFindBookCalled
    {
        private readonly Mock<IContext> _contextMock;
        private readonly Mock<IDbSet<Book>> _booksDbSetMock;
        private LibraryRepository _libraryRepository;

        public WhenFindBookCalled()
        {
            _contextMock = new Mock<IContext>();
            _booksDbSetMock = new Mock<IDbSet<Book>>();

            _contextMock.Setup(x => x.Books).Returns(_booksDbSetMock.Object);

            _libraryRepository = new LibraryRepository(_contextMock.Object);
        }

        [Fact]
        public void ShouldReturnExistingBookFromRepositoryIfExists()
        {
            var existingBook = new Book {Id = 0, Title = "Gra Endera", Author = "Orson Scott Card", Localisation = "Gliwice", ISBN = "9788376482514"};
            var booksInLibrary = new List<Book>
            {
                existingBook,
                new Book {Id = 1, Title = "Cieñ Endera", Author = "Orson Scott Card", Localisation = "Gliwice", ISBN = "9788378397649"}
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