using System.ComponentModel.DataAnnotations;

namespace graphql_api.Infrastructure.Database.Directors
{
    public class AddDirectorDTO
    {
        public string Firstname { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Surname can only contain letters.")] // Auto validation doesn't really work
        [StringLength(20)]
        public string Surname { get; set; }
    }
}
