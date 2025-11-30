/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

using System;
using FluentAssertions.Execution;

namespace DitzyExtensions.Testing.Assertions {
	public static class AssertionUtils {
		public static void WithAssertionScope(Action action) {
			using (new AssertionScope()) {
				action();
			}
		}
	}
}
