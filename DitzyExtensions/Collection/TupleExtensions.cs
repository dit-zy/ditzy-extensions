using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DitzyExtensions.Functional;

namespace DitzyExtensions.Collection {
	public static class TupleExtensions {
		
		#region select

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
		
		public static IEnumerable<R> SelectFirst<T, U, R>(this IEnumerable<(T t, U u)> source, Func<T, int, R> transform) =>
			source.Select((tuple, i) => transform(tuple.t, i));

		public static IEnumerable<R> SelectFirst<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<T, int, R> transform
		) =>
			source.Select((tuple, i) => transform(tuple.t, i));

		public static IEnumerable<R> SelectFirst<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<T, int, R> transform
		) =>
			source.Select((tuple, i) => transform(tuple.t, i));

		public static IEnumerable<R> SelectSecond<T, U, R>(this IEnumerable<(T t, U u)> source, Func<U, int, R> transform) =>
			source.Select((tuple, i) => transform(tuple.u, i));

		public static IEnumerable<R> SelectSecond<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<U, int, R> transform
		) =>
			source.Select((tuple, i) => transform(tuple.u, i));

		public static IEnumerable<R> SelectSecond<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<U, int, R> transform
		) =>
			source.Select((tuple, i) => transform(tuple.u, i));

		public static IEnumerable<R> SelectThird<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<V, int, R> transform
		) =>
			source.Select((tuple, i) => transform(tuple.v, i));

		public static IEnumerable<R> SelectThird<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<V, int, R> transform
		) =>
			source.Select((tuple, i) => transform(tuple.v, i));

		public static IEnumerable<R> SelectFourth<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<W, int, R> transform
		) =>
			source.Select((tuple, i) => transform(tuple.w, i));

		public static IEnumerable<R> SelectEntries<T, U, R>(
			this IEnumerable<(T t, U u)> source,
			Func<T, U, R> transform
		) =>
			source.Select(tuple => transform(tuple.t, tuple.u));

		public static IEnumerable<R> SelectEntries<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<T, U, V, R> transform
		) =>
			source.Select(tuple => transform(tuple.t, tuple.u, tuple.v));

		public static IEnumerable<R> SelectEntries<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<T, U, V, W, R> transform
		) =>
			source.Select(tuple => transform(tuple.t, tuple.u, tuple.v, tuple.w));

		public static IEnumerable<R> SelectEntries<T, U, R>(
			this IEnumerable<(T t, U u)> source,
			Func<T, U, int, R> transform
		) =>
			source.Select((tuple, i) => transform(tuple.t, tuple.u, i));

		public static IEnumerable<R> SelectEntries<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<T, U, V, int, R> transform
		) =>
			source.Select((tuple, i) => transform(tuple.t, tuple.u, tuple.v, i));

		public static IEnumerable<R> SelectEntries<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<T, U, V, W, int, R> transform
		) =>
			source.Select((tuple, i) => transform(tuple.t, tuple.u, tuple.v, tuple.w, i));
		
		#endregion
		
		#region select many

		public static IEnumerable<R> SelectManyFirst<T, U, R>(this IEnumerable<(T t, U u)> source, Func<T, IEnumerable<R>> transform) =>
			source.SelectMany(tuple => transform(tuple.t));

		public static IEnumerable<R> SelectManyFirst<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<T, IEnumerable<R>> transform
		) =>
			source.SelectMany(tuple => transform(tuple.t));

		public static IEnumerable<R> SelectManyFirst<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<T, IEnumerable<R>> transform
		) =>
			source.SelectMany(tuple => transform(tuple.t));

		public static IEnumerable<R> SelectManySecond<T, U, R>(this IEnumerable<(T t, U u)> source, Func<U, IEnumerable<R>> transform) =>
			source.SelectMany(tuple => transform(tuple.u));

		public static IEnumerable<R> SelectManySecond<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<U, IEnumerable<R>> transform
		) =>
			source.SelectMany(tuple => transform(tuple.u));

		public static IEnumerable<R> SelectManySecond<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<U, IEnumerable<R>> transform
		) =>
			source.SelectMany(tuple => transform(tuple.u));

		public static IEnumerable<R> SelectManyThird<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<V, IEnumerable<R>> transform
		) =>
			source.SelectMany(tuple => transform(tuple.v));

		public static IEnumerable<R> SelectManyThird<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<V, IEnumerable<R>> transform
		) =>
			source.SelectMany(tuple => transform(tuple.v));

		public static IEnumerable<R> SelectManyFourth<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<W, IEnumerable<R>> transform
		) =>
			source.SelectMany(tuple => transform(tuple.w));
		
		public static IEnumerable<R> SelectManyFirst<T, U, R>(this IEnumerable<(T t, U u)> source, Func<T, int, IEnumerable<R>> transform) =>
			source.SelectMany((tuple, i) => transform(tuple.t, i));

		public static IEnumerable<R> SelectManyFirst<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<T, int, IEnumerable<R>> transform
		) =>
			source.SelectMany((tuple, i) => transform(tuple.t, i));

		public static IEnumerable<R> SelectManyFirst<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<T, int, IEnumerable<R>> transform
		) =>
			source.SelectMany((tuple, i) => transform(tuple.t, i));

		public static IEnumerable<R> SelectManySecond<T, U, R>(this IEnumerable<(T t, U u)> source, Func<U, int, IEnumerable<R>> transform) =>
			source.SelectMany((tuple, i) => transform(tuple.u, i));

		public static IEnumerable<R> SelectManySecond<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<U, int, IEnumerable<R>> transform
		) =>
			source.SelectMany((tuple, i) => transform(tuple.u, i));

		public static IEnumerable<R> SelectManySecond<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<U, int, IEnumerable<R>> transform
		) =>
			source.SelectMany((tuple, i) => transform(tuple.u, i));

		public static IEnumerable<R> SelectManyThird<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<V, int, IEnumerable<R>> transform
		) =>
			source.SelectMany((tuple, i) => transform(tuple.v, i));

		public static IEnumerable<R> SelectManyThird<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<V, int, IEnumerable<R>> transform
		) =>
			source.SelectMany((tuple, i) => transform(tuple.v, i));

		public static IEnumerable<R> SelectManyFourth<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<W, int, IEnumerable<R>> transform
		) =>
			source.SelectMany((tuple, i) => transform(tuple.w, i));

		public static IEnumerable<R> SelectManyEntries<T, U, R>(
			this IEnumerable<(T t, U u)> source,
			Func<T, U, IEnumerable<R>> transform
		) =>
			source.SelectMany(tuple => transform(tuple.t, tuple.u));

		public static IEnumerable<R> SelectManyEntries<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<T, U, V, IEnumerable<R>> transform
		) =>
			source.SelectMany(tuple => transform(tuple.t, tuple.u, tuple.v));

		public static IEnumerable<R> SelectManyEntries<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<T, U, V, W, IEnumerable<R>> transform
		) =>
			source.SelectMany(tuple => transform(tuple.t, tuple.u, tuple.v, tuple.w));

		public static IEnumerable<R> SelectManyEntries<T, U, R>(
			this IEnumerable<(T t, U u)> source,
			Func<T, U, int, IEnumerable<R>> transform
		) =>
			source.SelectMany((tuple, i) => transform(tuple.t, tuple.u, i));

		public static IEnumerable<R> SelectManyEntries<T, U, V, R>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<T, U, V, int, IEnumerable<R>> transform
		) =>
			source.SelectMany((tuple, i) => transform(tuple.t, tuple.u, tuple.v, i));

		public static IEnumerable<R> SelectManyEntries<T, U, V, W, R>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<T, U, V, W, int, IEnumerable<R>> transform
		) =>
			source.SelectMany((tuple, i) => transform(tuple.t, tuple.u, tuple.v, tuple.w, i));
		
		#endregion

		#region where

		public static IEnumerable<(T t, U u)> WhereFirst<T, U>(this IEnumerable<(T t, U u)> source, Func<T, bool> transform) =>
			source.Where(tuple => transform(tuple.t));

		public static IEnumerable<(T t, U u, V v)> WhereFirst<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<T, bool> transform
		) =>
			source.Where(tuple => transform(tuple.t));

		public static IEnumerable<(T t, U u, V v, W w)> WhereFirst<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<T, bool> transform
		) =>
			source.Where(tuple => transform(tuple.t));

		public static IEnumerable<(T t, U u)> WhereSecond<T, U>(this IEnumerable<(T t, U u)> source, Func<U, bool> transform) =>
			source.Where(tuple => transform(tuple.u));

		public static IEnumerable<(T t, U u, V v)> WhereSecond<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<U, bool> transform
		) =>
			source.Where(tuple => transform(tuple.u));

		public static IEnumerable<(T t, U u, V v, W w)> WhereSecond<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<U, bool> transform
		) =>
			source.Where(tuple => transform(tuple.u));

		public static IEnumerable<(T t, U u, V v)> WhereThird<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<V, bool> transform
		) =>
			source.Where(tuple => transform(tuple.v));

		public static IEnumerable<(T t, U u, V v, W w)> WhereThird<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<V, bool> transform
		) =>
			source.Where(tuple => transform(tuple.v));

		public static IEnumerable<(T t, U u, V v, W w)> WhereFourth<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<W, bool> transform
		) =>
			source.Where(tuple => transform(tuple.w));
		
		public static IEnumerable<(T t, U u)> WhereFirst<T, U>(this IEnumerable<(T t, U u)> source, Func<T, int, bool> transform) =>
			source.Where((tuple, i) => transform(tuple.t, i));

		public static IEnumerable<(T t, U u, V v)> WhereFirst<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<T, int, bool> transform
		) =>
			source.Where((tuple, i) => transform(tuple.t, i));

		public static IEnumerable<(T t, U u, V v, W w)> WhereFirst<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<T, int, bool> transform
		) =>
			source.Where((tuple, i) => transform(tuple.t, i));

		public static IEnumerable<(T t, U u)> WhereSecond<T, U>(this IEnumerable<(T t, U u)> source, Func<U, int, bool> transform) =>
			source.Where((tuple, i) => transform(tuple.u, i));

		public static IEnumerable<(T t, U u, V v)> WhereSecond<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<U, int, bool> transform
		) =>
			source.Where((tuple, i) => transform(tuple.u, i));

		public static IEnumerable<(T t, U u, V v, W w)> WhereSecond<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<U, int, bool> transform
		) =>
			source.Where((tuple, i) => transform(tuple.u, i));

		public static IEnumerable<(T t, U u, V v)> WhereThird<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<V, int, bool> transform
		) =>
			source.Where((tuple, i) => transform(tuple.v, i));

		public static IEnumerable<(T t, U u, V v, W w)> WhereThird<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<V, int, bool> transform
		) =>
			source.Where((tuple, i) => transform(tuple.v, i));

		public static IEnumerable<(T t, U u, V v, W w)> WhereFourth<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<W, int, bool> transform
		) =>
			source.Where((tuple, i) => transform(tuple.w, i));

		public static IEnumerable<(T t, U u)> WhereEntries<T, U>(
			this IEnumerable<(T t, U u)> source,
			Func<T, U, bool> transform
		) =>
			source.Where(tuple => transform(tuple.t, tuple.u));

		public static IEnumerable<(T t, U u, V v)> WhereEntries<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<T, U, V, bool> transform
		) =>
			source.Where(tuple => transform(tuple.t, tuple.u, tuple.v));

		public static IEnumerable<(T t, U u, V v, W w)> WhereEntries<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<T, U, V, W, bool> transform
		) =>
			source.Where(tuple => transform(tuple.t, tuple.u, tuple.v, tuple.w));

		public static IEnumerable<(T t, U u)> WhereEntries<T, U>(
			this IEnumerable<(T t, U u)> source,
			Func<T, U, int, bool> transform
		) =>
			source.Where((tuple, i) => transform(tuple.t, tuple.u, i));

		public static IEnumerable<(T t, U u, V v)> WhereEntries<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Func<T, U, V, int, bool> transform
		) =>
			source.Where((tuple, i) => transform(tuple.t, tuple.u, tuple.v, i));

		public static IEnumerable<(T t, U u, V v, W w)> WhereEntries<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Func<T, U, V, W, int, bool> transform
		) =>
			source.Where((tuple, i) => transform(tuple.t, tuple.u, tuple.v, tuple.w, i));

		#endregion
		
		#region foreach

		public static IEnumerable<(T t, U u)> ForEachFirst<T, U>(this IEnumerable<(T t, U u)> source, Action<T> transform) =>
			source.ForEach(tuple => transform(tuple.t));

		public static IEnumerable<(T t, U u, V v)> ForEachFirst<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Action<T> transform
		) =>
			source.ForEach(tuple => transform(tuple.t));

		public static IEnumerable<(T t, U u, V v, W w)> ForEachFirst<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Action<T> transform
		) =>
			source.ForEach(tuple => transform(tuple.t));

		public static IEnumerable<(T t, U u)> ForEachSecond<T, U>(this IEnumerable<(T t, U u)> source, Action<U> transform) =>
			source.ForEach(tuple => transform(tuple.u));

		public static IEnumerable<(T t, U u, V v)> ForEachSecond<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Action<U> transform
		) =>
			source.ForEach(tuple => transform(tuple.u));

		public static IEnumerable<(T t, U u, V v, W w)> ForEachSecond<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Action<U> transform
		) =>
			source.ForEach(tuple => transform(tuple.u));

		public static IEnumerable<(T t, U u, V v)> ForEachThird<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Action<V> transform
		) =>
			source.ForEach(tuple => transform(tuple.v));

		public static IEnumerable<(T t, U u, V v, W w)> ForEachThird<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Action<V> transform
		) =>
			source.ForEach(tuple => transform(tuple.v));

		public static IEnumerable<(T t, U u, V v, W w)> ForEachFourth<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Action<W> transform
		) =>
			source.ForEach(tuple => transform(tuple.w));
		
		public static IEnumerable<(T t, U u)> ForEachFirst<T, U>(this IEnumerable<(T t, U u)> source, Action<T, int> transform) =>
			source.ForEach((tuple, i) => transform(tuple.t, i));

		public static IEnumerable<(T t, U u, V v)> ForEachFirst<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Action<T, int> transform
		) =>
			source.ForEach((tuple, i) => transform(tuple.t, i));

		public static IEnumerable<(T t, U u, V v, W w)> ForEachFirst<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Action<T, int> transform
		) =>
			source.ForEach((tuple, i) => transform(tuple.t, i));

		public static IEnumerable<(T t, U u)> ForEachSecond<T, U>(this IEnumerable<(T t, U u)> source, Action<U, int> transform) =>
			source.ForEach((tuple, i) => transform(tuple.u, i));

		public static IEnumerable<(T t, U u, V v)> ForEachSecond<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Action<U, int> transform
		) =>
			source.ForEach((tuple, i) => transform(tuple.u, i));

		public static IEnumerable<(T t, U u, V v, W w)> ForEachSecond<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Action<U, int> transform
		) =>
			source.ForEach((tuple, i) => transform(tuple.u, i));

		public static IEnumerable<(T t, U u, V v)> ForEachThird<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Action<V, int> transform
		) =>
			source.ForEach((tuple, i) => transform(tuple.v, i));

		public static IEnumerable<(T t, U u, V v, W w)> ForEachThird<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Action<V, int> transform
		) =>
			source.ForEach((tuple, i) => transform(tuple.v, i));

		public static IEnumerable<(T t, U u, V v, W w)> ForEachFourth<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Action<W, int> transform
		) =>
			source.ForEach((tuple, i) => transform(tuple.w, i));

		public static IEnumerable<(T t, U u)> ForEachEntry<T, U>(
			this IEnumerable<(T t, U u)> source,
			Action<T, U> transform
		) =>
			source.ForEach(tuple => transform(tuple.t, tuple.u));

		public static IEnumerable<(T t, U u, V v)> ForEachEntry<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Action<T, U, V> transform
		) =>
			source.ForEach(tuple => transform(tuple.t, tuple.u, tuple.v));

		public static IEnumerable<(T t, U u, V v, W w)> ForEachEntry<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Action<T, U, V, W> transform
		) =>
			source.ForEach(tuple => transform(tuple.t, tuple.u, tuple.v, tuple.w));

		public static IEnumerable<(T t, U u)> ForEachEntry<T, U>(
			this IEnumerable<(T t, U u)> source,
			Action<T, U, int> transform
		) =>
			source.ForEach((tuple, i) => transform(tuple.t, tuple.u, i));

		public static IEnumerable<(T t, U u, V v)> ForEachEntry<T, U, V>(
			this IEnumerable<(T t, U u, V v)> source,
			Action<T, U, V, int> transform
		) =>
			source.ForEach((tuple, i) => transform(tuple.t, tuple.u, tuple.v, i));

		public static IEnumerable<(T t, U u, V v, W w)> ForEachEntry<T, U, V, W>(
			this IEnumerable<(T t, U u, V v, W w)> source,
			Action<T, U, V, W, int> transform
		) =>
			source.ForEach((tuple, i) => transform(tuple.t, tuple.u, tuple.v, tuple.w, i));
		
		#endregion

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
		
#if NET7_0_OR_GREATER
		public static T Sum<T>(this (T a, T b) tuple) where T : INumber<T> =>
			tuple.a + tuple.b;
		
		public static T Sum<T>(this (T a, T b, T c) tuple) where T : INumber<T> =>
			tuple.a + tuple.b + tuple.c;

		public static T Sum<T>(this (T a, T b, T c, T d) tuple) where T : INumber<T> =>
			tuple.a + tuple.b + tuple.c + tuple.d;
#endif
	}
}
