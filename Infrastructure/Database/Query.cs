using graphql_api.DataModels;

namespace graphql_api.Infrastructure.Database
{
    public class Query
    {
        private readonly DirectorRepository _directorRepository;

        public Query(DirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }

        public IQueryable<Director> GetDirectors()
        {
            return _directorRepository.GetDirectors();
        }

        public Director GetDirector(int id)
        {
            return _directorRepository.GetDirector(id);
        }
    }
}
