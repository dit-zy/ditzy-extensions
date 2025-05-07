using System.Numerics;
using FluentAssertions.Execution;

namespace DitzyExtensions.Tests.TestUtils {
	public static class VectorTestExtensions {
		public static Vector2Assertions Should(this Vector2 instance) =>
			new Vector2Assertions(instance, AssertionChain.GetOrCreate());

		public static Vector3Assertions Should(this Vector3 instance) =>
			new Vector3Assertions(instance, AssertionChain.GetOrCreate());

		public static Vector4Assertions Should(this Vector4 instance) =>
			new Vector4Assertions(instance, AssertionChain.GetOrCreate());
	}
}
