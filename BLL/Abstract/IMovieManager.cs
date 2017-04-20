using System.Collections.Generic;
using TeamProject.DAL.Entities;

namespace BLL.Abstract
{
    public interface IMovieManager
    {
        IEnumerable<Movie> GetMovies(int v);
        IEnumerable<Movie> GetMovies(string name, int pageSize);
        Movie CreateMovie(Movie movie);
        Movie GetMovie(int id);
        void Update(Movie movie);
        IDictionary<string, object> GetAutoCompliteFormat(string query);
    }
}