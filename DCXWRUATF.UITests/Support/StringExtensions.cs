using System;

namespace DCXWRUATF.UITests.Support
{
    public static class StringExtensions
	{
		/// <summary>
		/// Returns substring after *from* word.
		/// <code>
		/// usage:
		/// string line = "My name is Billy";
		/// string result = line.SubString("is "); // "Billy"
		/// </code>
		/// </summary>
		public static string SubString(this string line, string from)
		{
			var start = line.Contains(from) ? (line.IndexOf(from, StringComparison.Ordinal) + from.Length) : line.Length;

			return line.Substring(start);
		}
	}
}
