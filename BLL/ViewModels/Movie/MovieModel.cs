using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamProject.DAL.Entities;

namespace BLL.ViewModels.Movie
{
    public class MovieModel
    {
       [Required]
        public string Name { get; set; }


        public int AgeLimit { get; set; }

        public int ReleaseYear { get; set; }
        public MovieModel()
        {
        }
        public ICollection<Picture> Pictures { get; set; }
    }
}