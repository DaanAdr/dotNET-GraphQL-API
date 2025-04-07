using graphql_api.DataModels;

namespace graphql_api.Infrastructure.Database
{
    public class Mutation
    {
        private readonly DirectorRepository _directorRepository;

        public Mutation(DirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }

        public Director AddDirector(Director director)
        {
            return _directorRepository.AddDirector(director);
        }

        public Director UpdateDirector(int id, Director director)
        {
            return _directorRepository.UpdateDirector(id, director);
        }

        public bool DeleteDirector(int id)
        {
            return _directorRepository.DeleteDirector(id);
        }
    }
}
