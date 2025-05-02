using System.Text;
using graphql_api.DataModels;
using graphql_api.Infrastructure.Database;
using graphql_api.Infrastructure.Database.Directors;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GraphQLMoviesAPITests.e2eTests.DirectorTests
{
    public class DirectorQueriesTests : IClassFixture<e2eTestsWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly e2eTestsWebApplicationFactory _factory;
        public DirectorQueriesTests(e2eTestsWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task GetAllDirectorsAsync()
        {
            // Arrange
            List<Director> databaseDirectors = new List<Director> {
                new Director()
                {
                    Id = 1,
                    Firstname = "Nigel",
                    Surname = "Young"
                }, 
                new Director()
                {
                    Id = 2,
                    Firstname = "Tom",
                    Surname = "Scott"
                } 
            };

            using (IServiceScope scope = _factory.Services.CreateScope())
            {
                AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Seed specific test data
                await db.Director.AddRangeAsync(databaseDirectors);
                await db.SaveChangesAsync();
            }

            // Act
            string json = JsonConvert.SerializeObject(new
            {
                query = @"
                    {
                        directors {
                            id
                            surname
                        }
                    }"
            });

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("graphql", content);

            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            List<DirectorType>? directorList = JObject.Parse(responseContent)["data"]["directors"].ToObject<List<DirectorType>>();

            // Assert
            Assert.Equal(databaseDirectors.Count, directorList.Count);

            for (int i = 0; i < directorList.Count; i++)
            {
                Assert.Equal(databaseDirectors[i].Id, directorList[i].Id);
                Assert.Equal(databaseDirectors[i].Surname, directorList[i].Surname);
            }
        }
    }
}
