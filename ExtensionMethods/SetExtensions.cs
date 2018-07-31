using System.Collections.Generic;

public static class SetExtensions
{
	public static void RemoveRange<T>(this ISet<T> set, IEnumerable<T> values)
	{
		foreach (T element in values)
		{
			set.Remove(element);
		}
	}

	public static void AddRange<T>(this ISet<T> set, IEnumerable<T> values)
	{
		foreach (T element in values)
		{
			set.Add(element);
		}
	}
}