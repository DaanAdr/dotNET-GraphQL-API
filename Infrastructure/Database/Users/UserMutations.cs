using graphql_api.DataModels;
using graphql_api.Helperclasses;

namespace graphql_api.Infrastructure.Database.Users
{
    [MutationType]
    public static class UserMutations
    {
        public static async Task<User> RegisterUser(
            UserDTO userDTO,
            AppDbContext databaseContext,
            CancellationToken cancellationToken)
        {
            string hashedPassword = PasswordHashLogic.HashPassword(userDTO.Password);

            User payload = new User
            {
                Email = userDTO.Email,
                Password = hashedPassword
            };

            await databaseContext.Users.AddAsync(payload);
            await databaseContext.SaveChangesAsync();

            return payload;
        }

        public static async Task<bool> LoginUser(
            UserDTO userDTO,
            AppDbContext databaseContext,
            CancellationToken cancellationToken)
        {
            // Search user with email
            User? foundUser = databaseContext.Users.SingleOrDefault(u => u.Email == userDTO.Email);

            if(foundUser != null)
            {
                // Check if password is correct
                if(PasswordHashLogic.VerifyPassword(userDTO.Password, foundUser.Password))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
