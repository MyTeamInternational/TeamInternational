using System.Collections.Generic;
using System.Web;
using TeamProject.DAL.Entities;

namespace BLL.Abstract
{
    public interface IMovieManager
    {
        IEnumerable<Movie> GetMovies(int v);
        IEnumerable<Movie> GetMovies(string name);
        Movie CreateMovie(Movie movie);
        Movie GetMovie(int id);
        void Update(Movie movie);
    }
}