using graphql_api.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MySql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DotNet.Testcontainers.Containers;

namespace GraphQLMoviesAPITests
{
    public class e2eTestsWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MySqlContainer _mySqlContainer = new MySqlBuilder()
            .WithName("e2e_db")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(x =>
            {
                // Remove the default database connection established in Program.cs
                x.Remove(x.Single(a => a.ServiceType == typeof(DbContextOptions<AppDbContext>)));

                // Add a new database for integration tests
                x.AddDbContext<AppDbContext>(a =>
                {
                    string constring = _mySqlContainer.GetConnectionString();
                    // a.UseMySql(constring);
                    a.UseMySql(connectionString: constring, serverVersion: new MySqlServerVersion(versionString: "11.7.2-MariaDB"));
                });
            });
        }

        public async Task DisposeAsync()
        {
            await _mySqlContainer.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            await _mySqlContainer.StartAsync();

            // Apply migrations
            using (var scope = Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();

                await db.Database.MigrateAsync();
            }
        }
    }
}