using System.ComponentModel.DataAnnotations;

namespace graphql_api.DataModels
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
    }
}
