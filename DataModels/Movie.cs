using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace graphql_api.DataModels
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int DirectorId { get; set; }

        public Director Director { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
