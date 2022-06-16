using CodeChallenge.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CodeChallenge.DataAccess
{
    public class CodeChallengeDbContext : DbContext
    {
        public CodeChallengeDbContext()
        {

        }
    
        //Constructor para inicializar con dbcontext
        public CodeChallengeDbContext(DbContextOptions<CodeChallengeDbContext> options) : base(options)
        {

        }

        //CONEXION DIRECTA - Movida cadena de conexion al archivo "appsettings.Development.json"
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    //optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;");
        //    optionsBuilder.UseSqlServer(@"Server=BOL-LT-220606C\SQLEXPRESS01;Database=CodeChallenge;Trusted_Connection=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Movido a archivo de configuracion "ProductConfiguration"
            //modelBuilder.Entity<Product>()
            //    .Property(e => e.Price).HasPrecision(5, 2);

            //Aplica el uso de las configuraciones personalizadas de la carpeta "Configurations"
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        //public DbSet<Product> Products { get; set; }
        //public DbSet<ProductType> ProductTypes { get; set; }
    }
}
