
using BLL.ViewModels.Movie;
using System.Collections.Generic;
using TeamProject.DAL.Entities;

namespace BLL.Abstract
{
    public interface IMovieVMBuilder
    {
        IEnumerable<MovieModel> GetVMList(IEnumerable<Movie> resultList);
    }
}