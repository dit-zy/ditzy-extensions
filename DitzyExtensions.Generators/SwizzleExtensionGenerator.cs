/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

using System.Text;
using CSharpFunctionalExtensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using static DitzyExtensions.Generators.GenUtils;

namespace DitzyExtensions.Generators;

[Generator]
public class SwizzleExtensionGenerator : IIncrementalGenerator {
	public void Initialize(IncrementalGeneratorInitializationContext context) {
		context.RegisterPostInitializationOutput(ctx => {
				ctx.AddSource("SwizzleAttribute.g.cs", SourceText.From(Swizzle, Encoding.UTF8));
			}
		);

		var sheetPatchData = context.SyntaxProvider
			.ForAttributeWithMetadataName(
				SwizzleAttributeFqn,
				predicate: static (_, _) => true,
				transform: static (ctx, _) => GetSwizzleData(ctx.SemanticModel, ctx.TargetNode)
			)
			.Where(static m => m is not null);

		context.RegisterSourceOutput(
			sheetPatchData,
			static (ctx, data) => GenerateSwizzleExtension(ctx, data)
		);
	}

	private static void GenerateSwizzleExtension(SourceProductionContext ctx, TargetExtensionData? data) {
		if (data is null) return;

		ctx.AddSource(
			$"{data.Value.ExtensionClassName}.g.cs",
			SourceText.From(GetExtensionClassText(data.Value), Encoding.UTF8)
		);
	}

	private static TargetExtensionData? GetSwizzleData(SemanticModel semanticModel, SyntaxNode targetNode) {
		if (semanticModel.GetDeclaredSymbol(targetNode) is not INamedTypeSymbol classSymbol) {
			return null;
		}

		var attributeType = semanticModel.Compilation.GetTypeByMetadataName(SwizzleAttributeFqn);
		if (attributeType is null) return null;

		var classSwizzleData = new List<SwizzleTargetData>();
		foreach (var attributeData in classSymbol.GetAttributes()) {
			if (!attributeType.Equals(attributeData.AttributeClass, SymbolEqualityComparer.Default)) {
				continue;
			}

			var swizzleFields = new List<string>();
			INamedTypeSymbol? sourceClass = null;
			INamedTypeSymbol? targetClass = null;
			var numToChoose = 0;

			var constructorArgs = attributeData.ConstructorArguments;
			for (var i = 0; i < constructorArgs.Length; i++) {
				var arg = constructorArgs[i];
				if (arg.Kind == TypedConstantKind.Error) return null;

				if (i == 0) {
					sourceClass = (arg.Value as INamedTypeSymbol)!;
				} else {
					if (arg.Kind == TypedConstantKind.Type) {
						targetClass = (arg.Value as INamedTypeSymbol)!;
					} else if (arg.Kind == TypedConstantKind.Primitive) {
						numToChoose = (int)(arg.Value as int?)!;
					} else if (arg.Kind == TypedConstantKind.Array) {
						swizzleFields.AddRange(arg.Values.Select(field => (field.Value as string)!));
					}
				}
			}

			foreach (var arg in attributeData.NamedArguments) {
				if (arg.Value.Kind == TypedConstantKind.Error) return null;

				switch (arg.Key) {
					case "SourceClass":
						sourceClass = (arg.Value.Value as INamedTypeSymbol)!;
						break;
					case "TargetClass":
						targetClass = (arg.Value.Value as INamedTypeSymbol)!;
						break;
					case "NumComponentsToChoose":
						numToChoose = (int)(arg.Value.Value as int?)!;
						break;
					case "ComponentsToSwizzle":
						swizzleFields.AddRange(arg.Value.Values.Select(field => (field.Value as string)!));
						break;
				}
			}

			if (sourceClass is null) return null;
			targetClass ??= sourceClass;
			if (numToChoose == 0) numToChoose = swizzleFields.Count;

			var swizzleTargetData = new SwizzleTargetData {
				SourceClassName = sourceClass.Name,
				SourceClassNamespace = sourceClass.ContainingNamespace.ToDisplayString(),
				TargetClassName = targetClass.Name,
				TargetClassNamespace = targetClass.ContainingNamespace.ToDisplayString(),
				NumFieldsToChoose = numToChoose,
				FieldsToSwizzle = new EqArray<SwizzleFieldData>(
					sourceClass
						.GetMembers()
						.Where(sym =>
							sym.Kind == SymbolKind.Property
							|| sym is { Kind: SymbolKind.Field, DeclaredAccessibility: Accessibility.Public }
						)
						.Select(sym => {
								return sym switch {
									IPropertySymbol propSym => new SwizzleFieldData {
										Name = propSym.Name,
										TypeName = propSym.Type.ToDisplayString(),
										TypeNamespace = propSym.ContainingNamespace.ToDisplayString(),
									},
									IFieldSymbol fieldSym => new SwizzleFieldData {
										Name = fieldSym.Name,
										TypeName = fieldSym.Type.ToDisplayString(),
										TypeNamespace = fieldSym.ContainingNamespace.ToDisplayString(),
									},
									_ => Maybe<SwizzleFieldData>.None
								};
							}
						)
						.Where(fieldData => fieldData.HasValue)
						.Select(fieldData => fieldData.Value)
						.Where(fieldData => swizzleFields.Contains(fieldData.Name))
				)
			};
			classSwizzleData.Add(swizzleTargetData);
		}

		return new TargetExtensionData {
			ExtensionClassName = classSymbol.Name,
			ExtensionClassNamespace = classSymbol.ContainingNamespace.ToDisplayString(),
			ClassSwizzleData = new EqArray<SwizzleTargetData>(classSwizzleData)
		};
	}
}
