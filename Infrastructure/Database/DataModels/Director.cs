using System.ComponentModel.DataAnnotations;

namespace graphql_api.Infrastructure.Database.DataModels
{
    public class Director
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
    }
}
