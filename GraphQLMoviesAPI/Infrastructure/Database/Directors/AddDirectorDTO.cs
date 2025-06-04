using System.ComponentModel.DataAnnotations;

namespace graphql_api.Infrastructure.Database.Directors
{
    public class AddDirectorDTO
    {
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Firstname can only contain letters.")]
        [StringLength(20, MinimumLength = 2)]
        public string Firstname { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Surname can only contain letters.")] 
        [StringLength(20, MinimumLength = 2)]
        public string Surname { get; set; }
    }
}
