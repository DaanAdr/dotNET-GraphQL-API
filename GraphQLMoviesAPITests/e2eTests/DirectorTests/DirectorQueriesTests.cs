using System.Text;
using Newtonsoft.Json;

namespace GraphQLMoviesAPITests.e2eTests.DirectorTests
{
    public class DirectorQueriesTests : IClassFixture<e2eTestsWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        public DirectorQueriesTests(e2eTestsWebApplicationFactory factory)
        {
            // _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:44328/graphql") };
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:29374/graphql") };

            //var testc = factory.CreateClient();
            
            //var tets = 2;
        }

        [Fact]
        public async Task GetAllDirectorsAsync()
        {
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

            var response = await _httpClient.PostAsync("", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var test2 = 2;

            // Assert
        }
    }
}
