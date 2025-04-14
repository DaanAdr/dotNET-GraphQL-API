using graphql_api.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//GraphQL
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseMySql(connectionString: builder.Configuration["DBConnectionString"]!, serverVersion: new MySqlServerVersion(versionString: "11.7.2-MariaDB"));
        });

builder.Services
    .AddGraphQLServer()
    .AddGraphQLMoviesAPITypes();
    //.AddMutationType<Mutation>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGraphQL();
app.Run();
