using System.Text;
using graphql_api.Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

SymmetricSecurityKey signingKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes("MySuperSecretKey"));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidIssuer = "https://auth.chillicream.com",
                ValidAudience = "https://graphql.chillicream.com",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey
            };
    });

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL();
app.Run();
