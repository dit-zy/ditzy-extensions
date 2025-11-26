using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpFunctionalExtensions;
#if !N48_S2
using System.Collections.Immutable;
#endif

namespace DitzyExtensions.Collection {
	public static class ListExtensions {
		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action) =>
			source.ForEach((value, _) => action.Invoke(value));

		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T, int> action) {
			var values = source as T[] ?? source.ToArray();
			for (var i = 0; i < values.Length; ++i) {
				action.Invoke(values[i], i);
			}
			return values;
		}

		public static IEnumerable<U> SelectWhere<T, U>(this IEnumerable<T> source, Func<T, (bool, U)> filteredSelector) =>
			source
				.Select(filteredSelector)
				.Where(result => result.Item1)
				.Select(result => result.Item2);

		public static IList<T> AsList<T>(this IEnumerable<T> source) =>
#if N48_S2
			source.ToList().AsReadOnly();
#else
			source.ToImmutableList();
#endif

		public static IList<T> AsMutableList<T>(this IEnumerable<T> source) =>
			source.ToList();

		public static ISet<T> AsMutableSet<T>(this IEnumerable<T> source) => new HashSet<T>(source);

		public static IList<T> AsSingletonList<T>(this T value) =>
			new[] { value }.AsList();

		/// <summary>
		/// Returns whether the <b>source</b> collection is empty.
		/// </summary>
		/// <param name="source">The source collection.</param>
		/// <typeparam name="T">The type of elements of <b>source</b></typeparam>
		/// <returns>Whether the <b>source</b> collection is empty.</returns>
		public static bool IsEmpty<T>(this ICollection<T> source) =>
			source.Count == 0;

		/// <summary>
		/// Returns whether the <b>source</b> collection is <b>not</b> empty.
		/// </summary>
		/// <param name="source">The source collection.</param>
		/// <typeparam name="T">The type of elements of <b>source</b>.</typeparam>
		/// <returns>Whether the <b>source</b> collection is <b>not</b> empty.</returns>
		public static bool IsNotEmpty<T>(this ICollection<T> source) =>
			0 < source.Count;

		/// <summary>
		/// Concatenate the enumerables in <b>source</b> into one single enumerable.
		/// </summary>
		/// <param name="source">The source of enumerables.</param>
		/// <typeparam name="T">The type of elements of <b>source</b>.</typeparam>
		/// <returns>A single enumerable of all the elements in the enumerables within <b>source</b>.</returns>
		/// <example>
		/// <code>
		/// [[1, 2], [3], [4, 5, 6]].Flatten(); // [1, 2, 3, 4, 5, 6]
		/// </code>
		/// </example>
		public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source) =>
			source.SelectMany(x => x);

		/// <summary>
		/// Return a subset of the <b>source</b> enumerable, only containing the first <b>numElements</b> elements.
		/// </summary>
		/// <param name="source">The source of elements.</param>
		/// <param name="numElements">The number of elements to return, starting at the beginning of <b>source</b>. If
		/// <c>numElements &lt; 0</c> then returns elements up to that many from the end of <b>source</b>. The default of
		/// <c>1</c> returns only the first element.</param>
		/// <typeparam name="T">The type of the elements of <b>source</b>.</typeparam>
		/// <returns>The first <b>numElements</b> elements from <b>source</b>.</returns>
		/// <example>
		/// <code>
		/// [1, 2, 3, 4].Head(); // [1]
		/// [1, 2, 3, 4].Head(2); // [1, 2]
		/// [1, 2, 3, 4].Head(0); // []
		/// [1, 2, 3, 4].Head(-1); // [1, 2, 3]
		/// [1, 2, 3, 4].Head(10); // [1, 2, 3, 4]
		/// [1, 2, 3, 4].Head(-10); // []
		/// </code>
		/// </example>
		public static IEnumerable<T> Head<T>(this IEnumerable<T> source, int numElements = 1) {
			if (numElements < 0) {
				var ls = source.AsList();
				numElements += ls.Count;
				source = ls;
			}
			var i = 0;
			foreach (var element in source) {
				if (numElements <= i) yield break;
				yield return element;
				i++;
			}
		}

		/// <summary>
		/// Return a subset of the <b>source</b> enumerable, only containing the last <b>numElements</b> elements.
		/// </summary>
		/// <param name="source">The source of elements.</param>
		/// <param name="numElements">The number of elements to return from the end of <b>source</b>. If <c>numElements
		/// &lt; 0</c> then skips that many elements and returns the rest (<c>source.Tail(-3) == source.Skip(3)</c>). The
		/// default of <c>-1</c> returns all elements except for the very first one.</param>
		/// <typeparam name="T">The type of the elements of <b>source</b>.</typeparam>
		/// <returns>The last <b>numElements</b> elements from <b>source</b>.</returns>
		/// <example>
		/// <code>
		/// [1, 2, 3, 4].Tail(); // [2, 3, 4];
		/// [1, 2, 3, 4].Tail(2); // [3, 4]
		/// [1, 2, 3, 4].Tail(0); // [];
		/// [1, 2, 3, 4].Tail(-1); // [2, 3, 4];
		/// [1, 2, 3, 4].Tail(10); // [1, 2, 3, 4];
		/// [1, 2, 3, 4].Tail(-10); // [];
		/// </code>
		/// </example>
		public static IEnumerable<T> Tail<T>(this IEnumerable<T> source, int numElements = -1) {
			var ls = source.AsList();
			var firstIndex = -numElements;
			if (firstIndex <= 0) {
				firstIndex += ls.Count;
			}
			for (var i = firstIndex; i < ls.Count; i++) {
				yield return ls[i];
			}
		}

