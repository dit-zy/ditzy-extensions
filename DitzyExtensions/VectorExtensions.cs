using System.Numerics;
#if !NETSTANDARD2_0
using DitzyExtensions.Generators;
#endif

namespace DitzyExtensions {
#if !NETSTANDARD2_0
	[Swizzle(
		SourceClass = typeof(Vector2),
		ComponentsToSwizzle = new[] { nameof(Vector2.X), nameof(Vector2.Y) }
	)]
	[Swizzle(
		SourceClass = typeof(Vector3),
		TargetClass = typeof(Vector2),
		NumComponentsToChoose = 2,
		ComponentsToSwizzle = new[] { nameof(Vector3.X), nameof(Vector3.Y), nameof(Vector3.Z) }
	)]
	[Swizzle(
		SourceClass = typeof(Vector4),
		TargetClass = typeof(Vector2),
		NumComponentsToChoose = 2,
		ComponentsToSwizzle = new[] { nameof(Vector4.X), nameof(Vector4.Y), nameof(Vector4.Z), nameof(Vector4.W) }
	)]
	[Swizzle(
		SourceClass = typeof(Vector3),
		ComponentsToSwizzle = new[] { nameof(Vector3.X), nameof(Vector3.Y), nameof(Vector3.Z) }
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
#endif
	public static partial class VectorExtensions { }
}
