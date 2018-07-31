using System;
using System.Data.SqlTypes;

public static class DateTimeExtensions
{
	public static DateTime FirstDayOfMonth(this DateTime date)
	{
		return date.AddDays((date.Day - 1) * -1);
	}

	public static DateTime LastDayOfMonth(this DateTime date)
	{
		date = date.AddMonths(1);
		return date.AddDays(date.Day * -1);
	}

	public static DateTime NextWeekday(this DateTime date, DayOfWeek day)
	{
		int days_to_add = ((int)day - (int)date.DayOfWeek + 7) % 7;
		return date.AddDays(days_to_add);
	}

	public static DateTime LastWeekday(this DateTime date, DayOfWeek day)
	{
		int days_to_subtract = ((int)date.DayOfWeek - (int)day + 7) % 7;
		return date.AddDays(days_to_subtract * -1);
	}

	public static DateTime EndOfDay(this DateTime date)
	{
		return date.AddDays(1).AddSeconds(-1);
	}

	public static string ToTeratronikDate(this DateTime date)
	{
		string data = (date.Subtract(new DateTime(1990, 1, 1, 0, 0, 0)).Days & 0xFFFF).MKI();
		data += ((int)(Math.Floor(date.TimeOfDay.TotalMinutes) % 0xFFFF)).MKI();
		return data;
	}

	public static Int64 ToUnixDate(this DateTime date)
	{
		DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (Int64)Math.Floor((date.ToUniversalTime() - Epoch).TotalSeconds);
	}

	public static DateTime Clamp(this DateTime date, DateTime min, DateTime max)
	{
		if (date <= min)
		{
			return min;
		}
		else if (date >= max)
		{
			return max;
		}
		return date;
	}

	public static DateTime ClampSqlDateTime(this DateTime date)
	{
		return date.Clamp((DateTime)SqlDateTime.MinValue, (DateTime)SqlDateTime.MaxValue);
	}
}