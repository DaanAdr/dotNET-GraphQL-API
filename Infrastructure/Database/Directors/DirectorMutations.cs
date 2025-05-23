﻿using System.ComponentModel.DataAnnotations;
using graphql_api.DataModels;
using HotChocolate.Authorization;

namespace graphql_api.Infrastructure.Database.Directors
{
    [MutationType]
    public static class DirectorMutations
    {
        [Authorize]     // DataAnnotation needs to be added the the functions, not the class.
                        // Adding it to the class will cause other mutations to also require authorization, like logging in and registering.
                        // Though it might be desirable to use the DataAnnotation [AllowAnonymous] for those
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

        [Authorize]
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

        [Authorize]
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
