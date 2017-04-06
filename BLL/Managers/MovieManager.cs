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
        Regex regex;
        private string RegexPWordsStartOnPrefix= "({0}\\w+(^\\s)*)";

        public MovieManager(ICinemaWork work)
        {
            this.work = work;
        }

        public IEnumerable<Movie> GetMovies(string name)
        {
            return work.Movies.Items.ToList().Where( e=>match(e,name));
        }
        private bool match(Movie e,string name)
        {
            regex =  new Regex(String.Format(RegexPWordsStartOnPrefix, name), RegexOptions.IgnoreCase);
            return regex.Match(e.Name).Success;  
        }
        public IEnumerable<Movie> GetMovies(int count)
        {
            return work.Movies.Items.Take(count);
        }
    }
}
