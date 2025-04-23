using System.ComponentModel.DataAnnotations;

namespace graphql_api.Infrastructure.Database.Actors
{
    public class AddActorDTO
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
    }
}
