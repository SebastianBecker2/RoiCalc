namespace System.Threading
{
	public static class InterlockedExtensions
	{
		public static bool ExchangeIfUnequal(ref int location, int value, int comparand)
		{
			int initialValue;
			do
			{
				initialValue = location;
				if (initialValue == comparand) return false;
			} while (System.Threading.Interlocked.CompareExchange(ref location, value, initialValue) != initialValue);
			return true;
		}

		public static bool ExchangeIfGreater(ref int location, int value, int comparand)
		{
			int initialValue;
			do
			{
				initialValue = location;
				if (initialValue <= comparand) return false;
			} while (System.Threading.Interlocked.CompareExchange(ref location, value, initialValue) != initialValue);
			return true;
		}

		public static bool ExchangeIfSmaller(ref int location, int value, int comparand)
		{
			int initialValue;
			do
			{
				initialValue = location;
				if (initialValue >= comparand) return false;
			} while (System.Threading.Interlocked.CompareExchange(ref location, value, initialValue) != initialValue);
			return true;
		}

		public static bool IncrementIfGreater(ref int location, int comparand)
		{
			int initialValue;
			int newValue;
			do
			{
				initialValue = location;
				if (initialValue <= comparand) return false;
				newValue = initialValue + 1;
			} while (System.Threading.Interlocked.CompareExchange(ref location, newValue, initialValue) != initialValue);
			return true;
		}

		public static bool IncrementIfSmaller(ref int location, int comparand)
		{
			int initialValue;
			int newValue;
			do
			{
				initialValue = location;
				if (initialValue >= comparand) return false;
				newValue = initialValue + 1;
			} while (System.Threading.Interlocked.CompareExchange(ref location, newValue, initialValue) != initialValue);
			return true;
		}

		public static bool DecrementIfGreater(ref int location, int comparand)
		{
			int initialValue;
			int newValue;
			do
			{
				initialValue = location;
				if (initialValue <= comparand) return false;
				newValue = initialValue - 1;
			} while (System.Threading.Interlocked.CompareExchange(ref location, newValue, initialValue) != initialValue);
			return true;
		}

		public static bool DecrementIfSmaller(ref int location, int comparand)
		{
			int initialValue;
			int newValue;
			do
			{
				initialValue = location;
				if (initialValue >= comparand) return false;
				newValue = initialValue - 1;
			} while (System.Threading.Interlocked.CompareExchange(ref location, newValue, initialValue) != initialValue);
			return true;
		}
	}
}