using BLL.ViewModels.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject.DAL.Entities;

namespace BLL.Helpers
{
    public  static class MovieHelper
    {
        public static Movie GetByModel(this MovieModel model)
        {   
            return new Movie
            {
                Name = model.Name,
                ReleaseYear = model.ReleaseYear,
                AgeLimit = 0
            };
        }
    }
}
