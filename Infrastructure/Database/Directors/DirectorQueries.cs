using graphql_api.DataModels;
using HotChocolate.Execution.Processing;
using Microsoft.EntityFrameworkCore;

namespace graphql_api.Infrastructure.Database.Directors
{
    [QueryType]
    public static class DirectorQueries
    {
        public static async Task<List<Director>> GetDirectorsAsync(
            AppDbContext databaseContext,
            ISelection selection)   // Ensures only the desired properties are fetched from the database
        {
            return await databaseContext.Director.Select(selection).ToListAsync();
        }

        public static async Task<Director> GetDirectorByIdAsync(
            int id,
            AppDbContext databaseContext,
            ISelection selection)
        {
            return await databaseContext.Director.Select(selection).FirstOrDefaultAsync(d => d.Id == id);
        }

        // Fetched all data from the database, for use with internal functions such as mutations
        internal static async Task<Director> GetDirectorByIdAsync(
            int id,
            AppDbContext databaseContext)
        {
            return await databaseContext.Director.FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
