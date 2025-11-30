/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

using System.Numerics;
using DitzyExtensions.Generators;

namespace DitzyExtensions {
	[Swizzle(
		SourceClass = typeof(Vector2),
		ComponentsToSwizzle = new[] { nameof(Vector2.X), nameof(Vector2.Y) }
	)]
	[Swizzle(
		SourceClass = typeof(Vector2),
		TargetClass = typeof(Vector3),
		NumComponentsToChoose = 3,
		ComponentsToSwizzle = new[] { nameof(Vector2.X), nameof(Vector2.Y) }
	)]
	[Swizzle(
		SourceClass = typeof(Vector2),
		TargetClass = typeof(Vector4),
		NumComponentsToChoose = 4,
		ComponentsToSwizzle = new[] { nameof(Vector2.X), nameof(Vector2.Y) }
	)]
	[Swizzle(
		SourceClass = typeof(Vector3),
		TargetClass = typeof(Vector2),
		NumComponentsToChoose = 2,
		ComponentsToSwizzle = new[] { nameof(Vector3.X), nameof(Vector3.Y), nameof(Vector3.Z) }
	)]
	[Swizzle(
		SourceClass = typeof(Vector3),
		ComponentsToSwizzle = new[] { nameof(Vector3.X), nameof(Vector3.Y), nameof(Vector3.Z) }
	)]
	[Swizzle(
		SourceClass = typeof(Vector3),
		TargetClass = typeof(Vector4),
		NumComponentsToChoose = 4,
		ComponentsToSwizzle = new[] { nameof(Vector3.X), nameof(Vector3.Y), nameof(Vector3.Z) }
	)]
	[Swizzle(
		SourceClass = typeof(Vector4),
		TargetClass = typeof(Vector2),
		NumComponentsToChoose = 2,
		ComponentsToSwizzle = new[] { nameof(Vector4.X), nameof(Vector4.Y), nameof(Vector4.Z), nameof(Vector4.W) }
	)]
	[Swizzle(
		SourceClass = typeof(Vector4),
		TargetClass = typeof(Vector3),
		NumComponentsToChoose = 3,
		ComponentsToSwizzle = new[] { nameof(Vector4.X), nameof(Vector4.Y), nameof(Vector4.Z), nameof(Vector4.W) }
	)]
	[Swizzle(
		SourceClass = typeof(Vector4),
		ComponentsToSwizzle = new[] { nameof(Vector4.X), nameof(Vector4.Y), nameof(Vector4.Z), nameof(Vector4.W) }
	)]
	public static partial class VectorExtensions { }
}
