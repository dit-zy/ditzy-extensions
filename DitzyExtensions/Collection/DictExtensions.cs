using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using DitzyExtensions.Functional;
#if N48_S2
using System.Collections.ObjectModel;
#else
using System.Collections.Immutable;
#endif

namespace DitzyExtensions.Collection {
	public static class DictExtensions {
		#region conversions

		public static IDictionary<K, V> AsDict<K, V>(this IEnumerable<KeyValuePair<K, V>> source)
#if N48_S2
			=>
#else
			where K : notnull =>
#endif
			source.Select(entry => (entry.Key, entry.Value)).AsDict();

		public static IDictionary<K, V> AsDict<K, V>(this IEnumerable<(K key, V value)> source)
#if N48_S2
			=>
				new ReadOnlyDictionary<K, V>(
					source
#else
			where K : notnull =>
			source
#endif
				.GroupBy(entry => entry.key)
				.Select(grouping => grouping.Last())
#if N48_S2
						.ToDictionary(entry => entry.key, entry => entry.value)
				);
#else
				.ToImmutableDictionary(entry => entry.key, entry => entry.value);
#endif

		public static IDictionary<K, V> AsDict<K, V>(this IEnumerable<V> source, Func<V, K> keySelector)
#if N48_S2
			=>
#else
			where K : notnull =>
#endif
			source.AsDict(keySelector, value => value);

		public static IDictionary<K, V> AsDict<T, K, V>(
			this IEnumerable<T> source,
			Func<T, K> keySelector,
			Func<T, V> valueSelector
		)
#if N48_S2
			=>
#else
			where K : notnull =>
#endif
			source.Select(entry => (keySelector(entry), valueSelector(entry))).AsDict();

		public static IDictionary<K, V> AsMutableDict<K, V>(this IEnumerable<KeyValuePair<K, V>> source)
#if N48_S2
			=>
#else
			where K : notnull =>
#endif
			source.Select(entry => (entry.Key, entry.Value)).AsMutableDict();

		public static IDictionary<K, V> AsMutableDict<K, V>(this IEnumerable<(K, V)> source)
#if N48_S2
			=>
#else
			where K : notnull =>
#endif
			source
				.AsDict()
				.ToDictionary(entry => entry.Key, entry => entry.Value);

		public static IMultiDict<K, V> AsMultiDict<K, V>(this IEnumerable<KeyValuePair<K, V>> source)
#if N48_S2
			=>
#else
			where K : notnull =>
#endif
			source.Select(entry => (entry.Key, entry.Value)).AsMultiDict();

		public static IMultiDict<K, V> AsMultiDict<K, V>(this IEnumerable<(K key, V value)> source)
#if N48_S2
			=>
#else
			where K : notnull =>
#endif
			new ImmutableMultiDict<K, V>(source.AsMutableMultiDict());

		public static IMultiDict<K, V> AsMultiDict<K, V>(this IEnumerable<V> source, Func<V, K> keySelector)
#if N48_S2
			=>
#else
			where K : notnull =>
#endif
			source.AsMultiDict(keySelector, value => value);

		public static IMultiDict<K, V> AsMultiDict<T, K, V>(
			this IEnumerable<T> source,
			Func<T, K> keySelector,
			Func<T, V> valueSelector
		)
#if N48_S2
			=>
#else
			where K : notnull =>
#endif
			source.Select(entry => (keySelector(entry), valueSelector(entry))).AsMultiDict();

		public static IMultiDict<K, V> AsMutableMultiDict<K, V>(this IEnumerable<KeyValuePair<K, V>> source)
#if N48_S2
			=>
#else
			where K : notnull =>
#endif
			source.Select(entry => (entry.Key, entry.Value)).AsMutableMultiDict();

		public static IMultiDict<K, V> AsMutableMultiDict<K, V>(this IEnumerable<(K key, V value)> source)
#if N48_S2
			=>
#else
			where K : notnull =>
#endif
			source
				.GroupBy(entry => entry.key)
				.Reduce(
					(acc, grouping) => {
						acc.AddRange(grouping.Key, grouping.Select(kv => kv.value));
						return acc;
					},
					new MultiDict<K, V>()
				);

		public static IMultiDict<K, V> AsMutableMultiDict<K, V>(this IEnumerable<V> source, Func<V, K> keySelector)
#if N48_S2
			=>
#else
			where K : notnull =>
#endif
			source.AsMutableMultiDict(keySelector, value => value);

		public static IMultiDict<K, V> AsMutableMultiDict<T, K, V>(
			this IEnumerable<T> source,
			Func<T, K> keySelector,
			Func<T, V> valueSelector
		)
#if N48_S2
			=>
#else
			where K : notnull =>
#endif
			source.Select(entry => (keySelector(entry), valueSelector(entry))).AsMutableMultiDict();

		#endregion

		#region loops

		public static IDictionary<K, V> ForEachEntry<K, V>(this IEnumerable<KeyValuePair<K, V>> source, Action<K, V> action)
#if N48_S2
			=>
#else
			where K : notnull =>
#endif
			source.ForEach(kv => action(kv.Key, kv.Value)).AsDict();

		public static IDictionary<K, V> ForEachEntry<K, V>(
			this IEnumerable<KeyValuePair<K, V>> source,
			Action<K, V, int> action
		)
#if N48_S2
			=>
#else
			where K : notnull =>
#endif
			source.ForEach((kv, index) => action(kv.Key, kv.Value, index)).AsDict();

