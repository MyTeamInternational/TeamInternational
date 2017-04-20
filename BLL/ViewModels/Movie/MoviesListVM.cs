using BLL.ViewModels.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.ViewModels.Movie
{
    public class MoviesListVM
    {
        public IEnumerable<MovieModel> Movies { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}

