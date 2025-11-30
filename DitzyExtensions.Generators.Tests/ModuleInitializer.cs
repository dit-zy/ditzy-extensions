/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

using System.Runtime.CompilerServices;

namespace DitzyExtensions.Generators.Tests;

public static class ModuleInitializer {
	[ModuleInitializer]
	public static void Init() {
		VerifySourceGenerators.Initialize();
	}
}
