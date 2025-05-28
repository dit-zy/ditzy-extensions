using System;
using System.Collections.Generic;
using System.Linq;
using DitzyExtensions.Functional;

namespace DitzyExtensions.Collection {
	public static class TupleExtensions {
		public static IEnumerable<T> SelectFirst<T, U>(this IEnumerable<(T t, U u)> source) =>
			source.Select(tuple => tuple.t);

		public static IEnumerable<T> SelectFirst<T, U, V>(this IEnumerable<(T t, U u, V v)> source) =>
			source.Select(tuple => tuple.t);

		public static IEnumerable<T> SelectFirst<T, U, V, W>(this IEnumerable<(T t, U u, V v, W w)> source) =>
			source.Select(tuple => tuple.t);

		public static IEnumerable<U> SelectSecond<T, U>(this IEnumerable<(T t, U u)> source) =>
			source.Select(tuple => tuple.u);

		public static IEnumerable<U> SelectSecond<T, U, V>(this IEnumerable<(T t, U u, V v)> source) =>
			source.Select(tuple => tuple.u);

		public static IEnumerable<U> SelectSecond<T, U, V, W>(this IEnumerable<(T t, U u, V v, W w)> source) =>
			source.Select(tuple => tuple.u);

		public static IEnumerable<V> SelectThird<T, U, V>(this IEnumerable<(T t, U u, V v)> source) =>
			source.Select(tuple => tuple.v);

		public static IEnumerable<V> SelectThird<T, U, V, W>(this IEnumerable<(T t, U u, V v, W w)> source) =>
			source.Select(tuple => tuple.v);

		public static IEnumerable<W> SelectFourth<T, U, V, W>(this IEnumerable<(T t, U u, V v, W w)> source) =>
			source.Select(tuple => tuple.w);

		public static IEnumerable<R> SelectFirst<T, U, R>(this IEnumerable<(T t, U u)> source, Func<T, R> transform) =>
			source.Select(tuple => transform(tuple.t));

		public static IEnumerable<R> SelectFirst<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<T, R> transform
		) =>
			source.Select(tuple => transform(tuple.t));

		public static IEnumerable<R> SelectFirst<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<T, R> transform
		) =>
			source.Select(tuple => transform(tuple.t));

		public static IEnumerable<R> SelectSecond<T, U, R>(this IEnumerable<(T t, U u)> source, Func<U, R> transform) =>
			source.Select(tuple => transform(tuple.u));

		public static IEnumerable<R> SelectSecond<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<U, R> transform
		) =>
			source.Select(tuple => transform(tuple.u));

		public static IEnumerable<R> SelectSecond<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<U, R> transform
		) =>
			source.Select(tuple => transform(tuple.u));

		public static IEnumerable<R> SelectThird<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<V, R> transform
		) =>
			source.Select(tuple => transform(tuple.v));

		public static IEnumerable<R> SelectThird<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<V, R> transform
		) =>
			source.Select(tuple => transform(tuple.v));

		public static IEnumerable<R> SelectFourth<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<W, R> transform
		) =>
			source.Select(tuple => transform(tuple.w));

		public static IEnumerable<(K key, V value)> AsPairs<K, V>(this IDictionary<K, V> source) =>
			source.Select(entry => (entry.Key, entry.Value));

		public static IEnumerable<(A, B)> WithDistinctFirst<A, B>(this IEnumerable<(A, B)> source)
#if N48_S2
			=>
#else
			where A : notnull =>
