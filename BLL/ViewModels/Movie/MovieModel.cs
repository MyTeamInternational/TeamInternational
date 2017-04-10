using System.Collections.Generic;
using TeamProject.DAL.Entities;

namespace BLL.ViewModels.Movie
{
    public class MovieModel
    {

        public string Name { get; set; }

        public int ReleaseYear { get; set; }

        public ICollection<View> Views { get; set; }
    }
}