using graphql_api.DataModels;
using HotChocolate.Execution.Processing;
using Microsoft.EntityFrameworkCore;

namespace graphql_api.Infrastructure.Database.Movies
{
    [QueryType]
    public static class MovieQueries
    {
        public static async Task<List<MovieType>> GetMoviesAsync(
            AppDbContext databaseContext,
            ISelection selection)
        {
            // return await databaseContext.Movies.Select(selection).ToListAsync();
            List<Movie> movies = await databaseContext.Movies
                .Include(m => m.MovieActors)
                    .ThenInclude(ma => ma.Actor)
                .Include(m => m.Director)
                .ToListAsync();

            List<MovieType> movieTypes = movies.Select(m => new MovieType()
            {
                Id = m.Id,
                Title = m.Title,
                Director = m.Director,
                Actors = m.MovieActors.Select(ma => ma.Actor).ToList()
            }).ToList();

            return movieTypes;
        }
    }
}
