using System.Collections.Generic;
using System.Linq;
using DitzyExtensions.Functional;

namespace DitzyExtensions {
	public static class StringExtensions {
		public static string AsLower(this string str) =>
			str.ToLowerInvariant();

		public static string AsUpper(this string str) =>
			str.ToUpperInvariant();

#if NET6_0_OR_GREATER
		public static string Join(this IEnumerable<string> source, string? separator) =>
#else
		public static string Join(this IEnumerable<string> source, string separator) =>
#endif
			string.Join(separator, source);

#if NET6_0_OR_GREATER
		public static string Join(this IEnumerable<char> source, string? separator) =>
#else
		public static string Join(this IEnumerable<char> source, string separator) =>
#endif
			string.Join(separator, source);

		public static string Indent(this string str, int level, int indentSize = 4, string indentChar = " ") =>
			indentChar
				.Repeat(indentSize)
				.Join(null)
				.Repeat(level)
				.Join(null)
			+ str;

		public static string IndentLines(
			this string str,
			int level,
			int indentSize = 4,
			string indentChar = " ",
			string newlineChar = "\n"
		) =>
			str
				.Split(newlineChar.ToCharArray())
				.Select(line => line.Indent(level, indentSize, indentChar))
				.Join(newlineChar);
	}
}
