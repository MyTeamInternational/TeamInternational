using BLL.Abstract;
using BLL.ViewModels.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using TeamProject.DAL.Entities;

namespace BLL.VMBuilders
{
    public class MovieVMBuilder : IMovieVMBuilder
    {
        public IEnumerable<MovieModel> GetVMList(IEnumerable<Movie> resultList)
        {
            return resultList.Select(e => new MovieModel { Name = e.Name,  });
        }
    }
}
