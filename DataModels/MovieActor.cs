﻿using System.ComponentModel.DataAnnotations;

namespace graphql_api.DataModels
{
    public class MovieActor
    {
        [Key]
        public int Id { get; set; }
        public int ActorId { get; set; }
        public int MovieId { get; set; }

        public Actor Actor { get; set; }
        public Movie Movie { get; set; }
    }
}
