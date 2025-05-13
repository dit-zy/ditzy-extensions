using System;
using System.Collections;
using System.Collections.Generic;

namespace DitzyExtensions.Collection {
#if NET6_0_OR_GREATER
	public interface IMultiDict<K, V> : IEnumerable<(K Key, V Value)> where K : notnull {
#else
	public interface IMultiDict<K, V> : IEnumerable<(K Key, V Value)> {
#endif
		
		int Count { get; }
		
		int CountKeys { get; }

		ICollection<K> Keys { get; }
		
		ICollection<V> Values { get; }
		
		ICollection<V> this[K key] { get; set; }

#if NET6_0_OR_GREATER
		bool TryGetValues(K key, out ICollection<V>? values);
#else
		bool TryGetValues(K key, out ICollection<V> values);
#endif

		void Clear();

		bool ContainsKey(K key);

		void Add(K key, V value);

		void AddRange(K key, IEnumerable<V> values);

		bool Remove(K key);

		bool Remove(K key, V value);
	}
}
