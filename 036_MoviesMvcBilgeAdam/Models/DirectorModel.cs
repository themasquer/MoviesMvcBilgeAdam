using System.ComponentModel.DataAnnotations;

namespace _036_MoviesMvcBilgeAdam.Models
{
    public class DirectorModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Surname { get; set; }

        public bool Retired { get; set; }
    }
}