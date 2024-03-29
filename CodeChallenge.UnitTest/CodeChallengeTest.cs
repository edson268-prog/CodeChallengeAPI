using AutoMapper;
using CodeChallenge.API.Profiles;
using CodeChallenge.DataAccess.Repositories;
using CodeChallenge.Dto.Request;
using CodeChallenge.Dto.Response;
using CodeChallenge.Entities;
using CodeChallenge.Services;
using CodeChallenge.Services.Implementations;
using Moq;
using Xunit;

namespace CodeChallenge.UnitTest
{
    public class CodeChallengeTest : DbContextUnitTest
    {
        //private readonly ProductService _sut;
        private readonly Mock<IProductRepository> _productRepoMock = new Mock<IProductRepository>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        //public CodeChallengeTest()
        //{
        //    _sut = new ProductService(_productRepoMock.Object, _mapper.Object);
        //}

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
            //HACK: Testing Feature list
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
            ////Arrange
            //var mapper = new Mock<IMapper>();
            //var repository = new ProductRepository(Context, mapper.Object);
            //var service = new ProductService(repository, mapper.Object);
            ////Act
            //var actual = await service.FilterAsync("", 1, 5);
            ////Assert
            //Assert.True(actual.TotalPages > 0);

            //TODO: Try to mock the DBContext and use the service.
            //Technically as it is right now its considered integration test.
            //MOck<Context>.Seup(It.IsANy).REturns(new List<>()

            ProductService _sut = null;
            try
            {
                var myProfile = new AutoMapperProfiles();
                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
                Mock<IMapper> mapper = new Mock<IMapper>(configuration);
                _sut = new ProductService(_productRepoMock.Object, mapper.Object);
            }
            catch (Exception ex)
            {
                throw;
            }


            int productId = 1;
            var product = new Product
            {
                Id = productId,
                Name = "Capitan America",
                Description = "Juguete articulable de plastico",
                Company = "Disney",
                AgeRestriction = 8,
                Price = 150,
                ProductTypeId = 2,
                Active = true,
                SoldOut = false
            };

            //_productRepoMock.Setup(x => x.CreateAsync(productDto)).ReturnsAsync(productId);
            //_mapper.Setup(m => m.Map<Product, DtoResponseProduct>(It.IsAny<Product>())).Returns(new DtoResponseProduct());
            _productRepoMock.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);
            //Act
            var productResult = await _sut.GetByIdAsync(productId);

            Assert.Equal(productId, productResult.ResponseResult.Id);
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