using System;
using CSharpFunctionalExtensions;

namespace DitzyExtensions {
	public class ExceptionExtensions {
		public static Result<T, U> Try<T, U, E>(Func<T> action, Func<E, U> catchAction) where E : Exception {
			try {
				return action();
			} catch (E e) {
				return catchAction(e);
			}
		}
	}
}
