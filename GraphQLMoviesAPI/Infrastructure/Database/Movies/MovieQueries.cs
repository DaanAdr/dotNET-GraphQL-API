using graphql_api.DataModels;
using graphql_api.Infrastructure.Database.Actors;
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
                Actors = m.MovieActors.Select(ma => new ActorType()
                {
                    Id = ma.Actor.Id,
                    Firstname = ma.Actor.Firstname,
                    Surname = ma.Actor.Surname
                }).ToList()
            }).ToList();

            return movieTypes;
        }
    }
}
