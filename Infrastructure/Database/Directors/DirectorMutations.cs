using graphql_api.DataModels;

namespace graphql_api.Infrastructure.Database.Directors
{
    [MutationType]
    public static class DirectorMutations
    {
        public static async Task<Director> AddDirectorAsync(
            AddDirectorDTO director,
            AppDbContext databaseContext,
            CancellationToken cancellationToken)
        {
            Director payload = new Director
            {
                Firstname = director.Firstname,
                Surname = director.Surname
            };

            databaseContext.Director.Add(payload);
            await databaseContext.SaveChangesAsync(cancellationToken);

            return payload;
        }

        public static async Task<Director> UpdateDirector(
            int id,
            UpdateDirectorDTO director,
            AppDbContext databaseContext,
            CancellationToken cancellationToken)
        {
            Director directorToUpdate = DirectorQueries.GetDirectorById(id, databaseContext);

            if (directorToUpdate != null)
            {
                if (!string.IsNullOrEmpty(director.Surname)) directorToUpdate.Surname = director.Surname;

                if (!string.IsNullOrEmpty(director.Firstname)) directorToUpdate.Firstname = director.Firstname;

                databaseContext.Director.Update(directorToUpdate);
                await databaseContext.SaveChangesAsync(cancellationToken);

                return directorToUpdate;
            }
            return null;
        }

        public static async Task<bool> DeleteDirector(
            int id,
            AppDbContext databaseContext,
            CancellationToken cancellationToken)
        {
            Director directorToRemove = DirectorQueries.GetDirectorById(id, databaseContext);

            if(directorToRemove != null)
            {
                databaseContext.Director.Remove(directorToRemove);
                await databaseContext.SaveChangesAsync(cancellationToken);
                return true;
            }

            return false;
        }
    }
}
