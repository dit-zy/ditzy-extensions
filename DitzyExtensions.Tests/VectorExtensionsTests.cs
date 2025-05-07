using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using CSharpFunctionalExtensions;
using DitzyExtensions.Collection;
using DitzyExtensions.Testing.FsCheck;
using DitzyExtensions.Tests.TestUtils;
using FluentAssertions;
using FluentAssertions.Execution;
using FsCheck;
using FsCheck.Xunit;
using JetBrains.Annotations;
using static DitzyExtensions.MathUtils;
using FCU = DitzyExtensions.Testing.FsCheck.FsCheckUtils;

namespace DitzyExtensions.Tests {
	[TestSubject(typeof(VectorExtensions))]
	public class VectorExtensionsTests {
		[Property]
		public Property Prop_Swizzle_V2xV2() => FCU.ForAll(
			Arbs.Vector2(),
			v => {
				using (new AssertionScope()) {
					GetSwizzleMethods<Vector2, Vector2>()
						.ForEach(method =>
							GetActualVector2(method, v).Should().Equal(
								GetExpectedSwizzleVector2(method.Name, v),
								because: method.Name
							)
						);
				}
			}
		);

		[Property]
		public Property Prop_Swizzle_V3xV2() => FCU.ForAll(
			Arbs.Vector3(),
			v => {
				using (new AssertionScope()) {
					GetSwizzleMethods<Vector3, Vector2>()
						.ForEach(method =>
							GetActualVector2(method, v).Should().Equal(
								GetExpectedSwizzleVector2(method.Name, v),
								because: method.Name
							)
						);
				}
			}
		);

		[Property]
		public Property Prop_Swizzle_V3xV3() => FCU.ForAll(
			Arbs.Vector3(),
			v => {
				using (new AssertionScope()) {
					GetSwizzleMethods<Vector3, Vector3>()
						.ForEach(method =>
							GetActualVector3(method, v).Should().Equal(
								GetExpectedSwizzleVector3(method.Name, v),
								because: method.Name
							)
						);
				}
			}
		);

		[Property]
		public Property Prop_Swizzle_V4xV2() => FCU.ForAll(
			Arbs.Vector4(),
			v => {
				using (new AssertionScope()) {
					GetSwizzleMethods<Vector4, Vector2>()
						.ForEach(method =>
							GetActualVector2(method, v).Should().Equal(
								GetExpectedSwizzleVector2(method.Name, v),
								because: method.Name
							)
						);
				}
			}
		);

		[Property]
		public Property Prop_Swizzle_V4xV3() => FCU.ForAll(
			Arbs.Vector4(),
			v => {
				using (new AssertionScope()) {
					GetSwizzleMethods<Vector4, Vector3>()
						.ForEach(method =>
							GetActualVector3(method, v).Should().Equal(
								GetExpectedSwizzleVector3(method.Name, v),
								because: method.Name
							)
						);
				}
			}
		);

		[Property]
		public Property Prop_Swizzle_V4xV4() => FCU.ForAll(
			Arbs.Vector4(),
			v => {
				using (new AssertionScope()) {
					GetSwizzleMethods<Vector4, Vector4>()
						.ForEach(method =>
							GetActualVector4(method, v).Should().Equal(
								GetExpectedSwizzleVector4(method.Name, v),
								because: method.Name
							)
						);
				}
			}
		);

		private Vector2 GetActualVector2(MethodInfo method, object vec) =>
			(Vector2)method.Invoke(null, new[] { vec });

		private Vector3 GetActualVector3(MethodInfo method, object vec) =>
			(Vector3)method.Invoke(null, new[] { vec });

		private Vector4 GetActualVector4(MethodInfo method, object vec) =>
			(Vector4)method.Invoke(null, new[] { vec });

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
