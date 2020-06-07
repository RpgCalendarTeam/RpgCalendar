namespace RPGCalendar.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.GameCalendar;
    using Dto;
    using Calendar = Data.GameCalendar.Calendar;
    using Month = Data.GameCalendar.Month;

    public class CalendarMapperProfiles : Profile
    {
        public CalendarMapperProfiles()
        {
            CreateMap<Calendar, Dto.Calendar>().ForMember(des => des.CurrentTimeInSeconds,
                    opt => opt.MapFrom(
                        src => src.CurrentTime))
                .ForMember(des => des.FormattedDate,
                    opt => opt.MapFrom(
                        src => src.GetFormattedDateString()))
                .ForMember(des => des.FullYear,
                    opt => opt.MapFrom(
                        src => src.GetFullYear()))
                .ForMember(des => des.Month,
                    opt => opt.MapFrom(
                        src => src.GetMonth()))
                .ForMember(des => des.Date,
                    opt => opt.MapFrom(
                        src => src.GetDate()))
                .ForMember(des => des.Hour,
                    opt => opt.MapFrom(
                        src => src.GetHour()))
                .ForMember(des => des.Minutes,
                    opt => opt.MapFrom(
                        src => src.GetMinutes()))
                .ForMember(des => des.Second,
                    opt => opt.MapFrom(
                        src => src.GetSeconds()))
                .ForMember(des => des.Day,
                    opt => opt.MapFrom(
                        src => src.GetDay()))
                .ForMember(des => des.MonthName,
                    opt => opt.MapFrom(
                        src => src.GetMonthName()))
                .ForMember(des => des.YearLengthInMonths,
                    opt => opt.MapFrom(
                        src => src.Months.Count))
                .ForMember(des => des.MonthLengthInDays,
                    opt => opt.MapFrom(
                        src => src.GetDaysInMonth()))
                .ForMember(des => des.WeekLengthInDays,
                    opt => opt.MapFrom(
                        src => src.DaysOfWeek.Count))
                .ForMember(des => des.DayLengthInHours,
                    opt => opt.MapFrom(
                        src => src.GetDayLengthInHours()))
                .ForMember(des => des.MonthNames,
                    opt => opt.MapFrom(
                        src => GetMonthNames(src.Months)))
                .ForMember(des => des.DayNames,
                    opt => opt.MapFrom(
                        src => GetDayNames(src.DaysOfWeek)));



            CreateMap<CalendarInput, Calendar>().ForMember(des => des.CurrentTime,
                    opt => opt.Ignore())
                .ForMember(des => des.HourLength,
                    opt => opt.MapFrom(
                        src => src.HourLength))
                .ForMember(des => des.DayLength,
                    opt => opt.MapFrom(
                        src => src.HourLength))
                .ForMember(des => des.Months,
                    opt => opt.MapFrom(
                        src => BuildMonths(src.Months!)))
                .ForMember(des => des.DaysOfWeek,
                    opt => opt.MapFrom(
                        src => BuildDays(src.DaysOfWeek!)));

        }

        private static string[] GetMonthNames(IEnumerable<Month> months) =>
            months.Select(e => e.Name).ToArray();

        private static string[] GetDayNames(IEnumerable<Day> days) =>
            days.Select(e => e.Name).ToArray();
        private static IEnumerable<Month> BuildMonths(IEnumerable<Dto.Month> dtoMonths)
            => dtoMonths.Select((t, i) => new Month { Order = i, Name = t.Name!, LengthInDays = t.LengthInDays });

        private static IEnumerable<Day> BuildDays(IEnumerable<string> daysOfWeek)
            => daysOfWeek.Select((t, i) => new Day { Name = t, Order = i });
    }
}
