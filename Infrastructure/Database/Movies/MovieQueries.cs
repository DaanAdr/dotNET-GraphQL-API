using graphql_api.DataModels;
using HotChocolate.Execution.Processing;
using Microsoft.EntityFrameworkCore;

namespace graphql_api.Infrastructure.Database.Movies
{
    [QueryType]
    public static class MovieQueries
    {
        public static async Task<List<Movie>> GetMoviesAsync(
            AppDbContext databaseContext,
            ISelection selection)
        {
            return await databaseContext.Movies.Select(selection).ToListAsync();
        }
    }
}
