using System;
using System.ComponentModel.DataAnnotations;

namespace DateCalculator.Models
{
    public class BussinessViewModel
    {
        [Required(ErrorMessage = "Please select the start date")]
        public string FirstDate { get; set; }

        [Required(ErrorMessage = "Please select the start month")]
        public string FirstMonth { get; set; }

        [Required(ErrorMessage = "Please select the start year")]
        public string FirstYear { get; set; }

        [Required(ErrorMessage = "Please select the end date")]
        public string SecondDate { get; set; }

        [Required(ErrorMessage = "Please select the end month")]
        public string SecondMonth { get; set; }

        [Required(ErrorMessage = "Please select the end year")]
        public string SecondYear { get; set; }

        public bool IncludeEndDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool HasResult { get; set; }

        public string ResultRefineOption { get; set; }

        public string Option { get; set; }
        
        public double Days { get; set; }
        public double Hours { get; set; }
        public double Minutes { get; set; }
        public double Seconds { get; set; }
        public string Weeks { get; set; }
        public string LongDurations { get; set; }
    }
}