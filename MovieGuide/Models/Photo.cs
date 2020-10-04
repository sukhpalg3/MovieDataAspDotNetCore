using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieGuide.Models
{
    public class Photo
    {
        [Key]
        public int PhotoID { get; set; }

        [Required]
        [StringLength(50)]
        public string ExtName { get; set; }

        [Required]
        public int MovieID { get; set; }

        public Movie mName { get; set; }

        [NotMapped]
        public BufferedSingleFileUploadDb FileUpload { get; set; }
    }
    public class BufferedSingleFileUploadDb
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}
