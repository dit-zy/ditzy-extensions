/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace DitzyExtensions.Testing.Assertions.Functional {
	public class ResultAssertions<T, E> : ReferenceTypeAssertions<Result<T, E>, ResultAssertions<T, E>> {
		protected override string Identifier => $"Result<{typeof(T).Name},{typeof(E).Name}>";

		private readonly AssertionChain _chain;

		public ResultAssertions(Result<T, E> instance, AssertionChain chain) : base(instance, chain) {
			_chain = chain;
		}

		private AndConstraint<ResultAssertions<T, E>> Be(
			Result<T, E> expectedValue,
			string because,
			params object[] becauseArgs
		) {
			_chain
				.BecauseOf(because, becauseArgs)
				.ForCondition(Equals(Subject, expectedValue))
				.WithDefaultIdentifier(Identifier)
				.FailWith(
					"Expected {context} to be {0}{reason}, but found {1}.",
					expectedValue.ToString(),
					Subject.ToString()
				);

			return new AndConstraint<ResultAssertions<T, E>>(this);
		}

		private AndConstraint<ResultAssertions<T, E>> NotBe(
			Result<T, E> expectedValue,
			string because,
			params object[] becauseArgs
		) {
			_chain
				.BecauseOf(because, becauseArgs)
				.ForCondition(!Equals(Subject, expectedValue))
				.WithDefaultIdentifier(Identifier)
				.FailWith(
					"Expected {context} to not be {0}{reason}, but found {1}.",
					expectedValue.ToString(),
					Subject.ToString()
				);

			return new AndConstraint<ResultAssertions<T, E>>(this);
		}

		[CustomAssertion]
		public AndConstraint<ResultAssertions<T, E>> BeSuccess(
			string because = "",
			params object[] becauseArgs
		) {
			_chain
				.BecauseOf(because, becauseArgs)
				.ForCondition(Subject.IsSuccess)
				.WithDefaultIdentifier(Identifier)
				.FailWith(
					"Expected {context} to be Success{reason}, but found {0}.",
					Subject.ToString()
				);

			return new AndConstraint<ResultAssertions<T, E>>(this);
		}

		[CustomAssertion]
		public AndConstraint<ResultAssertions<T, E>> BeSuccess(
			T successValue,
			string because = "",
			params object[] becauseArgs
		) =>
			Be(Result.Success<T, E>(successValue), because, becauseArgs);

		[CustomAssertion]
		public AndConstraint<ResultAssertions<T, E>> NotBeSuccess(
			T successValue,
			string because = "",
			params object[] becauseArgs
		) =>
			NotBe(Result.Success<T, E>(successValue), because, becauseArgs);

		[CustomAssertion]
		public AndConstraint<ResultAssertions<T, E>> BeFailure(
			string because = "",
			params object[] becauseArgs
		) {
			_chain
				.BecauseOf(because, becauseArgs)
				.ForCondition(Subject.IsFailure)
				.WithDefaultIdentifier(Identifier)
				.FailWith(
					"Expected {context} to be Success{reason}, but found {0}.",
					Subject.ToString()
				);

			return new AndConstraint<ResultAssertions<T, E>>(this);
		}

		[CustomAssertion]
		public AndConstraint<ResultAssertions<T, E>> BeFailure(
			E errorValue,
			string because = "",
			params object[] becauseArgs
		) =>
			Be(Result.Failure<T, E>(errorValue), because, becauseArgs);

		[CustomAssertion]
		public AndConstraint<ResultAssertions<T, E>> NotBeFailure(
			E errorValue,
			string because = "",
			params object[] becauseArgs
		) =>
			NotBe(Result.Failure<T, E>(errorValue), because, becauseArgs);
	}
}
