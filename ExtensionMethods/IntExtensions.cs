using System.Linq;

public static class IntExtensions
{
	public static string MKL(this System.Int64 value)
	{
		return System.Convert.ToUInt32(value & 0xFFFFFFFF).MKL();
	}

	public static string MKLN(this System.Int64 value)
	{
		return System.Convert.ToUInt32(value & 0xffffffff).MKLN();
	}

	public static string MKL(this System.UInt32 value)
	{
		byte[] bytes = System.BitConverter.GetBytes(value);
		if ((!System.BitConverter.IsLittleEndian)) bytes.Reverse();
		return System.Text.Encoding.Default.GetString(bytes);
	}

	public static string MKLN(this System.UInt32 value)
	{
		byte[] bytes = System.BitConverter.GetBytes(value);
		if ((System.BitConverter.IsLittleEndian))
			bytes.Reverse();
		return System.Text.Encoding.Default.GetString(bytes);
	}

	public static string MKI(this System.Int32 value)
	{
		return System.Convert.ToUInt16(value & 0xffff).MKI();
	}

	public static string MKIN(this System.Int32 value)
	{
		return System.Convert.ToUInt16(value & 0xffff).MKIN();
	}

	public static string MKI(this System.UInt16 value)
	{
		byte[] bytes = System.BitConverter.GetBytes(value);
		if ((!System.BitConverter.IsLittleEndian)) System.Array.Reverse(bytes);
		return System.Text.Encoding.Default.GetString(bytes);
	}

	public static string MKIN(this System.UInt16 value)
	{
		byte[] bytes = System.BitConverter.GetBytes(value);
		if ((System.BitConverter.IsLittleEndian)) System.Array.Reverse(bytes);
		return System.Text.Encoding.Default.GetString(bytes);
	}

	public static string MKB(this byte value)
	{
		return System.Text.Encoding.Default.GetString(new byte[] { value });
	}

	public static System.DateTime FromUnixDate(this System.Int64 value)
	{
		System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
		dtDateTime = dtDateTime.AddSeconds(value).ToLocalTime();
		return dtDateTime;
	}

	public static System.DateTime FromUnixDate(this System.Int32 value)
	{
		return ((System.Int64)value).FromUnixDate();
	}
}