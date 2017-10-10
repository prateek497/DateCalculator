using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DateCalculator.Models;

namespace DateCalculator.Controllers
{
    //[HandleError]  
    public class DateCalculatorController : Controller
    {
        [HttpGet]
        public ActionResult DateCalculator()
        {
            return View(new DateViewModel()
            {
                HasResult = false
            });
        }

        [HttpPost]
        public ActionResult DateCalculator(DateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!model.FirstDate.All(char.IsDigit) || !(Convert.ToInt16(model.FirstDate) > 0) || !(Convert.ToInt16(model.FirstDate) <= 31) ||
                    !model.SecondDate.All(char.IsDigit) || !(Convert.ToInt16(model.SecondDate) > 0) || !(Convert.ToInt16(model.SecondDate) <= 31)) ModelState.AddModelError("", "Date should be between 1 to 31.");
                if (!model.FirstMonth.All(char.IsDigit) || !(Convert.ToInt16(model.FirstMonth) > 0) || !(Convert.ToInt16(model.FirstMonth) <= 12) ||
                    !model.SecondMonth.All(char.IsDigit) || !(Convert.ToInt16(model.SecondMonth) > 0) || !(Convert.ToInt16(model.SecondMonth) <= 12)) ModelState.AddModelError("", "Month should be between 1 to 12.");
                if (!model.FirstYear.All(char.IsDigit) || !(Convert.ToInt16(model.FirstYear) > 1952) || !(Convert.ToInt16(model.FirstYear) <= 2099) ||
                    !model.SecondYear.All(char.IsDigit) || !(Convert.ToInt16(model.SecondYear) > 1952) || !(Convert.ToInt16(model.SecondYear) <= 2099)) ModelState.AddModelError("", "Please enter valid year");

                DateTime startDateTime, endDateTime;
                if (!DateTime.TryParse(model.FirstYear + "-" + model.FirstMonth + "-" + model.FirstDate, out startDateTime)) ModelState.AddModelError("", "Please enter valid start date");
                if (!DateTime.TryParse(model.SecondYear + "-" + model.SecondMonth + "-" + model.SecondDate, out endDateTime)) ModelState.AddModelError("", "Please enter valid end date");

