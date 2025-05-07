using System;
using FluentAssertions.Execution;

namespace DitzyExtensions.Tests.TestUtils {
	public static class AssertionUtils {
		public static void WithAssertionScope(Action action) {
			using (new AssertionScope()) {
				action();
			}
		}
	}
}
