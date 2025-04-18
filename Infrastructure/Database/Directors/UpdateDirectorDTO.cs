namespace graphql_api.Infrastructure.Database.Directors
{
    public class UpdateDirectorDTO
    {
        public string? Firstname { get; set; }  // Properties made nullable so only the necessary fields get updated
        public string? Surname { get; set; }
    }
}
