using System;

namespace DCXWRUATF.UITests.Support
{
    public static class DateTimeExtensions
	{
		/// <summary>
		/// Return true if *seconds* seconds passed from *startTime*.
		/// <code>
		/// usage:
		/// DateTime now = DateTime.Now;
		/// Thread.Sleep(10000); // sleep 10 seconds;
		/// bool passed10 = now.HasTimedOut(5); // true
		/// bool passed60 = now.HasTimedOut(); // false
		/// </code>
		/// </summary>
		public static bool HasTimedOut(this DateTime startTime, int timeOutInSeconds = 60)
		{
			return (DateTime.Now.Subtract(startTime).TotalSeconds > timeOutInSeconds);
		}
	}
}
