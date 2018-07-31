using System;
using System.Linq;

public static class StringExtensions
{
	public static int SoftParse(this string value, int fallback)
	{
		int result;
		if (int.TryParse(value, out result))
		{
			return result;
		}
		return fallback;
	}

	public static uint SoftParse(this string value, uint fallback)
	{
		uint result;
		if (uint.TryParse(value, out result))
		{
			return result;
		}
		return fallback;
	}

	public static bool SoftParse(this string value, bool fallback)
	{
		bool result;
		if (bool.TryParse(value, out result))
		{
			return result;
		}
		return fallback;
	}

	public static string RemoveLastCharacter(this string value)
	{
		if (value.Length <= 0) return "";
		return value.Remove(value.Length - 1);
	}

	public static System.UInt32 CVL(this string value)
	{
		System.Byte[] bytes = System.Text.Encoding.Default.GetBytes(value.Substring(0, 4));
		if (System.BitConverter.IsLittleEndian) bytes.Reverse();
		return System.BitConverter.ToUInt32(bytes, 0);
	}

	public static System.UInt32 CVL(this string value, int index)
	{
		return value.Substring(index).CVL();
	}

	public static System.UInt32 CVLN(this string value)
	{
		System.Byte[] bytes = System.Text.Encoding.Default.GetBytes(value.Substring(0, 4));
		if (!System.BitConverter.IsLittleEndian) bytes.Reverse();
		return System.BitConverter.ToUInt32(bytes, 0);
	}

	public static System.UInt32 CVLN(this string value, int index)
	{
		return value.Substring(index).CVLN();
	}

	public static System.UInt16 CVI(this string value)
	{
		System.Byte[] bytes = System.Text.Encoding.Default.GetBytes(value.Substring(0, 2));
		if (System.BitConverter.IsLittleEndian) bytes.Reverse();
		return System.BitConverter.ToUInt16(bytes, 0);
	}

	public static System.UInt16 CVI(this string value, int index)
	{
		return value.Substring(index).CVI();
	}

	public static System.UInt16 CVIN(this string value)
	{
		System.Byte[] bytes = System.Text.Encoding.Default.GetBytes(value.Substring(0, 2));
		if (!System.BitConverter.IsLittleEndian) bytes.Reverse();
		return System.BitConverter.ToUInt16(bytes, 0);
	}

	public static System.UInt16 CVIN(this string value, int index)
	{
		return value.Substring(index).CVIN();
	}

	public static byte CVB(this string value)
	{
		var bytes = System.Text.Encoding.Default.GetBytes(value.Substring(0, 1));
		return bytes[0];
	}

	public static byte CVB(this string value, int index)
	{
		return value.Substring(index, 1).CVB();
	}

	public static string ToHex(this string value)
	{
		byte[] bytes = System.Text.Encoding.Default.GetBytes(value);
		value = System.BitConverter.ToString(bytes);
		return value.Replace("-", "");
	}

	public static string FromHex(this string value)
	{
		byte[] bytes = new byte[value.Length / 2];
		for (int i = 0; i < bytes.Length; i++)
		{
			bytes[i] = System.Convert.ToByte(value.Substring((i * 2), 2), 16);
		}
		return System.Text.Encoding.Default.GetString(bytes);
	}

	public static bool TryParse(this string value, out int result)
	{
		return int.TryParse(value, out result);
	}

	public static bool TryParse(this string value, out uint result)
	{
		return uint.TryParse(value, out result);
	}

	public static bool TryParse(this string value, System.Globalization.NumberStyles style, out int result)
	{
		return value.TryParse(style, System.Threading.Thread.CurrentThread.CurrentCulture, out result);
	}

	public static bool TryParse(this string value, System.Globalization.NumberStyles style, System.IFormatProvider provider, out int result)
	{
		return int.TryParse(value, style, provider, out result);
	}

	public static bool TryParseCurrency(this string value, out int result)
	{
		decimal decimal_result;
		bool return_value = value.TryParse(System.Globalization.NumberStyles.Currency, out decimal_result);
		if (return_value)
		{
			if (decimal_result > (System.Int32.MaxValue / 100))
			{
				return_value = false;
			}
			if (decimal_result < (System.Int32.MinValue / 100))
			{
				return_value = false;
			}
		}
		if (return_value)
		{
			result = decimal.ToInt32(decimal_result * 100);
		}
		else
		{
			result = 0;
		}
		return return_value;
	}

	public static bool TryParseCurrency(this string value, out uint result)
	{
		decimal decimal_result;
		bool return_value = value.TryParse(System.Globalization.NumberStyles.Currency, out decimal_result);
		if (return_value)
		{
			if (decimal_result > (System.UInt32.MaxValue / 100))
			{
				return_value = false;
			}
			if (decimal_result < (System.UInt32.MinValue))
			{
				return_value = false;
			}
		}
		if (return_value)
		{
			result = decimal.ToUInt32(decimal_result * 100);
		}
		else
		{
			result = 0;
		}
		return return_value;
	}

	public static bool TryParseCurrency(this string value, out long result)
	{
		decimal decimal_result;
		bool return_value = value.TryParse(System.Globalization.NumberStyles.Currency, out decimal_result);
		if (return_value)
		{
			if (decimal_result > (long.MaxValue / 100))
			{
				return_value = false;
			}
			if (decimal_result < (long.MinValue / 100))
			{
				return_value = false;
			}
		}
		if (return_value)
		{
			result = decimal.ToInt64(decimal_result * 100);
		}
		else
		{
			result = 0;
		}
		return return_value;
	}

	public static bool TryParse(this string value, out decimal result)
	{
		return decimal.TryParse(value, out result);
	}

	public static bool TryParse(this string value, System.Globalization.NumberStyles style, out decimal result)
	{
		return value.TryParse(style, System.Threading.Thread.CurrentThread.CurrentCulture, out result);
	}

	public static bool TryParse(this string value, System.Globalization.NumberStyles style, System.IFormatProvider provider, out decimal result)
	{
		return decimal.TryParse(value, style, provider, out result);
	}

	public static System.Drawing.Image FromBase64ToImage(this string data)
	{
		byte[] file_bytes = System.Convert.FromBase64String(data);
		System.IO.MemoryStream memory_stream = new System.IO.MemoryStream(file_bytes);
		System.Drawing.Image image = System.Drawing.Image.FromStream(memory_stream, true);
		return image;
	}

	public static DateTime FromTeratronikDate(this string value)
	{
		return value.FromTeratronikDate(0);
	}

	public static DateTime FromTeratronikDate(this string value, int offset)
	{
		DateTime date = new DateTime(1990, 1, 1, 0, 0, 0);
		date = date.AddDays(value.CVI(offset + 0));
		date = date.AddMinutes(value.CVI(offset + 2));
		return date;
	}

	public static string Last(this string value, int count)
	{
		if (value.Length < count)
		{
			return value;
		}
		return value.Substring(value.Length - count);
	}
}