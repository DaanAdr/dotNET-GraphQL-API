using graphql_api.DataModels;

namespace graphql_api.Infrastructure.Database.Movies
{
    public class MovieType  // Essentially functions as a return DTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Director Director { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
