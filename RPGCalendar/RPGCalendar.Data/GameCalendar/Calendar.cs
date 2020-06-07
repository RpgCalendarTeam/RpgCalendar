namespace RPGCalendar.Data.GameCalendar
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Calendar : FingerPrintEntityBase
    {
        private long _dayLength = 1;
        private long _hourLength = 1;
        private long _currentTime;
        private List<Month> _months = new List<Month>();
        private List<Day> _daysOfWeek = new List<Day>();

        public int GameId { get; set; }
        public Game? Game { get; set; }
        public long CurrentTime
        {
            get => _currentTime;
            set => _currentTime = value >= 0 ? value : throw new ArgumentException(nameof(CurrentTime));
        }

        public long HourLength
        {
            get => _hourLength;
            set => _hourLength = value > 0 ? value : throw new ArgumentException(nameof(HourLength));
        }

        public long DayLength
        {
            get => _dayLength;
            set => _dayLength = value > 0 ? value : throw new ArgumentException(nameof(DayLength));
        }

        public List<Month> Months
        {
            get
            {
                _months.Sort();
                return _months;
            }
            set => _months = value ?? throw new ArgumentNullException(nameof(Months));
        }

        public List<Day> DaysOfWeek
        {
            get
            {
                _daysOfWeek.Sort();
                return _daysOfWeek;
            }
            set => _daysOfWeek = value ?? throw new ArgumentNullException(nameof(Months));
        }

        public void addSeconds(long seconds)
        {
            if(seconds + CurrentTime < 0)
                throw new ArgumentOutOfRangeException(nameof(seconds), "Cannot go back before epoch time");
            this.CurrentTime += seconds;
        }
        public void AddMins(int min) => this.addSeconds(min * 60);

        public void AddDays(int days) => this.addSeconds(days * DayLength);

        public void AddHours(int hours) => this.addSeconds(hours * HourLength);

        public void AddYears(int years) => this.addSeconds(years * GetYearLength());



        public int GetSeconds()
        {
            return (int)(CurrentTime % 60);
        }

        public int GetMinutes()
        {
            return (int)(CurrentTime % HourLength / 60);
        }
        public int GetHour()
        {
            return (int)(CurrentTime % DayLength / HourLength);
        }


        public int GetDate()
        {
            var days = (int)((CurrentTime % GetYearLength()) / DayLength);
            foreach (var month in Months)
            {
                if (days < month.LengthInDays)
                {
                    return days;
                }

                days -= month.LengthInDays;
            }

            throw new ArgumentOutOfRangeException(nameof(days));
        }


        public int GetMonth()
        {
            var days = (int)((CurrentTime % GetYearLength())/DayLength);
            for (int i = 0; i < Months.Count; i++)
            {
                if (days < Months[i].LengthInDays)
                {
                    return i;
                }

                days -= Months[i].LengthInDays;
            }

            throw new ArgumentOutOfRangeException(nameof(days));
        }

        public int GetDaysInMonth() => Months[GetMonth()].LengthInDays;
        public int GetFullYear() => (int)(CurrentTime / GetYearLength())%Int32.MaxValue;

        public string GetMonthName()
        {
            var month = GetMonth();
            return Months[month].Name;
        }

        public string GetDay()
        {
            var dayOfWeek = (int)(CurrentTime/DayLength % DaysOfWeek.Count);
            return DaysOfWeek[dayOfWeek].Name;
        }

        public string GetFormattedDateString()
        {
            return $"{GetDay()}, {GetMonthName()} {GetDate()}, {GetFullYear()}";
        }
        
        private long GetYearLength()
        {
            var days = Months.Sum(month => month.LengthInDays);
            return DayLength * days;
        }

        public int GetDayLengthInHours() => (int)(DayLength / HourLength % int.MaxValue);

    }

    public class Day : FingerPrintEntityBase, IComparable<Day>
    {
        private string _name = string.Empty;

        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(Name));
        }

        public int Order { get; set; } = 0;
        public int CompareTo(Day? other)
        {
            if (other is null)
                return 1;
            return this.Order - other!.Order;
        }
    }
    public class Month : FingerPrintEntityBase, IComparable<Month>
    {
        private string _name = string.Empty;
        private int _lengthInDays = 1;
        public int Order { get; set; } = 0;
        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(Name));
        }

        public int LengthInDays
        {
            get => _lengthInDays;
            set => _lengthInDays = value > 0 ? value : throw new ArgumentException(nameof(LengthInDays));
        }

        public int CompareTo(Month? other)
        {
            if (other is null)
                return 1;
            return this.Order - other!.Order;
        }
    }
}
