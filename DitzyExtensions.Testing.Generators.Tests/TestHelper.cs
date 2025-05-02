using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DitzyExtensions.Testing.Generators.Tests;

public static class TestHelper {
	public static Task VerifySource(string source) {
		var syntaxTree = CSharpSyntaxTree.ParseText(source);
		IList<PortableExecutableReference> references = new[] {
			MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
		};

		var compilation = CSharpCompilation.Create(
			assemblyName: "Tests",
			syntaxTrees: new[] { syntaxTree },
			references: references
		);

		var generator = new FsCheckOverloadsExtensionGenerator();

		GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

		driver = driver.RunGenerators(compilation);

		return Verify(driver).UseDirectory("Snapshots");
	}
}
