using graphql_api.Infrastructure.Database.DataModels;

namespace graphql_api.Infrastructure.Database.Queries
{
    [QueryType]
    public static class DirectorQueries
    {
        public static IQueryable<Director> GetDirectors(
            AppDbContext databaseContext)
        {
            return databaseContext.Director.AsQueryable();
        }

        public static Director GetDirectorById(
            int id,
            AppDbContext databaseContext)
        {
            return databaseContext.Director.FirstOrDefault(d => d.Id == id);
        }
    }
}
