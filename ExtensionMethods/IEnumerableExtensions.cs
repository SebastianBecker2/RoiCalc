using System;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtensions
{
	public static void Dispose(this IEnumerable<IDisposable> collection)
	{
		foreach (IDisposable item in collection)
			if (item != null)
				item.Dispose();
	}

	public static int FindIndex<T>(this IEnumerable<T> source, IEnumerable<T> heystack)
	{
		if (source.Count() < heystack.Count())
		{
			return -1;
		}

		var last_index = source.Count() - heystack.Count();
		for (int i = 0; i < (last_index + 1); i++)
		{
			if (heystack.SequenceEqual(source.Skip(i).Take(heystack.Count())))
			{
				return i;
			}
		}

		return -1;
	}

	public static IEnumerable<TResult> Merge<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first,
			IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> operation)
	{
		using (var iter1 = first.GetEnumerator())
		using (var iter2 = second.GetEnumerator())
		{
			while (iter1.MoveNext())
			{
				if (iter2.MoveNext())
				{
					yield return operation(iter1.Current, iter2.Current);
				}
				else
				{
					yield return operation(iter1.Current, default(TSecond));
				}
			}
			while (iter2.MoveNext())
			{
				yield return operation(default(TFirst), iter2.Current);
			}
		}
	}
}