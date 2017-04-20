using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject.DAL.Entities.Interfaces;

namespace TeamProject.DAL.Entities
{
    // Will be keeping in file system.
    public class Picture : IEntity
    {
        [Key]
        public int ID { get; set; }

        public int MovieID { get; set; }

        [ForeignKey("MovieID")]
        public Movie Movie { get; set; }
    }
}
