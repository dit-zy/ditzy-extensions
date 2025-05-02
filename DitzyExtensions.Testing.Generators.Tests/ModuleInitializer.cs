using System.Runtime.CompilerServices;

namespace DitzyExtensions.Testing.Generators.Tests;

public static class ModuleInitializer {
	[ModuleInitializer]
	public static void Init() {
		VerifySourceGenerators.Initialize();
	}
}
