/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

namespace DitzyExtensions.Testing.Generators.Tests;

public class FsCheckOverloadsExtensionGeneratorTest {
	[Fact]
	public Task GeneratesSimpleSwizzleExtension() =>
		TestHelper.VerifySource(
			"""
			  using DitzyExtensions.Testing.Generators;
				
				namespace GenTests.Tests;
			  
			  [FsCheckOverloads]
			  public static partial class FsCheckUtils { }
			"""
		);
}
