using graphql_api.DataModels;
using graphql_api.Helperclasses;

namespace graphql_api.Infrastructure.Database.Users
{
    [MutationType]
    public sealed class UserMutations(AuthLogic tokenLogic)
    {
        public async Task<User> RegisterUser(
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

        public async Task<LoginUserResultDTO> LoginUser(
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
                    LoginUserResultDTO loggedInUser = new LoginUserResultDTO
                    {
                        Email = userDTO.Email,
                        Token = tokenLogic.GetAuthToken(foundUser)
                    };

                    return loggedInUser;
                }
                else
                {
                    // Password is incorrect
                    throw new UnauthorizedAccessException("Invalid password.");
                }
            }
            else
            {
                // User not found
                throw new System.Collections.Generic.KeyNotFoundException("User not found.");
            }
        }
    }
}
