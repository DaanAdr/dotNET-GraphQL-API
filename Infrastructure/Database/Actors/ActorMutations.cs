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
    }
}
