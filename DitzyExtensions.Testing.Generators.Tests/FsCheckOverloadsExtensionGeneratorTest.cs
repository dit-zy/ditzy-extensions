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
