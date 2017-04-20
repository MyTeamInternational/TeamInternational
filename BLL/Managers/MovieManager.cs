using BLL.Abstract;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TeamProject.DAL;
using TeamProject.DAL.Entities;

namespace BLL.Managers
{
    public class MovieManager : IMovieManager
    {
        [Inject]
        ICinemaWork work;
        private string RegexPWordsStartOnPrefix = "({0}\\w+(^\\s)*)";
        private string RegexPWordStartOnWord = "^{0}\\w+";
        public MovieManager(ICinemaWork work)
        {
            this.work = work;
        }
        public IEnumerable<Movie> GetMovies(string name, int PageSize)
        {
            return work.Movies.Items.Where(e => e.Name.Contains(name)).Take(PageSize);
        }

        public int GetMoviesCount()
        {
            return work.Movies.Items.Count();
        }

        private bool match(Movie e, string name)
        {
            return new Regex(string.Format(RegexPWordStartOnWord, name), RegexOptions.IgnoreCase).Match(e.Name).Success;

        }
        public IEnumerable<Movie> GetMovies(int count)
        {
            return work.Movies.Items.Take(count);
        }

        public Movie CreateMovie(Movie movie)
        {
            work.Movies.Create(movie);
            work.Save();
            return work.Movies.Items.ToList().LastOrDefault(e => e.Name == movie.Name);

        }

        public Movie GetMovie(int id)
        {
            return work.Movies.Get(id);
        }

        public void Update(Movie movie)
        {
            work.Movies.Update(movie);
            work.Save();
        }

        public IDictionary<string, object> GetAutoCompliteFormat(string query)
        {
            var responseData = new Dictionary<string, object>();
            responseData.Add("query", query);
            responseData.Add("suggestions", work.Movies.Items.Where(e => e.Name.Contains(query)).Take(5).Select(e => new { value = e.Name, data = e.ID }));
            return responseData;
        }
    }
}
