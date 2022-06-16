using System;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.DataAccess;
using CodeChallenge.Entities;

namespace CodeChallenge.UnitTest
{
    public class DbContextUnitTest : IDisposable
    {
        protected readonly CodeChallengeDbContext Context;

        protected DbContextUnitTest()
        {
            var options = new DbContextOptionsBuilder<CodeChallengeDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            Context = new CodeChallengeDbContext(options);

            Context.Database.EnsureCreated();

            Seed();
        }

        private void Seed()
        {
            var random = new Random();
            var list = new List<Product>();

            for (var i = 0; i < 100; i++)
            {
                var valor = random.Next(20, 400);
                var valor2 = random.Next(1, 15);

                list.Add(new Product
                {
                    Name = $"Juguete colección Marvel # {valor}",
                    Description = $"Juguete superheroe # {valor}",
                    Company = "Disney",
                    AgeRestriction = valor2,
                    Price = random.Next(),
                    ProductTypeId = 2,
                    Active = true,
                    SoldOut = false
                });
            }

            Context.Set<Product>().AddRange(list);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
