using graphql_api.DTOs;
using graphql_api.Infrastructure.Database.DataModels;

namespace graphql_api.Infrastructure.Database
{
    public class Mutation
    {
        private readonly DirectorRepository _directorRepository;

        public Mutation(DirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }

        public Director AddDirector(AddDirector director)
        {
            Director payload = new Director
            {
                Firstname = director.Firstname,
                Surname = director.Surname
            };

            return _directorRepository.AddDirector(payload);
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
