using System;
using CSharpFunctionalExtensions;

namespace DitzyExtensions {
	public static class EnumExtensions {
		public static T[] GetEnumValues<T>() where T : struct, Enum =>
#if NET7_0_OR_GREATER
			Enum.GetValuesAsUnderlyingType<T>() as T[] ?? Array.Empty<T>();
#else
			Enum.GetValues(typeof(T)) as T[] ?? Array.Empty<T>();
#endif

		public static T AsEnum<T>(this string enumName) where T : struct =>
			TryAsEnum<T>(enumName).TryGetValue(out T result)
				? result
				: throw new ArgumentException($"Enum '{enumName}' does not exist.");

		public static Result<T, string> TryAsEnum<T>(this string enumName) where T : struct =>
			enumName.TryAsEnum(out T result)
				? Result.Success<T, string>(result)
				: $"Enum '{enumName}' not found for type '{typeof(T).Name}'.";

		public static bool TryAsEnum<T>(this string enumName, out T result) where T : struct {
			var foundEnum = Enum.TryParse(enumName, true, out T parseResult);
			result = parseResult;
			return foundEnum;
		}
	}
}
