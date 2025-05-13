using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DitzyExtensions.Collection {
#if NET6_0_OR_GREATER
	public class ImmutableMultiDict<K, V> : IMultiDict<K, V> where K : notnull {
#else
	public class ImmutableMultiDict<K, V> : IMultiDict<K, V> {
#endif
		private static readonly string NotSupportedExceptionMessage
			= $"Operation not supported on {nameof(ImmutableMultiDict<K, V>)}";

		private readonly IDictionary<K, IList<V>> _contents;

		public int Count { get; }

		public int CountKeys => _contents.Count;

		public ICollection<K> Keys => _contents.Keys;

		public ICollection<V> Values => _contents.Values.Flatten().AsList();

		public ImmutableMultiDict(IEnumerable<(K, V)> contents) : this(new MultiDict<K, V>(contents)) { }

		public ImmutableMultiDict(IEnumerable<(K, IEnumerable<V>)> contents) : this(new MultiDict<K, V>(contents)) { }

		public ImmutableMultiDict(MultiDict<K, V> contents) {
			_contents = contents.RawContents.AsDict();
			Count = contents.Count;
		}

		public ICollection<V> this[K key] {
#if NET6_0_OR_GREATER
			get => TryGetValues(key, out var values) ? values! : Array.Empty<V>();
#else
			get => TryGetValues(key, out var values) ? values : Array.Empty<V>();
#endif
			set { throw new NotSupportedException(NotSupportedExceptionMessage); }
		}

#if NET6_0_OR_GREATER
		public bool TryGetValues(K key, out ICollection<V>? values) {
#else
		public bool TryGetValues(K key, out ICollection<V> values) {
#endif
			var result = _contents.TryGetValue(key, out var rawValues);
			values = rawValues;
			return result;
		}

		public bool ContainsKey(K key) => _contents.ContainsKey(key);

		public IEnumerator<(K Key, V Value)> GetEnumerator() =>
			_contents
				.SelectManyEntries((k, v) => v.Select(value => (k, value)))
				.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public void Clear() => throw new NotSupportedException(NotSupportedExceptionMessage);

		public void Add(K key, V value) => throw new NotSupportedException(NotSupportedExceptionMessage);

		public void AddRange(K key, IEnumerable<V> values) => throw new NotSupportedException(NotSupportedExceptionMessage);

		public bool Remove(K key) => throw new NotSupportedException(NotSupportedExceptionMessage);

		public bool Remove(K key, V value) => throw new NotSupportedException(NotSupportedExceptionMessage);
	}
}
