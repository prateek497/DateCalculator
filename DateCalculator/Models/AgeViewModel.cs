using System;
using System.ComponentModel.DataAnnotations;

namespace DateCalculator.Models
{
    public class AgeViewModel
    {
        [Required(ErrorMessage = "Please select the date of your birth")]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "Please select the month of your birth")]
        public string BirthMonth { get; set; }

        [Required(ErrorMessage = "Please select the year of your birth")]
        public string BirthYear { get; set; }

        [Required(ErrorMessage = "Please select the date number")]
        public string AsOfNowDate { get; set; }

        [Required(ErrorMessage = "Please select the month")]
        public string AsOfNowMonth { get; set; }

        [Required(ErrorMessage = "Please select the year")]
        public string AsOfNowYear { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime NextBirthday { get; set; }

        public int NextBirthdayDayLeft { get; set; }

        public bool HasResult { get; set; }

        public double Days { get; set; }
        public double Hours { get; set; }
        public double Minutes { get; set; }
        public double Seconds { get; set; }
        public string Weeks { get; set; }
        public string LongDurations { get; set; }

        public string NextBirthdayLongDurations { get; set; }
    }
}