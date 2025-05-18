using CSharpFunctionalExtensions;
using DitzyExtensions.Testing.Assertions.Functional;
using FluentAssertions.Execution;

namespace DitzyExtensions.Testing.Functional.Result {
	public static class ResultAssertionsExtensions {
		public static ResultAssertions<T, E> Should<T, E>(this Result<T, E> subject) =>
			new ResultAssertions<T, E>(subject, AssertionChain.GetOrCreate());
	}
}
