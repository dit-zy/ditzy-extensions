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
	public class MaybeAssertions<T> : ReferenceTypeAssertions<Maybe<T>, MaybeAssertions<T>> {
		protected override string Identifier => $"Maybe<{typeof(T).Name}>";

		private readonly AssertionChain _chain;

		public MaybeAssertions(Maybe<T> instance, AssertionChain chain) : base(instance, chain) {
			_chain = chain;
		}

		private AndConstraint<MaybeAssertions<T>> Be(
			Maybe<T> expectedValue,
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

			return new AndConstraint<MaybeAssertions<T>>(this);
		}

		private AndConstraint<MaybeAssertions<T>> NotBe(
			Maybe<T> expectedValue,
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

			return new AndConstraint<MaybeAssertions<T>>(this);
		}

		[CustomAssertion]
		public AndConstraint<MaybeAssertions<T>> HaveValue(
			string because = "",
			params object[] becauseArgs
		) {
			_chain
				.BecauseOf(because, becauseArgs)
				.ForCondition(Subject.HasValue)
				.WithDefaultIdentifier(Identifier)
				.FailWith(
					"Expected {context} to have a value{reason}, but found {0}.",
					Subject.ToString()
				);

			return new AndConstraint<MaybeAssertions<T>>(this);
		}

		[CustomAssertion]
		public AndConstraint<MaybeAssertions<T>> HaveValue(
			T value,
			string because = "",
			params object[] becauseArgs
		) =>
			Be(Maybe.From(value), because, becauseArgs);

		[CustomAssertion]
		public AndConstraint<MaybeAssertions<T>> NotHaveValue(
			T value,
			string because = "",
			params object[] becauseArgs
		) =>
			NotBe(Maybe.From(value), because, becauseArgs);

		[CustomAssertion]
		public AndConstraint<MaybeAssertions<T>> HaveNoValue(
			string because = "",
			params object[] becauseArgs
		) {
			_chain
				.BecauseOf(because, becauseArgs)
				.ForCondition(Subject.HasNoValue)
				.WithDefaultIdentifier(Identifier)
				.FailWith(
					"Expected {context} to have no value{reason}, but found {0}.",
					Subject.ToString()
				);

			return new AndConstraint<MaybeAssertions<T>>(this);
		}
	}
}
