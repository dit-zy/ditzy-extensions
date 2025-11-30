/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

using System.Numerics;
using DitzyExtensions.Tests.TestUtils;
using FluentAssertions.Execution;

namespace DitzyExtensions.Testing.Assertions {
	public static class VectorAssertionsExtensions {
		public static Vector2Assertions Should(this Vector2 instance) =>
			new Vector2Assertions(instance, AssertionChain.GetOrCreate());

		public static Vector3Assertions Should(this Vector3 instance) =>
			new Vector3Assertions(instance, AssertionChain.GetOrCreate());

		public static Vector4Assertions Should(this Vector4 instance) =>
			new Vector4Assertions(instance, AssertionChain.GetOrCreate());
	}
}
