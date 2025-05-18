using System;
using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace DitzyExtensions.Testing.Functional.Result {
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
			Be(CSharpFunctionalExtensions.Result.Success<T, E>(successValue), because, becauseArgs);

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
			Be(CSharpFunctionalExtensions.Result.Failure<T, E>(errorValue), because, becauseArgs);
	}
}
