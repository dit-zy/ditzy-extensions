/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

namespace DitzyExtensions.Generators.Tests;

public class SwizzleExtensionGeneratorTest {
	[Fact]
	public Task GeneratesSimpleSwizzleExtension() =>
		TestHelper.VerifySource(
			"""
			  using DitzyExtensions.Generators;
				using System.Numerics;
				
				namespace GenTests.Tests;
			  
			  [Swizzle(typeof(Vector4), nameof(Vector4.X), nameof(Vector4.Y), nameof(Vector4.Z), nameof(Vector4.W))]
			  public static partial class VectorExtensions { }
			"""
		);
	
	[Fact]
	public Task GeneratesSubsetSwizzleExtension() =>
		TestHelper.VerifySource(
			"""
			  using DitzyExtensions.Generators;
				using System.Numerics;
				
				namespace GenTests.Tests;
			  
			  [Swizzle(typeof(Vector4), typeof(Vector3), 3, nameof(Vector4.X), nameof(Vector4.Y), nameof(Vector4.Z), nameof(Vector4.W))]
			  public static partial class VectorExtensions { }
			"""
		);
	
	[Fact]
	public Task GeneratesSimpleSwizzleExtensionWithNamedArgs() =>
		TestHelper.VerifySource(
			"""
			  using DitzyExtensions.Generators;
				using System.Numerics;
				
				namespace GenTests.Tests;
			  
			  [Swizzle(
					SourceClass = typeof(Vector4),
					ComponentsToSwizzle = new[] {nameof(Vector4.X), nameof(Vector4.Y), nameof(Vector4.Z), nameof(Vector4.W)}
				)]
			  public static partial class VectorExtensions { }
			"""
		);
	
	[Fact]
	public Task GeneratesSubsetSwizzleExtensionWithNamedArgs() =>
		TestHelper.VerifySource(
			"""
			  using DitzyExtensions.Generators;
				using System.Numerics;
				
				namespace GenTests.Tests;
			  
			  [Swizzle(
					SourceClass = typeof(Vector4),
					TargetClass = typeof(Vector3),
					NumComponentsToChoose = 3,
					ComponentsToSwizzle = new[] {nameof(Vector4.X), nameof(Vector4.Y), nameof(Vector4.Z), nameof(Vector4.W)}
				)]
			  public static partial class VectorExtensions { }
			"""
		);
	
	[Fact]
	public Task GeneratesMultipleSwizzleExtensions() =>
		TestHelper.VerifySource(
			"""
			  using DitzyExtensions.Generators;
				using System.Numerics;
				
				namespace GenTests.Tests;
			  
			  [Swizzle(typeof(Vector2), new[] {nameof(Vector2.X), nameof(Vector2.Y)})]
			  [Swizzle(
					SourceClass = typeof(Vector3),
					TargetClass = typeof(Vector2),
					NumComponentsToChoose = 2,
					ComponentsToSwizzle = new[] {nameof(Vector3.X), nameof(Vector3.Y), nameof(Vector3.Z)}
				)]
			  public static partial class VectorExtensions { }
			"""
		);
}
