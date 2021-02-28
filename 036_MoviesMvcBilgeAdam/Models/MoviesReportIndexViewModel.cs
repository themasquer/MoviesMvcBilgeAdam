using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace _036_MoviesMvcBilgeAdam.Models
{
    public class MoviesReportIndexViewModel
    {
        public List<MovieReportInnerJoinModel> InnerJoinList { get; set; }

        public SelectList OnlyMatchedSelectList { get; set; }

        [DisplayName("Only Matched")]
        //public int OnlyMatchedValue { get; set; }
        public int OnlyMatchedValue { get; set; } = 1;
    }
}