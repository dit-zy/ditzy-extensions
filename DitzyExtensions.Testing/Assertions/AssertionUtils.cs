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