		public static IEnumerable<T> SelectEntries<K, V, T>(
			this IEnumerable<KeyValuePair<K, V>> source,
			Func<K, V, T> transform
		) =>
			source.Select(kv => transform(kv.Key, kv.Value));

		public static IEnumerable<T> SelectEntries<K, V, T>(
			this IEnumerable<KeyValuePair<K, V>> source,
			Func<K, V, int, T> transform
		) =>
			source.Select((kv, index) => transform(kv.Key, kv.Value, index));

		public static IEnumerable<T> SelectManyEntries<K, V, T>(
			this IEnumerable<KeyValuePair<K, V>> source,
			Func<K, V, IEnumerable<T>> transform
		) =>
			source.SelectMany(kv => transform(kv.Key, kv.Value));

		public static IEnumerable<T> SelectManyEntries<K, V, T>(
			this IEnumerable<KeyValuePair<K, V>> source,
			Func<K, V, int, IEnumerable<T>> transform
		) =>
			source.SelectMany((kv, index) => transform(kv.Key, kv.Value, index));

		public static IEnumerable<KeyValuePair<K, V>> WhereEntries<K, V>(
			this IEnumerable<KeyValuePair<K, V>> source,
			Func<K, V, bool> transform
		) =>
			source.Where(kv => transform(kv.Key, kv.Value));

		public static IEnumerable<KeyValuePair<K, V>> WhereEntries<K, V>(
			this IEnumerable<KeyValuePair<K, V>> source,
			Func<K, V, int, bool> transform
		) =>
			source.Where((kv, index) => transform(kv.Key, kv.Value, index));

		#endregion

#if N48_S2
		public static V GetValueOrDefault<K, V>(this IDictionary<K, V> source, K key, V defaultValue = default) =>
#else
		public static V GetValueOrDefault<K, V>(this IDictionary<K, V> source, K key, V defaultValue = default!) =>
#endif
			source.TryGetValue(key, out var value) ? value : defaultValue;

#if N48_S2
		public static V GetValueOrSetDefault<K, V>(this IDictionary<K, V> source, K key, V defaultValue = default) {
#else
		public static V GetValueOrSetDefault<K, V>(this IDictionary<K, V> source, K key, V defaultValue = default!) {
#endif
			if (source.TryGetValue(key, out var value)) return value;
			source.Add(key, defaultValue);
			return defaultValue;
		}

		public static bool NotContainsKey<K, V>(this IDictionary<K, V> source, K key) => !source.ContainsKey(key);

		public static IDictionary<K, V> With<K, V>(this IDictionary<K, V> source, params (K, V)[] entries)
#if N48_S2
		{
#else
			where K : notnull {
#endif
			var dict = source.IsReadOnly ? source.AsMutableDict() : source;
			entries.ForEach(entry => dict[entry.Item1] = entry.Item2);
			return source.IsReadOnly ? dict.AsDict() : source;
		}

		public static IDictionary<K, V> Without<K, V>(this IDictionary<K, V> source, params K[] keys)
#if N48_S2
		{
#else
			where K : notnull {
#endif
			var dict = source.IsReadOnly ? source.AsMutableDict() : source;
			keys.ForEach(key => dict.Remove(key));
			return source.IsReadOnly ? dict.AsDict() : dict;
		}

		public static IDictionary<V, K> Flip<K, V>(this IDictionary<K, V> source)
#if N48_S2
			=>
#else
			where V : notnull =>
#endif
			source
				.Select(entry => (entry.Value, entry.Key))
				.AsDict();

		public static IDictionary<K, V> Update<K, V>(
			this IDictionary<K, V> source,
			IEnumerable<(K key, V value)> updateEntries
#if N48_S2
		) {
#else
		) where K : notnull {
#endif
			if (source.IsReadOnly)
				return source
					.AsPairs()
					.Concat(updateEntries)
					.AsDict();

			updateEntries.ForEach(entry => source[entry.key] = entry.value);
			return source;
		}

		public static IDictionary<K, V> UpdateKey<K, V>(this IDictionary<K, V> source, K key, Func<K, V, V> updateFunc) {
			if (source.TryGetValue(key, out var value)) {
				source.Remove(key);
				source.Add(key, updateFunc(key, value));
			}
			return source;
		}

		public static IDictionary<K, V> Put<K, V>(this IDictionary<K, V> source, K key, V value) {
			source.Remove(key);
			source.Add(key, value);
			return source;
		}

		public static IDictionary<K, V> UseToUpdate<K, V>(
			this IEnumerable<(K, V)> updateEntries,
			IDictionary<K, V> target
#if N48_S2
		) =>
#else
		) where K : notnull =>
#endif
			target.Update(updateEntries);

		public static Maybe<V> MaybeRemove<K, V>(this IDictionary<K, V> source, K key) {
			if (!source.TryGetValue(key, out var value)) return Maybe<V>.None;
			source.Remove(key);
			return value;
		}

		public static IDictionary<K, V> VerifyEnumDictionary<K, V>(this IDictionary<K, V> enumDict)
			where K : struct, Enum {
#if NET7_0_OR_GREATER
			var allEnumsAreInDict = (Enum.GetValuesAsUnderlyingType<K>() as K[])!.All(enumDict.ContainsKey);
#else
			var allEnumsAreInDict = (Enum.GetValues(typeof(K)) as K[] ?? Array.Empty<K>()).All(enumDict.ContainsKey);
#endif
			if (!allEnumsAreInDict) {
				throw new Exception($"All values of enum [{typeof(K).Name}] must be in the dictionary.");
			}

			return enumDict.AsDict();
		}
	}
}
