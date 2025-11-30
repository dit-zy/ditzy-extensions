/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using DitzyExtensions.Collection;

namespace DitzyExtensions.Functional {
	public static class PureExtensions {
		public static bool NotEquals<T>(this T a, T b) => !object.Equals(a, b);
		
		public static ACC Reduce<T, ACC>(this IEnumerable<T> source, Func<ACC, T, ACC> reducer, ACC initial) =>
			source.Reduce((acc, value, _) => reducer(acc, value), initial);

		public static ACC Reduce<T, ACC>(this IEnumerable<T> source, Func<ACC, T, int, ACC> reducer, ACC initial) {
			var acc = initial;
			source.ForEach((value, i) => { acc = reducer.Invoke(acc, value, i); });
			return acc;
		}

		public static IEnumerable<T> Repeat<T>(this T item) => item.Repeat(-1);

		public static IEnumerable<T> Repeat<T>(this T item, int count) {
			for (var i = 0; i < count || count < 0; i++) {
				yield return item;
			}
		}

		public static IEnumerable<T> Iterate<T>(this T initialValue, Func<T, T> iterFunc) => initialValue.Iterate(iterFunc, -1);

		public static IEnumerable<T> Iterate<T>(this T initialValue, Func<T, T> iterFunc, int count) {
			if (count == 0) yield break;
			
			var value = initialValue;
			yield return value;
			for (var i = 1; i < count || count < 0; i++) {
				value = iterFunc(value);
				yield return value;
			}
		}
		
		public static IEnumerable<T> Iterate<T>(this Func<T, T> iterFunc, T initialValue) => iterFunc.Iterate(initialValue, -1);
		
		public static IEnumerable<T> Iterate<T>(this Func<T, T> iterFunc, T initialValue, int count) => initialValue.Iterate(iterFunc, count);

#if !N48_S2
		public static IEnumerable<int> Sequence(this Range range) {
			for (var i = range.Start.Value; i < range.End.Value; i++) {
				yield return i;
			}
		}
#endif

		public static IEnumerable<int> SequenceTo(this int startInclusive, int endExclusive, int step = 1) {
			if (step == 0) throw new ArgumentException("step cannot be 0.");
			if (Math.Sign(endExclusive - startInclusive) != Math.Sign(step))
				throw new ArgumentException(
					$"end [{endExclusive}] is not reachable from start [{startInclusive}] with step [{step}]."
				);

			for (int i = startInclusive; (0 < step && i < endExclusive) || (step < 0 && endExclusive < i); i += step) {
				yield return i;
			}
		}
		
		public static U Let<T, U>(this T item, Func<T, U> transform) => transform(item);
		
		public static T Run<T>(this T item, Action<T> action) {
			action(item);
			return item;
		}

		public static Func<Nothing> AsFunc(this Action action) =>
			() => {
				action();
				return Nothing.Value;
			};

		public struct Nothing {
			public static readonly Nothing Value = new Nothing();
		}
	}
}
