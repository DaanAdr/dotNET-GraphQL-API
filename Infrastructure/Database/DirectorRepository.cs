using graphql_api.DataModels;

namespace graphql_api.Infrastructure.Database
{
    public class DirectorRepository
    {
        private readonly List<Director> _directors = new List<Director>
        {
            new Director { Id = 1, Firstname = "Martin", Surname = "Scorsese"},
            new Director { Id = 2, Firstname = "Peter", Surname = "Berg"},
        };

        public IQueryable<Director> GetDirectors()
        {
            return _directors.AsQueryable();
        }

        public Director GetDirector(int id)
        {
            return _directors.FirstOrDefault(d => d.Id == id);
        }

        public Director AddDirector(Director director)
        {
            int id = _directors.Max(d => d.Id) + 1;
            director.Id = id;
            _directors.Add(director);
            return director;
        }

        public Director UpdateDirector(int id, Director director)
        {
            Director directorToUpdate = GetDirector(id);

            if (directorToUpdate != null)
            {
                _directors.Remove(director);
                director.Id = directorToUpdate.Id;
                _directors.Add(director);
                return director;
            }
            return null;
        }

        public bool DeleteDirector(int id)
        {
            Director director = GetDirector(id);

            if (director != null)
            {
                _directors.Remove(director);
                return true;
            }
            return false;
        }
    }
}
