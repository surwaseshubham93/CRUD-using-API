using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }
        public DatabaseContext(DbContextOptions<DatabaseContext>options):base(options) { }

        public DbSet<EmployeeModel> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeModel>().HasData(
                new EmployeeModel
                {
                    Id = 1,
                    Name = "Prakash",
                    Designation = "Developer",
                    Description = "developer with 3 years of experience",
                    EmployeeAge = 24,
                    EmployeePhone = 7894561230
                },
                new EmployeeModel
                {
                    Id = 2,
                    Name = "Ganesh",
                    Designation = "Tester",
                    Description = "tester with 1 year of experience",
                    EmployeeAge = 22,
                    EmployeePhone = 0123654789
                },
                new EmployeeModel
                {
                    Id = 3,
                    Name = "Om",
                    Designation = "DA",
                    Description = "DA with 1 year of experience",
                    EmployeeAge = 25,
                    EmployeePhone = 0123654259
                });
        }

    }
}
