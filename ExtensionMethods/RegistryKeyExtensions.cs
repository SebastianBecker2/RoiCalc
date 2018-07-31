using System.Collections.Generic;
using Microsoft.Win32;

public static class RegistryKeyExtensions
{
	public static RegistryKey TryOpenSubKey(this RegistryKey base_key, string name)
	{
		try
		{
			return base_key.OpenSubKey(name);
		}
		catch
		{
			return null;
		}
	}

	public static List<RegistryKey> FindSubKeys(this RegistryKey location, string name)
	{
		List<RegistryKey> result = new List<RegistryKey>();
		if (location == null)
		{
			return result;
		}

		foreach (var s in location.GetSubKeyNames())
		{
			if (s.Equals(name))
			{
				RegistryKey new_result_value = location.TryOpenSubKey(s);
				if (new_result_value != null)
				{
					result.Add(new_result_value);
				}
			}

			result.AddRange(location.TryOpenSubKey(s).FindSubKeys(name));
		}

		return result;
	}
}