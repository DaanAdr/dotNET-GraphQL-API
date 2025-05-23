﻿using graphql_api.DataModels;
using HotChocolate.Authorization;

namespace graphql_api.Infrastructure.Database.Movies
{
    [MutationType]
    public static class MovieMutations
    {
        [Authorize]
        public static async Task<Movie> AddMovie(
            AddMovieDTO movieDTO,
            AppDbContext databaseContext,
            CancellationToken cancellationToken)
        {
            Movie payload = new Movie
            {
                DirectorId = movieDTO.DirectorId,
                Title = movieDTO.Title
            };

            await databaseContext.Movies.AddAsync(payload);
            await databaseContext.SaveChangesAsync(cancellationToken);

            return payload;
        }
    }
}
