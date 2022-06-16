using AutoMapper;
using CodeChallenge.DataAccess.Repositories;
using CodeChallenge.Services;
using CodeChallenge.Services.Implementations;
using Moq;
using Xunit;

namespace CodeChallenge.UnitTest
{
    public class CodeChallengeTest : DbContextUnitTest
    {
        [Fact]
        public void SumaTest()
        {
            //Arrange
            int a = 6;
            int b = 7;
            //Act
            var suma = a + b;
            var expected = 13;
            //Assert
            Assert.Equal(expected, suma);
        }

        [Fact]
        public void PaginationTest()
        {
            //Arrange
            int total = 100;
            int rows = 10;
            //Act
            var resultado = Utils.GetTotalPages(total, rows);
            var expected = 10;
            //Assert
            Assert.Equal(expected, resultado);
        }

        [Theory]
        [InlineData(30, 10, 3)]
        [InlineData(110, 10, 11)]
        [InlineData(200, 4, 50)]
        public void PaginationWithParameters(int total, int rows, int expected)
        {
            //Arrange
            //Act
            var resultado = Utils.GetTotalPages(total, rows);
            //Assert
            Assert.Equal(expected, resultado);
        }

        [Fact]
        public async Task FindSeedDataTest()
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var repository = new ProductRepository(Context, mapper.Object);
            var service = new ProductService(repository, mapper.Object);
            //Act
            var actual = await service.FilterAsync("", 1, 5);
            //Assert
            Assert.True(actual.TotalPages > 0);
        }

        [Theory]
        [InlineData("", 10, 10)] //ENCONTRAR TODOS
        [InlineData("abc", 5, 0)] //ENCONTRAR NINGUNO
        [InlineData("Juguete", 4, 25)] 
        public async Task PaginationForProductsTest(string filter, int rows, int expected)
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var repository = new ProductRepository(Context, mapper.Object);
            var service = new ProductService(repository, mapper.Object);
            //Act
            var actual = await service.FilterAsync(filter, 1, rows);
            //Assert
            Assert.Equal(expected, actual.TotalPages);
        }
    }
}