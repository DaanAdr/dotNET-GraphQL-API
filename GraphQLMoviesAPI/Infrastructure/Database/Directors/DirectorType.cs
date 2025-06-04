using graphql_api.DataModels;
using graphql_api.Infrastructure.Database.Movies;

namespace graphql_api.Infrastructure.Database.Directors
{
    public class DirectorType
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }

        public List<MovieType> Movies { get; set; }
    }
}
