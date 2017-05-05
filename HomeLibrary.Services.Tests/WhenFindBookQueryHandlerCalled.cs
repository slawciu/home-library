using System;
using System.Collections.Generic;
using System.Linq;
using HomeLib.BooksInformationService;
using HomeLibrary.DataLayer;
using Moq;
using Xunit;

namespace HomeLibrary.Services.Tests
{
    public class WhenFindBookQueryHandlerCalled
    {
        private readonly FindBook _findBook;
        private readonly Mock<ILibraryRepository> _libraryRepositoryMock;
        private readonly Mock<IBooksInformationService> _googleApiServiceMock;
        private readonly Mock<IBooksInformationService> _ksiazkiOrgApiService;

        public WhenFindBookQueryHandlerCalled()
        {
            _googleApiServiceMock = new Mock<IBooksInformationService>();
            _ksiazkiOrgApiService = new Mock<IBooksInformationService>();
            _libraryRepositoryMock = new Mock<ILibraryRepository>();
            var bookServices = new List<IBooksInformationService> { _googleApiServiceMock.Object, _ksiazkiOrgApiService.Object };
            _findBook = new FindBook(_libraryRepositoryMock.Object, bookServices);
        }

        [Fact]
        public void ShouldReturnListOfBookInformationForGivenIsbn()
        {
            var bookInfo = _findBook.Handle(new FindBookQuery {ISBN = "9788375106626" });

            Assert.IsType<List<Book>>(bookInfo);
        }

        [Fact]
        public void ShouldCheckIfBookWithGivenIsbnExistsInRepository()
        {
            _findBook.Handle(new FindBookQuery {ISBN = "9788375106626" });

            _libraryRepositoryMock.Verify(x => x.FindBookWithGivenIsbn("9788375106626"));
        }

        [Fact]
        public void ShouldReturnBookInfoFromRepositoryIfExists()
        {
            var existingBook = new Book
            {
                Id = 1,
                ISBN = "9788375106626",
                Title = "Diuna",
                //Author = "Frank Herbert"
            };
            _libraryRepositoryMock.Setup(x => x.FindBookWithGivenIsbn("9788375106626")).Returns(() => existingBook);

            var bookInfos = _findBook.Handle(new FindBookQuery { ISBN = "9788375106626" });

            Assert.Contains(bookInfos, bookInfo => existingBook == bookInfo);
        }

        [Fact]
        public void ShouldReturnEmptyBookInformationWhenNotFoundInRepository()
        {
            _libraryRepositoryMock.Setup(x => x.FindBookWithGivenIsbn("9788375106626")).Returns(() => null);

            var bookInfos = _findBook.Handle(new FindBookQuery { ISBN = "9788375106626" });

            Assert.Collection(bookInfos, AssertIsEmpty);
        }

        [Fact]
        public void ShouldCallGoogleApiInOrderToFindBookWithGivenIsbn()
        {
            _libraryRepositoryMock.Setup(x => x.FindBookWithGivenIsbn("9788375106626")).Returns(() => null);

            _findBook.Handle(new FindBookQuery { ISBN = "9788375106626" });

            _googleApiServiceMock.Verify(x => x.GetByIsbn("9788375106626"));
        }

        [Fact]
        public void ShouldReturnBookFoundInGoogleApi()
        {
            _libraryRepositoryMock.Setup(x => x.FindBookWithGivenIsbn("9788375106626")).Returns(() => null);
            var bookInformationFromApi = new BookInformation
            {
                ISBN = "9788375106626",
                Title = "Diuna",
                Author = "Frank Herbert"
            };
            _googleApiServiceMock.Setup(x => x.GetByIsbn("9788375106626")).Returns(bookInformationFromApi);

            var handlerResults = _findBook.Handle(new FindBookQuery { ISBN = "9788375106626" });

            Assert.Contains(handlerResults, book => bookInformationFromApi.Author == book.Author.Fullname);
        }

        [Fact]
        public void ShouldCallKsiazkiOrgApiInOrderToFindBookWithGivenIsbn()
        {
            _libraryRepositoryMock.Setup(x => x.FindBookWithGivenIsbn("9788375106626")).Returns(() => null);

            _findBook.Handle(new FindBookQuery { ISBN = "9788375106626" });

            _ksiazkiOrgApiService.Verify(x => x.GetByIsbn("9788375106626"));
        }

        private static void AssertIsEmpty(Book book)
        {
            throw new NotImplementedException();
            //Assert.Equal("", book.Author);
            //Assert.Equal("", book.Localisation);
            //Assert.Equal("", book.Title);
            //Assert.Equal(0, book.Id);
            //Assert.NotEqual("", book.ISBN);
        }
    }
}