#if N48_S2
		public static Maybe<T> MinBy<T, U>(
			this IEnumerable<T> source,
			Func<T, int, U> keySelector
		) =>
			source.MinBy(keySelector, null);

		public static Maybe<T> MinBy<T, U>(
			this IEnumerable<T> source,
			Func<T, int, U> keySelector,
			IComparer<U> comparer
		) {
			if (comparer is null) {
				comparer = Comparer<U>.Default;
			}
			
			T minEntry = default;
			U minKey = default;
			var started = false;

			source.ForEach(
				(t, i) => {
					var newKey = keySelector(t, i);

					if (!started) {
						minEntry = t;
						minKey = newKey;
						started = true;
						return;
					}

					if (comparer.Compare(newKey, minKey) < 0) {
						minEntry = t;
						minKey = newKey;
					}
				}
			);

			return started ? minEntry : Maybe<T>.None;
		}
#endif

		public static IEnumerable<T> With<T>(this IEnumerable<T> source, params (int, T)[] updateEntries) =>
			source.With((IEnumerable<(int, T)>)updateEntries);

		public static IEnumerable<T> With<T>(this IEnumerable<T> source, IEnumerable<(int, T)> updateEntries) =>
			source
				.Select((value, index) => (index, value))
				.Concat(updateEntries)
				.GroupBy(entry => entry.Item1)
				.Select(grouping => grouping.Last().Item2);

		public static IEnumerable<T> Sort<T>(this IEnumerable<T> source) {
			var list = new List<T>(source);
			list.Sort();
			return list.AsList();
		}

		public static IEnumerable<T> Sort<T>(this IEnumerable<T> source, IComparer<T> comparer) {
			var list = new List<T>(source);
			list.Sort(comparer);
			return list.AsList();
		}

		public static IEnumerable<T> Sort<T>(this IEnumerable<T> source, Func<T, T, int> comparer) {
			var list = new List<T>(source);
			list.Sort(new Comparison<T>(comparer));
			return list.AsList();
		}

		public static IEnumerable<IList<T>> Choose<T>(this IEnumerable<T> source, int numElementsToChoose) {
			var ls = source.AsList();
			var n = ls.Count;
			var bitmask = (1 << numElementsToChoose) - 1;
			while (bitmask < (1 << n)) {
				yield return GetChoice(bitmask, ls);
				var x = bitmask & -bitmask;
				var y = bitmask + x;
				var z = (bitmask & ~y);
				bitmask = z / x;
				bitmask >>= 1;
				bitmask |= y;
			}
		}

		private static IList<T> GetChoice<T>(int bitmask, IList<T> source) =>
			source.Where((t, i) => bitmask.IsBitSet(i)).AsList();

		public static IEnumerable<IList<T>> Permute<T>(this IEnumerable<T> source) {
			var ls = source.ToArray();
			var c = new int[ls.Length];

			yield return Copy(ls);

			var i = 1;
			var safetyLimit = ls.Length.Factorial() * 3;
			for (var safety = 0; i < ls.Length && safety < safetyLimit; safety++) {
				if (c[i] < i) {
					if (i % 2 == 0) {
						Swap(ls, 0, i);
					} else {
						Swap(ls, c[i], i);
					}
					yield return Copy(ls);
					c[i] += 1;
					i = 1;
				} else {
					c[i] = 0;
					i += 1;
				}
			}
		}

		private static void Swap<T>(T[] ls, int i, int j) {
			var n = ls.Length - 1;
			(ls[n - i], ls[n - j]) = (ls[n - j], ls[n - i]);
		}

		private static T[] Copy<T>(T[] ls) {
			var copy = new T[ls.Length];
			ls.CopyTo(copy, 0);
			return copy;
		}

		public static string AsString<T>(this IEnumerable<T> source) =>
			new StringBuilder()
				.Append('[')
				.Append(source.Select(element => element?.ToString() ?? "null").Join(", "))
				.Append(']')
				.ToString();
	}
}
