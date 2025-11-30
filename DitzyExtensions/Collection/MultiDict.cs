/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DitzyExtensions.Collection {
#if NET6_0_OR_GREATER
	public class MultiDict<K, V> : IMultiDict<K, V> where K : notnull {
#else
	public class MultiDict<K, V> : IMultiDict<K, V> {
#endif

		private readonly Dictionary<K, IList<V>> _contents = new Dictionary<K, IList<V>>();

		internal IDictionary<K, IList<V>> RawContents => _contents.AsDict();

		public int Count { get; private set; } = 0;

		public int CountKeys => _contents.Count;

		public ICollection<K> Keys => _contents.Keys;

		public ICollection<V> Values => _contents.Values.Flatten().AsList();

		public MultiDict() { }

		public MultiDict(IEnumerable<(K, V)> contents) {
			contents.ForEach(entry => Add(entry.Item1, entry.Item2));
		}

		public MultiDict(IEnumerable<(K, IEnumerable<V>)> contents) {
			contents.ForEach(entry => AddRange(entry.Item1, entry.Item2));
		}

		public ICollection<V> this[K key] {
#if NET6_0_OR_GREATER
			get => TryGetValues(key, out var values) ? values! : Array.Empty<V>();
#else
			get => TryGetValues(key, out var values) ? values : Array.Empty<V>();
#endif
			set {
				_contents.Remove(key);
				AddRange(key, value);
			}
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

		public IEnumerator<(K Key, V Value)> GetEnumerator() =>
			_contents
				.SelectManyEntries((k, v) => v.Select(value => (k, value)))
				.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public void Clear() {
			_contents.Clear();
			Count = 0;
		}

		public bool ContainsKey(K key) => _contents.ContainsKey(key);

		public void Add(K key, V value) {
			if (!_contents.TryGetValue(key, out var valueList)) {
				valueList = new List<V>();
				_contents.Add(key, valueList);
			}

			valueList.Add(value);
			Count++;
		}

		public void AddRange(K key, IEnumerable<V> values) {
			if (!_contents.TryGetValue(key, out var valueList)) {
				valueList = new List<V>();
				_contents.Add(key, valueList);
			}

			values.ForEach(value => {
					valueList.Add(value);
					Count++;
				}
			);
		}

		public bool Remove(K key) {
			var result = _contents.TryGetValue(key, out var values);
			if (!result) return false;
			_contents.Remove(key);
			Count -= values?.Count ?? 0;
			return true;
		}

		public bool Remove(K key, V value) {
			var result = _contents.TryGetValue(key, out var values);
			if (!result) return false;
#if NET6_0_OR_GREATER
			result = values!.Remove(value);
#else
			result = values.Remove(value);
#endif
			if (result) Count--;
			if (values.IsEmpty()) _contents.Remove(key);
			return result;
		}
	}
}
