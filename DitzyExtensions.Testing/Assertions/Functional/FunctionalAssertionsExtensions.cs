/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

using CSharpFunctionalExtensions;
using DitzyExtensions.Testing.Functional.Result;
using FluentAssertions.Execution;

namespace DitzyExtensions.Testing.Assertions.Functional {
	public static class FunctionalAssertionsExtensions {
		public static MaybeAssertions<T> Should<T, E>(this Maybe<T> subject) =>
			new MaybeAssertions<T>(subject, AssertionChain.GetOrCreate());
		
		public static ResultAssertions<T, E> Should<T, E>(this Result<T, E> subject) =>
			new ResultAssertions<T, E>(subject, AssertionChain.GetOrCreate());
	}
}
