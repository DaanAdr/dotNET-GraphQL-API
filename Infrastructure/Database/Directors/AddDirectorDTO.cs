using System.ComponentModel.DataAnnotations;

namespace graphql_api.Infrastructure.Database.Directors
{
    public class AddDirectorDTO
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Surname { get; set; }
    }
}
