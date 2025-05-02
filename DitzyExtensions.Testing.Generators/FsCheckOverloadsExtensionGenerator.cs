using System.Text;
using CSharpFunctionalExtensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using static DitzyExtensions.Testing.Generators.GenUtils;

namespace DitzyExtensions.Testing.Generators;

[Generator]
public class FsCheckOverloadsExtensionGenerator : IIncrementalGenerator {
	public void Initialize(IncrementalGeneratorInitializationContext context) {
		context.RegisterPostInitializationOutput(ctx => {
				ctx.AddSource("FsCheckOverloadsAttribute.g.cs", SourceText.From(FsCheckOverloadsAttribute, Encoding.UTF8));
			}
		);

		var sheetPatchData = context.SyntaxProvider
			.ForAttributeWithMetadataName(
				FsCheckOverloadsAttributeFqn,
				predicate: static (_, _) => true,
				transform: static (ctx, _) => GetAnnotationTargetData(ctx.SemanticModel, ctx.TargetNode)
			)
			.Where(static m => m is not null);

		context.RegisterSourceOutput(
			sheetPatchData,
			static (ctx, data) => GenerateExtensions(ctx, data)
		);
	}

	private static void GenerateExtensions(SourceProductionContext ctx, TargetExtensionData? data) {
		if (data is null) return;

		ctx.AddSource(
			$"{data.Value.ExtensionClassName}.g.cs",
			SourceText.From(GetExtensionClassText(data.Value), Encoding.UTF8)
		);
	}

	private static TargetExtensionData? GetAnnotationTargetData(SemanticModel semanticModel, SyntaxNode targetNode) {
		if (semanticModel.GetDeclaredSymbol(targetNode) is not INamedTypeSymbol classSymbol) {
			return null;
		}

		return new TargetExtensionData {
			ExtensionClassName = classSymbol.Name,
			ExtensionClassNamespace = classSymbol.ContainingNamespace.ToDisplayString(),
		};
	}
}
