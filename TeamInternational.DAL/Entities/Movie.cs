﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamInternational.DAL.Entities.Interfaces;

namespace TeamInternational.DAL.Entities
{
    public class Movie : IEntity
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public int AgeLimit { get; set; }

        public int ReleaseYear { get; set; }

        public ICollection<Picture> Pictures { get; set; }
    }
}
