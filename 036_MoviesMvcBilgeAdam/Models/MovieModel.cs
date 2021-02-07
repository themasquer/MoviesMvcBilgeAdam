using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _036_MoviesMvcBilgeAdam.Models
{
    public class MovieModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(4)]
        public string ProductionYear { get; set; }

        public double? BoxOfficeReturn { get; set; }

        public List<DirectorModel> Directors { get; set; }

        public List<ReviewModel> Reviews { get; set; }
    }
}