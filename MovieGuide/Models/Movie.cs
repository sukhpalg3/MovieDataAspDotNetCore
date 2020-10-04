using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieGuide.Models
{
    public class Movie
    {
        [Key]
        public int MovieID { get; set; }

        [Required]
        [StringLength(300)]
        public string MovieName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int GenreID { get; set; }

        public Genre con { get; set; }

        public ICollection<Photo> Photos { get; set; }

    }
}
