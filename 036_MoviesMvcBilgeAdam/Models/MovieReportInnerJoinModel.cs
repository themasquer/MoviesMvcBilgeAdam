using System;
using System.ComponentModel;

namespace _036_MoviesMvcBilgeAdam.Models
{
    public class MovieReportInnerJoinModel
    {
        [DisplayName("Movie Name")]
        public string MovieName { get; set; }

        [DisplayName("Movie Production Year")] 
        public string MovieProductionYear { get; set; }

        public double? MovieBoxOfficeReturnValue { get; set; }

        [DisplayName("Movie Box Office Return")]
        public string MovieBoxOfficeReturn { get; set; }

        [DisplayName("Director Name")] 
        public string DirectorFullName { get; set; }

        public bool DirectorRetiredValue { get; set; }

        [DisplayName("Is Director Retired?")] 
        public string DirectorRetired { get; set; }

        [DisplayName("Review Content")] 
        public string ReviewContent { get; set; }

        public int ReviewRatingValue { get; set; }

        [DisplayName("Review Rating")] 
        public string ReviewRating { get; set; }

        [DisplayName("Reviewer")] 
        public string ReviewReviewer { get; set; }

        public DateTime ReviewDateValue { get; set; }

        [DisplayName("Review Date")] 
        public string ReviewDate { get; set; }
    }
}