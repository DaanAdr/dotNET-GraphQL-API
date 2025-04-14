using System.ComponentModel.DataAnnotations;
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
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(director);
            bool isValid = Validator.TryValidateObject(director, validationContext, validationResults, true);

            if (!isValid)
            {
                var errMessage = validationResults.First().ErrorMessage;
                throw new GraphQLException(errMessage);
            }

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
            Director directorToUpdate = await DirectorQueries.GetDirectorByIdAsync(id, databaseContext);

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
            Director directorToRemove = await DirectorQueries.GetDirectorByIdAsync(id, databaseContext);

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
