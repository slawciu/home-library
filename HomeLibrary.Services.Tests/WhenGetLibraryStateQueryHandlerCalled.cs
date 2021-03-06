﻿using System.Collections.Generic;
using HomeLibrary.DataLayer;
using Moq;
using Xunit;

namespace HomeLibrary.Services.Tests
{
    public class WhenGetLibraryStateQueryHandlerCalled
    {
        [Fact]
        public void ShouldReturnLibraryState()
        {
            var getLibraryState = new GetLibraryState(new Mock<ILibraryRepository>().Object);

            var libraryState = getLibraryState.Handle(new GetLibraryStateQuery());

            Assert.NotNull(libraryState);
        }

        [Fact]
        public void ShouldRetrieveBookListFromRepository()
        {
            var libraryRepository = new Mock<ILibraryRepository>();

            libraryRepository.Setup(x => x.GetAllBooks()).Returns(new List<Book>
            {
                new Book
                {
                    ISBN = "1234567890123"
                }
            });


            var getLibraryState = new GetLibraryState(libraryRepository.Object);

            var libraryState = getLibraryState.Handle(new GetLibraryStateQuery());

            Assert.Collection(libraryState.Books, x => Assert.True(x.ISBN == "1234567890123"));
        }
    }
}
