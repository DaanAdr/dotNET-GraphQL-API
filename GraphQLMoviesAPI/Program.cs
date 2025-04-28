using System.Text;
using graphql_api.Helperclasses;
using graphql_api.Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //GraphQL
        builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
                {
                    optionsBuilder.UseMySql(connectionString: builder.Configuration["DBConnectionString"]!, serverVersion: new MySqlServerVersion(versionString: "11.7.2-MariaDB"));
                });

        builder.Services
            .AddGraphQLServer()
            .AddGraphQLMoviesAPITypes()
            .AddAuthorization();

        builder.Services.AddSingleton<AuthLogic>();

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidIssuer = builder.Configuration["TokenSettings:Issuer"],
                        ValidAudience = builder.Configuration["TokenSettings:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["TokenSettings:Key"])),
                        ValidateIssuer = true,
                        // ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
            });

        WebApplication app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapGraphQL();
        app.Run();
    }
}