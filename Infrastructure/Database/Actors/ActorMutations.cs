using graphql_api.DataModels;

namespace graphql_api.Infrastructure.Database.Actors
{
    [MutationType]
    public static class ActorMutations
    {
        public static async Task<Actor> AddActorAsync(
            AddActorDTO actor,
            AppDbContext databaseContext,
            CancellationToken cancellationToken)
        {
            Actor payload = new Actor
            {
                Firstname = actor.Firstname,
                Surname = actor.Surname
            };

            await databaseContext.Actors.AddAsync(payload);
            await databaseContext.SaveChangesAsync(cancellationToken);

            return payload;
        }

        public static async Task<bool> AddActorToMovie(
            AddActorToMovieInput addActorToMovieInput,
            AppDbContext databaseContext,
            CancellationToken cancellationToken)
        {
            MovieActor payload = new MovieActor
            {
                MovieId = addActorToMovieInput.MovieId,
                ActorId = addActorToMovieInput.ActorId
            };

            await databaseContext.MovieActors.AddAsync(payload);
            await databaseContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
