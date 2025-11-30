/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;

namespace DitzyExtensions.Collection {
	public class IncrementalComparerBuilder<T> {
		
		private readonly List<Func<T, T, int>> _comparers = new List<Func<T, T, int>>();
		
		public static IncrementalComparerBuilder<T> Create() => new IncrementalComparerBuilder<T>();

		public IncrementalComparerBuilder<T> AddComparison(Func<T, T, int> comparison) {
			_comparers.Add(comparison);
			return this;
		}

		public IComparer<T> Build() {
			return new IncrementalComparer(_comparers);
		}

		private class IncrementalComparer : IComparer<T> {
			private readonly IList<Func<T, T, int>> _comparers;

			public IncrementalComparer(IEnumerable<Func<T, T, int>> comparers) {
				_comparers = comparers.AsList();
			}

#if N48_S2
			public int Compare(T x, T y) {
				if (x == null && y == null) return 0;
				if (x == null) return -1;
				if (y == null) return 1;
#else
			public int Compare(T? x, T? y) {
				if (x is null && y is null) return 0;
				if (x is null) return -1;
				if (y is null) return 1;
#endif
				return _comparers
					.Select(comparison => comparison(x, y))
					.TryFirst(result => result != 0)
					.GetValueOrDefault(0);
			}
		}
	}
}
