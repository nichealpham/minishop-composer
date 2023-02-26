using AppGlobal.Commons;
using AppGlobal.Constants;
using System;
using System.Collections.Generic;

namespace AppGlobal.Extensions
{
    public static class DateExtension
    {
        public static DateTime StartOfQuarter(this DateTime date)
        {
            int quarterNumber = (date.Month - 1) / 3 + 1;
            return new DateTime(date.Year, (quarterNumber - 1) * 3 + 1, 1);
        }
        public static DateTime EndOfQuarter(this DateTime date)
        {
            return date.StartOfQuarter().AddMonths(3).AddDays(-1);
        }
        public static DateTime StartOfMonth(this DateTime date)
            => new DateTime(date.Year, date.Month, 1);
        public static DateTime EndOfMonth(this DateTime date)
            => date.StartOfMonth().AddMonths(1).AddDays(-1);
        public static DateTime StartOfDay(this DateTime theDate)
            => theDate.Date;
        public static DateTime EndOfDay(this DateTime theDate)
            => theDate.Date.AddDays(1).AddTicks(-1);
        public static int AsAge(this DateTime birthday)
            => DateTime.UtcNow.Year - birthday.Year;
        public static int MinutesUntilNow(this DateTime time)
            => (int)DateTime.Now.Subtract(time).TotalMinutes;
        public static int MinutesUntilNow(this DateTime? time)
            => time?.MinutesUntilNow() ?? 0;

        // Date events convenience methods

        public static bool InRange(this DateTime value, DateTime first, DateTime? second)
            => Helper.InRange(value, first, second ?? DateTime.MaxValue);
        public static DateTime UpdateDate(this DateTime time, DateTime date)
            => new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
        public static DateTime Add(this DateTime date, RecurrenceType type, double count = 1.0)
            => type switch
            {
                RecurrenceType.Daily => date.AddDays(count),
                RecurrenceType.Weekly => date.AddWeeks(count),
                RecurrenceType.Monthly => date.AddMonths((int)count),
                RecurrenceType.Yearly => date.AddYears((int)count),
                _ => throw new ApplicationException("Unsupported period type"),
            };
        public static DateTime AddWeeks(this DateTime date, double weeks)
            => date.AddDays(7 * weeks);
        public static bool IsLeapYear(this DateTime date)
            => DateTime.IsLeapYear(date.Year);
        public static bool IsWeekend(this DateTime time)
            => time.DayOfWeek == DayOfWeek.Saturday || time.DayOfWeek == DayOfWeek.Sunday;
        public static DateTime NextBusinessDay(this DateTime time)  // If weekend return Monday, else return next day
            => time.DayOfWeek == DayOfWeek.Friday ?
                time.AddDays(3) :
                time.DayOfWeek == DayOfWeek.Saturday ?
                    time.AddDays(2) :
                    time.AddDays(1);
        public static IEnumerable<DateTime> RecurringUntil(
            this DateTime start,
            DateTime end,
            int period,
            bool workdaysOnly = false)
        {
            for (var date = start; date < end; date = date.AddDays(period))
                yield return workdaysOnly && date.IsWeekend() ?
                    date.NextBusinessDay() :
                    date;
        }
        public static IEnumerable<DateTime> RecurringUntil(
            this DateTime start,
            DateTime end,
            RecurrenceType type,
            bool workdaysOnly = false)
        {
            for (var date = start; date < end; date = date.Add(type))
                yield return workdaysOnly && date.IsWeekend() ?
                    date.NextBusinessDay() :
                    date;
        }
        public static IEnumerable<DateTime> Recurring(
            this DateTime start,
            Func<DateTime, bool> predicate,
            Func<DateTime, DateTime> incrementor,
            Func<DateTime, DateTime> action)
        {
            for (var date = start; predicate.Invoke(date); date = incrementor.Invoke(date))
                yield return action.Invoke(date);
        }
    }
}
