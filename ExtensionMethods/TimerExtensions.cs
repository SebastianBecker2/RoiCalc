public static class TimerExtensions
{
	public static void StartSingle(this System.Threading.Timer timer, int due_time)
	{
		timer.Change(due_time, System.Threading.Timeout.Infinite);
	}

	public static void StartRecurring(this System.Threading.Timer timer, int interval)
	{
		timer.Change(interval, interval);
	}

	public static void Stop(this System.Threading.Timer timer)
	{
		timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
	}
}