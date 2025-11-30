/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

using System.Collections.Generic;

namespace DitzyExtensions.Functional {
	public interface IAccumulatedResults<out T, E> {
		T Value { get; }

		ICollection<E> Errors { get; }

		bool HasValue { get; }

		bool HasErrors { get; }
	}
}