                if (ModelState.IsValid)
                {
                    if (startDateTime > endDateTime) ModelState.AddModelError("", "Oops! First date should not be greater than second date");

                    if (ModelState.IsValid)
                    {
                        model.HasResult = true;

                        TimeSpan duratioDateTime = (endDateTime - startDateTime);
                        if (model.IncludeEndDate) duratioDateTime = duratioDateTime.Add(new TimeSpan(1, 0, 0, 0));
                        model.StartDate = startDateTime;
                        model.EndDate = endDateTime;
                        model.Days = duratioDateTime.TotalDays;
                        model.Hours = duratioDateTime.TotalHours;
                        model.Minutes = duratioDateTime.TotalMinutes;
                        model.Seconds = duratioDateTime.TotalSeconds;
                        model.Weeks = string.Format("{0} Week(s) and {1} Day(s)", Math.Floor(model.Days / 7),
                            (model.Days % 7));
                        model.LongDurations = DaysToMonthsYear(endDateTime, startDateTime, model.IncludeEndDate);
                        return View(model);
                    }
                }
                return View(new DateViewModel() { HasResult = false });
            }
            return View(new DateViewModel() { HasResult = false });
        }

        [HttpGet]
        public ActionResult AgeCalculator()
        {

            return View(new AgeViewModel() { HasResult = false });
        }

        [HttpPost]
        public ActionResult AgeCalculator(AgeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!model.BirthDate.All(char.IsDigit) || !(Convert.ToInt16(model.BirthDate) > 0) || !(Convert.ToInt16(model.BirthDate) <= 31) ||
                   !model.AsOfNowDate.All(char.IsDigit) || !(Convert.ToInt16(model.AsOfNowDate) > 0) || !(Convert.ToInt16(model.AsOfNowDate) <= 31)) ModelState.AddModelError("", "Date should be between 1 to 31.");
                if (!model.BirthMonth.All(char.IsDigit) || !(Convert.ToInt16(model.BirthMonth) > 0) || !(Convert.ToInt16(model.BirthMonth) <= 12) ||
                    !model.AsOfNowMonth.All(char.IsDigit) || !(Convert.ToInt16(model.AsOfNowMonth) > 0) || !(Convert.ToInt16(model.AsOfNowMonth) <= 12)) ModelState.AddModelError("", "Month should be between 1 to 12.");
                if (!model.BirthYear.All(char.IsDigit) || !(Convert.ToInt16(model.BirthYear) > 1950) || !(Convert.ToInt16(model.BirthYear) <= 2099) ||
                    !model.AsOfNowYear.All(char.IsDigit) || !(Convert.ToInt16(model.AsOfNowYear) > 1950) || !(Convert.ToInt16(model.AsOfNowYear) <= 2099)) ModelState.AddModelError("", "Please enter valid year");

                DateTime birthDateTime, asOfNowDateTime;
                if (!DateTime.TryParse(model.BirthYear + "-" + model.BirthMonth + "-" + model.BirthDate, out birthDateTime)) ModelState.AddModelError("", "Please enter valid birth date");
                if (!DateTime.TryParse(model.AsOfNowYear + "-" + model.AsOfNowMonth + "-" + model.AsOfNowDate, out asOfNowDateTime)) ModelState.AddModelError("", "Please enter valid end date");

                if (ModelState.IsValid)
                {
                    if (birthDateTime > asOfNowDateTime) ModelState.AddModelError("", "Oops! Date of birth should not be greater than age at the date of");

                    if (ModelState.IsValid)
                    {
                        model.HasResult = true;
                        TimeSpan duratioDateTime = (asOfNowDateTime - birthDateTime);
                        model.StartDate = birthDateTime;
                        model.EndDate = asOfNowDateTime;
                        model.Days = duratioDateTime.TotalDays;
                        model.Hours = duratioDateTime.TotalHours;
                        model.Minutes = duratioDateTime.TotalMinutes;
                        model.Seconds = duratioDateTime.TotalSeconds;
                        model.Weeks = string.Format("{0} Week(s) and {1} Day(s)", Math.Floor(model.Days / 7), (model.Days % 7));
                        model.LongDurations = DaysToMonthsYear(asOfNowDateTime, birthDateTime);
                        model.NextBirthday = new DateTime(DateTime.Now.Year, birthDateTime.Month, birthDateTime.Day) > asOfNowDateTime
                            ? new DateTime(DateTime.Now.Year, birthDateTime.Month, birthDateTime.Day)
                            : new DateTime(DateTime.Now.Year, birthDateTime.Month, birthDateTime.Day).AddYears(1);
                        model.NextBirthdayDayLeft = (int)(model.NextBirthday - asOfNowDateTime).TotalDays;
                        model.NextBirthdayLongDurations = DaysToMonthsYear(model.NextBirthday, asOfNowDateTime);
                        return View(model);
                    }
                    return View(new AgeViewModel() { HasResult = false });
                }
            }
            return View(new AgeViewModel() { HasResult = false });
        }

        [HttpGet]
        public ActionResult BussinessCalculator()
        {
            return View(new BussinessViewModel()
            {
                HasResult = false
            });
        }

        [HttpPost]
        public ActionResult BussinessCalculator(BussinessViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!model.FirstDate.All(char.IsDigit) || !(Convert.ToInt16(model.FirstDate) > 0) || !(Convert.ToInt16(model.FirstDate) <= 31) ||
                      !model.SecondDate.All(char.IsDigit) || !(Convert.ToInt16(model.SecondDate) > 0) || !(Convert.ToInt16(model.SecondDate) <= 31)) ModelState.AddModelError("", "Date should be between 1 to 31.");
                if (!model.FirstMonth.All(char.IsDigit) || !(Convert.ToInt16(model.FirstMonth) > 0) || !(Convert.ToInt16(model.FirstMonth) <= 12) ||
                    !model.SecondMonth.All(char.IsDigit) || !(Convert.ToInt16(model.SecondMonth) > 0) || !(Convert.ToInt16(model.SecondMonth) <= 12)) ModelState.AddModelError("", "Month should be between 1 to 12.");
                if (!model.FirstYear.All(char.IsDigit) || !(Convert.ToInt16(model.FirstYear) > 1952) || !(Convert.ToInt16(model.FirstYear) <= 2099) ||
                    !model.SecondYear.All(char.IsDigit) || !(Convert.ToInt16(model.SecondYear) > 1952) || !(Convert.ToInt16(model.SecondYear) <= 2099)) ModelState.AddModelError("", "Please enter valid year");

                DateTime startDateTime, endDateTime;
                if (!DateTime.TryParse(model.FirstYear + "-" + model.FirstMonth + "-" + model.FirstDate, out startDateTime)) ModelState.AddModelError("", "Please enter valid start date");
                if (!DateTime.TryParse(model.SecondYear + "-" + model.SecondMonth + "-" + model.SecondDate, out endDateTime)) ModelState.AddModelError("", "Please enter valid end date");

                if (ModelState.IsValid)
                {
                    if (startDateTime > endDateTime) ModelState.AddModelError("", "Oops! First date should not be greater than second date");

                    if (ModelState.IsValid)
                    {
                        model.HasResult = true;

                        TimeSpan duratioDateTime = (endDateTime - startDateTime);
                        if (model.IncludeEndDate) duratioDateTime = duratioDateTime.Add(new TimeSpan(1, 0, 0, 0));
                        model.StartDate = startDateTime;
                        model.EndDate = endDateTime;

                        if ((model.ResultRefineOption.Equals("exclude") && model.Option.Equals("nodays")) || (model.ResultRefineOption.Equals("includeonly") && model.Option.Equals("alldays")))
                        {
                            model.Days = duratioDateTime.TotalDays;
                            model.Hours = duratioDateTime.TotalHours;
                            model.Minutes = duratioDateTime.TotalMinutes;
                            model.Seconds = duratioDateTime.TotalSeconds;
                            model.Weeks = string.Format("{0} Week(s) and {1} Day(s)", Math.Floor(model.Days / 7), (model.Days % 7));
                            model.LongDurations = DaysToMonthsYear(endDateTime, startDateTime, model.IncludeEndDate);
                            return View(model);
                        }

                        if (model.ResultRefineOption.Equals("exclude") && model.Option.Equals("weekends"))
                        {
                            double daysCount = duratioDateTime.TotalDays;
                            for (DateTime date = model.StartDate; date <= model.EndDate; date = date.AddDays(1))
                            {
                                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) daysCount -= 1;
                            }
                            TimeSpan duration = TimeSpan.FromDays(daysCount);
                            model.Days = duration.TotalDays;
                            model.Hours = duration.TotalHours;
                            model.Minutes = duration.TotalMinutes;
                            model.Seconds = duration.TotalSeconds;
                            model.Weeks = string.Format("{0} Week(s) and {1} Day(s)", Math.Floor(model.Days / 7), (model.Days % 7));
                            model.LongDurations = DaysToMonthYearMaths(model.Days);
                            return View(model);
                        }
                        if (model.ResultRefineOption.Equals("includeonly") && model.Option.Equals("weekends"))
                        {
                            double daysCount = 0;
                            for (DateTime date = model.StartDate; date <= model.EndDate; date = date.AddDays(1))
                            {
                                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) daysCount += 1;
                            }
                            TimeSpan duration = TimeSpan.FromDays(daysCount);
                            model.Days = duration.TotalDays;
                            model.Hours = duration.TotalHours;
                            model.Minutes = duration.TotalMinutes;
                            model.Seconds = duration.TotalSeconds;
                            model.Weeks = string.Format("{0} Week(s) and {1} Day(s)", Math.Floor(model.Days / 7), (model.Days % 7));
                            model.LongDurations = DaysToMonthYearMaths(model.Days);
                            return View(model);
                        }
                        ModelState.AddModelError("", "You have selected wrong options.");
                        if (!ModelState.IsValid) return View(new BussinessViewModel() { HasResult = false });
                    }
                }
                return View(new BussinessViewModel() { HasResult = false });
            }
            return View(new BussinessViewModel() { HasResult = false });
        }

        /// <summary>
        /// Convert Days into long string.
        /// </summary>
        /// <param name="dor">EndDate</param>
        /// <param name="doj">StartDate</param>
        /// <param name="includeEndDate"></param>
        /// <returns></returns>
        [NonAction]
        public string DaysToMonthsYear(DateTime dor, DateTime doj, bool includeEndDate = false)
        {
            //Ref- http://stackoverflow.com/questions/30612880/convert-number-of-days-into-years-months-days
            var totalmonths = (dor.Year - doj.Year) * 12 + dor.Month - doj.Month;
            totalmonths += dor.Day < doj.Day ? -1 : 0;

            var durationYears = totalmonths / 12;
            var durationMonths = totalmonths % 12;
            var durationDays = dor.Subtract(doj.AddMonths(totalmonths)).Days;

            if (includeEndDate) durationDays += 1;

            StringBuilder durationString = new StringBuilder();

            if (durationDays > 0) durationString.Append(string.Format(", {0} day(s)", durationDays));
            if (durationMonths > 0) durationString.Insert(0, string.Format(", {0} month(s)", durationMonths));
            if (durationYears > 0) durationString.Insert(0, string.Format("{0} year(s)", durationYears));

            return durationString.ToString();
        }

        [NonAction]
        public string DaysToMonthYearMaths(double totalDays)
        {
            //calculate number of month in total days
            var durationYears = (int)Math.Truncate(totalDays / 365.24);
            var durationMonths = (int)Math.Truncate((totalDays % 365.24) / 30.44);
            var durationDays = (int)Math.Truncate((totalDays % 365.24) % 30.44);
            
            StringBuilder durationString = new StringBuilder();

            if (durationDays > 0) durationString.Append(string.Format(", {0} day(s)", durationDays));
            if (durationMonths > 0) durationString.Insert(0, string.Format(", {0} month(s)", durationMonths));
            if (durationYears > 0) durationString.Insert(0, string.Format("{0} year(s)", durationYears));

            return durationString.ToString();
        }
    }
}