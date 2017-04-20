using BLL.ViewModels.Movie;
using MvcUi.Models;
using System.Collections.Generic;

namespace MvcUi.Helpers
{
    public static class MovieListHelper
    {
        public static MoviesListVM MoviesListVM(IEnumerable<MovieModel> movies, PagingInfo pagingInfo)
        {
            MoviesListVM movieListVM = new MoviesListVM
            {
                Movies = movies,
                PagingInfo = pagingInfo
            };
            return movieListVM;
        }
    }
}
