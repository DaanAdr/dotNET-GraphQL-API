using graphql_api.Infrastructure.Database.DataModels;

namespace graphql_api.Infrastructure.Database
{
    public class DirectorRepository
    {
        private readonly AppDbContext _appDbContext;
        public DirectorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IQueryable<Director> GetDirectors()
        {
            return _appDbContext.Director.AsQueryable();
        }

        public Director GetDirector(int id)
        {
            return _appDbContext.Director.FirstOrDefault(d => d.Id == id);
        }

        public Director AddDirector(Director director)
        {
            _appDbContext.Director.Add(director);
            return director;
        }

        public Director UpdateDirector(int id, Director director)
        {
            Director directorToUpdate = GetDirector(id);

            if (directorToUpdate != null)
            {
                _appDbContext.Director.Remove(directorToUpdate);
                director.Id = directorToUpdate.Id;
                _appDbContext.Director.Add(director);
                return director;
            }
            return null;
        }

        public bool DeleteDirector(int id)
        {
            Director director = GetDirector(id);

            if (director != null)
            {
                _appDbContext.Director.Remove(director);
                return true;
            }
            return false;
        }
    }
}