#endif
			source
				.AsDict()
				.AsPairs();

		public static IEnumerable<(U t, T u)> Flip<T, U>(this IEnumerable<(T t, U u)> source) =>
			source.Select(entry => (entry.u, entry.t));

		public static IEnumerable<(V t, U u, T v)> Flip<T, U, V>(this IEnumerable<(T t, U u, V v)> source) =>
			source.Select(entry => (entry.v, entry.u, entry.t));

		public static IEnumerable<(W t, V u, U v, T w)> Flip<T, U, V, W>(this IEnumerable<(T t, U u, V v, W w)> source) =>
			source.Select(entry => (entry.w, entry.v, entry.u, entry.t));

		#region zipwith

		public static IEnumerable<(T t, U u)> ZipWith<T, U>(
			this IEnumerable<T> first,
			IEnumerable<U> second
		) =>
			first.ZipWith(second, (t, u) => (t, u));

		public static IEnumerable<(T t, U u, V v)> ZipWith<T, U, V>(
			this IEnumerable<T> first,
			IEnumerable<U> second,
			IEnumerable<V> third
		) =>
			first.ZipWith(second, third, (t, u, v) => (t, u, v));

		public static IEnumerable<(T t, U u, V v, W w)> ZipWith<T, U, V, W>(
			this IEnumerable<T> first,
			IEnumerable<U> second,
			IEnumerable<V> third,
			IEnumerable<W> fourth
		) =>
			first.ZipWith(second, third, fourth, (t, u, v, w) => (t, u, v, w));

		public static IEnumerable<R> ZipWith<T, U, R>(
			this IEnumerable<T> first,
			IEnumerable<U> second,
			Func<T, U, R> transform
		) {
			using (var eT = first.GetEnumerator())
			using (var eU = second.GetEnumerator()) {
				while (eT.MoveNext() && eU.MoveNext()) {
					yield return transform(eT.Current, eU.Current);
				}
			}
		}

		public static IEnumerable<R> ZipWith<T, U, V, R>(
			this IEnumerable<T> first,
			IEnumerable<U> second,
			IEnumerable<V> third,
			Func<T, U, V, R> transform
		) {
			using (var eT = first.GetEnumerator())
			using (var eU = second.GetEnumerator())
			using (var eV = third.GetEnumerator()) {
				while (eT.MoveNext() && eU.MoveNext() && eV.MoveNext()) {
					yield return transform(eT.Current, eU.Current, eV.Current);
				}
			}
		}

		public static IEnumerable<R> ZipWith<T, U, V, W, R>(
			this IEnumerable<T> first,
			IEnumerable<U> second,
			IEnumerable<V> third,
			IEnumerable<W> fourth,
			Func<T, U, V, W, R> transform
		) {
			using (var eT = first.GetEnumerator())
			using (var eU = second.GetEnumerator())
			using (var eV = third.GetEnumerator())
			using (var eW = fourth.GetEnumerator()) {
				while (eT.MoveNext() && eU.MoveNext() && eV.MoveNext() && eW.MoveNext()) {
					yield return transform(eT.Current, eU.Current, eV.Current, eW.Current);
				}
			}
		}

		#endregion

		#region unzip

		public static (IEnumerable<T> ts, IEnumerable<U> us) Unzip<T, U>(this IEnumerable<(T t, U u)> source) =>
			source.Unzip((ts, us) => (ts, us));

		public static (IEnumerable<T> ts, IEnumerable<U> us, IEnumerable<V> vs) Unzip<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source
		) =>
			source.Unzip((ts, us, vs) => (ts, us, vs));

		public static (IEnumerable<T> ts, IEnumerable<U> us, IEnumerable<V> vs, IEnumerable<W> ws) Unzip<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source
		) =>
			source.Unzip((ts, us, vs, ws) => (ts, us, vs, ws));

		public static R Unzip<T, U, R>(
			this IEnumerable<(T t, U u)> source,
			Func<IEnumerable<T>, IEnumerable<U>, R> transform
		) {
			var (ts, us) = source
				.Reduce(
					(acc, pair) => {
						acc.ts.Add(pair.t);
						acc.us.Add(pair.u);
						return acc;
					},
					(ts: new List<T>(), us: new List<U>())
				);
			return transform.Invoke(ts, us);
		}

		public static R Unzip<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<IEnumerable<T>, IEnumerable<U>, IEnumerable<V>, R> transform
		) {
			var (ts, us, vs) = source
				.Reduce(
					(acc, pair) => {
						acc.ts.Add(pair.t);
						acc.us.Add(pair.u);
						acc.vs.Add(pair.v);
						return acc;
					},
					(ts: new List<T>(), us: new List<U>(), vs: new List<V>())
				);
			return transform.Invoke(ts, us, vs);
		}

		public static R Unzip<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<IEnumerable<T>, IEnumerable<U>, IEnumerable<V>, IEnumerable<W>, R> transform
		) {
			var (ts, us, vs, ws) = source
				.Reduce(
					(acc, pair) => {
						acc.ts.Add(pair.t);
						acc.us.Add(pair.u);
						acc.vs.Add(pair.v);
						acc.ws.Add(pair.w);
						return acc;
					},
					(ts: new List<T>(), us: new List<U>(), vs: new List<V>(), ws: new List<W>())
				);
			return transform.Invoke(ts, us, vs, ws);
		}

		#endregion
	}
}
