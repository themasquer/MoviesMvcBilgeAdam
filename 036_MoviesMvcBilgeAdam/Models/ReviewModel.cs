using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace _036_MoviesMvcBilgeAdam.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        [StringLength(200)]
        public string Reviewer { get; set; }

        [Required(ErrorMessage = "{0} must not be empty!")]
        public DateTime? Date { get; set; }

        [DisplayName("Date")] 
        public string DateText => Date.HasValue ? Date.Value.ToString("yyyy/MM/dd", new CultureInfo("en")) : "";

        public int MovieId { get; set; }
        public MovieModel Movie { get; set; }

        public List<int> AllRatings { get; set; }
    }
}