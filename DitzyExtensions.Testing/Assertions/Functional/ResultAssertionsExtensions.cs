/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

using CSharpFunctionalExtensions;
using DitzyExtensions.Testing.Assertions.Functional;
using FluentAssertions.Execution;

namespace DitzyExtensions.Testing.Functional.Result {
	public static class ResultAssertionsExtensions {
		public static ResultAssertions<T, E> Should<T, E>(this Result<T, E> subject) =>
			new ResultAssertions<T, E>(subject, AssertionChain.GetOrCreate());
	}
}
