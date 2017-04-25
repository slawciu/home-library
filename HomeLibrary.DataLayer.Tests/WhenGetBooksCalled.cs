using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace HomeLibrary.DataLayer.Tests
{
    public class WhenGetBooksCalled
    {
        private readonly Mock<IContext> _contextMock;
        private readonly Mock<IDbSet<Book>> _booksDbSetMock;
        private readonly LibraryRepository _libraryRepository;

        public WhenGetBooksCalled()
        {
            _contextMock = new Mock<IContext>();
            _booksDbSetMock = new Mock<IDbSet<Book>>();

            _contextMock.Setup(x => x.Books).Returns(_booksDbSetMock.Object);

            _libraryRepository = new LibraryRepository(_contextMock.Object);
        }

        [Fact]
        public void ShouldReturnBooksFromDbSet()
        {
            var expectedBooks = new List<Book>
            {
                new Book {Id = 0, Title = "Gra Endera", Author = "Orson Scott Card", Localisation = "Gliwice", ISBN = "9788376482514"},
                new Book {Id = 1, Title = "Cień Endera", Author = "Orson Scott Card", Localisation = "Gliwice", ISBN = "9788378397649"}
            };

            _booksDbSetMock.As<IQueryable<Book>>().Setup(x => x.ElementType).Returns(expectedBooks.AsQueryable().ElementType);
            _booksDbSetMock.As<IQueryable<Book>>().Setup(x => x.Expression).Returns(expectedBooks.AsQueryable().Expression);
            _booksDbSetMock.As<IQueryable<Book>>().Setup(x => x.Provider).Returns(expectedBooks.AsQueryable().Provider);
            _booksDbSetMock.As<IQueryable<Book>>().Setup(x => x.GetEnumerator()).Returns(expectedBooks.GetEnumerator());

            _contextMock.Setup(x => x.Books).Returns(_booksDbSetMock.Object);

            var booksFromRepository = _libraryRepository.GetAllBooks();

            Assert.Equal(expectedBooks, booksFromRepository);
        }
    }
}
