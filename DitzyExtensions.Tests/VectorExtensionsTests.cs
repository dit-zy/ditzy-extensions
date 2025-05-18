using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using CSharpFunctionalExtensions;
using DitzyExtensions.Collection;
using DitzyExtensions.Testing.Assertions;
using DitzyExtensions.Testing.FsCheck;
using DitzyExtensions.Tests.TestUtils;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using JetBrains.Annotations;
using Xunit.Abstractions;
using static DitzyExtensions.MathUtils;
using static DitzyExtensions.Testing.Assertions.AssertionUtils;
using FCU = DitzyExtensions.Testing.FsCheck.FsCheckUtils;

namespace DitzyExtensions.Tests {
	[TestSubject(typeof(VectorExtensions))]
	public class VectorExtensionsTests {
		private readonly ITestOutputHelper _output;

		public VectorExtensionsTests(ITestOutputHelper output) {
			_output = output;
		}

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8605 // Unboxing a possibly null value.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
		[Property]
		public Property Prop_Swizzle_V2xV2() =>
			SwizzleTest(Arbs.Vector2(), GetActualVector2, GetExpectedSwizzleVector2);

		[Property]
		public Property Prop_Swizzle_V2xV3() =>
			SwizzleTest(Arbs.Vector2(), GetActualVector3, GetExpectedSwizzleVector3);

		[Property]
		public Property Prop_Swizzle_V2xV4() =>
			SwizzleTest(Arbs.Vector2(), GetActualVector4, GetExpectedSwizzleVector4);

		[Property]
		public Property Prop_Swizzle_V3xV2() =>
			SwizzleTest(Arbs.Vector3(), GetActualVector2, GetExpectedSwizzleVector2);

		[Property]
		public Property Prop_Swizzle_V3xV3() =>
			SwizzleTest(Arbs.Vector3(), GetActualVector3, GetExpectedSwizzleVector3);

		[Property]
		public Property Prop_Swizzle_V3xV4() =>
			SwizzleTest(Arbs.Vector3(), GetActualVector4, GetExpectedSwizzleVector4);

		[Property]
		public Property Prop_Swizzle_V4xV2() =>
			SwizzleTest(Arbs.Vector4(), GetActualVector2, GetExpectedSwizzleVector2);

		[Property]
		public Property Prop_Swizzle_V4xV3() =>
			SwizzleTest(Arbs.Vector4(), GetActualVector3, GetExpectedSwizzleVector3);

		[Property]
		public Property Prop_Swizzle_V4xV4() =>
			SwizzleTest(Arbs.Vector4(), GetActualVector4, GetExpectedSwizzleVector4);

		private Property SwizzleTest<From, To>(
			Arbitrary<From> arb,
			Func<MethodInfo, object, To> getActual,
			Func<string, From, To> getExpected
		) {
			var methods = GetSwizzleMethods<From, To>();
			_output.WriteLine("testing {0} methods", methods.Count);
			return FCU.ForAll(
				arb,
				v => WithAssertionScope(() =>
					methods.ForEach(method => {
							var actual = getActual(method, v);
							var expected = getExpected(method.Name, v);
							if (typeof(To) == typeof(Vector2)) {
								((Vector2)(object)actual).Should().Equal((Vector2)(object)expected, because: method.Name);
							} else if (typeof(To) == typeof(Vector3)) {
								((Vector3)(object)actual).Should().Equal((Vector3)(object)expected, because: method.Name);
							} else {
								((Vector4)(object)actual).Should().Equal((Vector4)(object)expected, because: method.Name);
							}
						}
					)
				)
			);
		}

		private Vector2 GetActualVector2(MethodInfo method, object vec) =>
			(Vector2)method.Invoke(null, new[] { vec });

		private Vector3 GetActualVector3(MethodInfo method, object vec) =>
			(Vector3)method.Invoke(null, new[] { vec });

		private Vector4 GetActualVector4(MethodInfo method, object vec) =>
			(Vector4)method.Invoke(null, new[] { vec });

#pragma warning restore CS8600
#pragma warning restore CS8605
#pragma warning restore CS8604

		private Vector2 GetExpectedSwizzleVector2(string methodName, Vector2 vec) =>
			GetExpectedSwizzleVector2(methodName, V4(vec.X, vec.Y, 0, 0));

		private Vector2 GetExpectedSwizzleVector2(string methodName, Vector3 vec) =>
			GetExpectedSwizzleVector2(methodName, V4(vec.X, vec.Y, vec.Z, 0));

		private Vector2 GetExpectedSwizzleVector2(string methodName, Vector4 vec) {
			var v = GetExpectedSwizzleVector4(methodName, vec);
			return V2(v.X, v.Y);
		}

		private Vector3 GetExpectedSwizzleVector3(string methodName, Vector2 vec) =>
			GetExpectedSwizzleVector3(methodName, V4(vec.X, vec.Y, 0, 0));

		private Vector3 GetExpectedSwizzleVector3(string methodName, Vector3 vec) =>
			GetExpectedSwizzleVector3(methodName, V4(vec.X, vec.Y, vec.Z, 0));

		private Vector3 GetExpectedSwizzleVector3(string methodName, Vector4 vec) {
			var v = GetExpectedSwizzleVector4(methodName, vec);
			return V3(v.X, v.Y, v.Z);
		}

		private Vector4 GetExpectedSwizzleVector4(string methodName, Vector2 vec) =>
			GetExpectedSwizzleVector4(methodName, V4(vec.X, vec.Y, 0, 0));

		private Vector4 GetExpectedSwizzleVector4(string methodName, Vector3 vec) =>
			GetExpectedSwizzleVector4(methodName, V4(vec.X, vec.Y, vec.Z, 0));

		private Vector4 GetExpectedSwizzleVector4(string methodName, Vector4 vec) {
			var components = methodName
				.Select(c => {
						switch (c) {
							case 'X':
								return vec.X;
							case 'Y':
								return vec.Y;
							case 'Z':
								return vec.Z;
							case 'W':
								return vec.W;
							default:
								throw new ArgumentException($"invalid swizzle component [{c}] for method: {methodName}");
						}
					}
				)
				.Concat(new float[] { 0, 0, 0, 0 })
				.AsList();
			return V4(components[0], components[1], components[2], components[3]);
		}

		private IList<MethodInfo> GetSwizzleMethods<From, To>() =>
			typeof(VectorExtensions)
				.Methods()
				.ThatAreStatic()
				.ThatReturn<To>()
				.Where(method =>
					method
						.GetParameters()
						.TryFirst()
						.Map(param => param.ParameterType == typeof(From))
						.GetValueOrDefault(false)
				)
				.AsList();
	}
}
