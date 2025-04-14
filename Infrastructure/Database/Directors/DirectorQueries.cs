using graphql_api.DataModels;
using HotChocolate.Execution.Processing;

namespace graphql_api.Infrastructure.Database.Directors
{
    [QueryType]
    public static class DirectorQueries
    {
        public static IQueryable<Director> GetDirectors(
            AppDbContext databaseContext,
            ISelection selection)   // Ensures only the desired properties are fetched from the database
        {
            return databaseContext.Director.Select(selection).AsQueryable();
        }

        public static Director GetDirectorById(
            int id,
            AppDbContext databaseContext,
            ISelection selection)
        {
            return databaseContext.Director.Select(selection).FirstOrDefault(d => d.Id == id);
        }
    }
}
