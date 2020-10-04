using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieGuide.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }

        [Required]
        [StringLength(200)]
        public string GenreName { get; set; }

        public ICollection<Movie> Movies { get; set; }


    }
}
