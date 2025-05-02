using System.Text;
using graphql_api.DataModels;
using graphql_api.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace GraphQLMoviesAPITests.e2eTests.DirectorTests
{
    public class DirectorQueriesTests : IClassFixture<e2eTestsWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _databaseContext;
        private readonly e2eTestsWebApplicationFactory _factory;
        public DirectorQueriesTests(e2eTestsWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
            //_databaseContext = factory.Services.GetRequiredService<AppDbContext>();
            _factory = factory;
        }

        [Fact]
        public async Task GetAllDirectorsAsync()
        {
            // Arrange
            var director1 = new Director()
            {
                Id = 1,
                Firstname = "Nigel",
                Surname = "Young"
            };

            var director2 = new Director()
            {
                Id = 2,
                Firstname = "Tom",
                Surname = "Scott"
            };

            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();

                // Seed specific test data
                await db.Director.AddRangeAsync([director1, director2]);
                await db.SaveChangesAsync();
            }

            // Act
            var requestBody = new
            {
                query = @"
                    {
                        directors {
                            id
                            surname
                        }
                    }"
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("graphql", content);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var test2 = 2;

            // Assert
        }
    }
}
