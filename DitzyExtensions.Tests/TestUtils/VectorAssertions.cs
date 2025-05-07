using System;
using System.IO;
using System.Numerics;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace DitzyExtensions.Tests.TestUtils {
	public abstract class VectorAssertions<S, A> : ReferenceTypeAssertions<S, VectorAssertions<S, A>>
		where A : VectorAssertions<S, A> {
		private readonly AssertionChain _chain;

		public VectorAssertions(S instance, AssertionChain chain) : base(instance, chain) {
			_chain = chain;
		}

		protected virtual float X => throw new ArgumentException($"{typeof(S)} has no X component.");
		protected virtual float Y => throw new ArgumentException($"{typeof(S)} has no Y component.");
		protected virtual float Z => throw new ArgumentException($"{typeof(S)} has no Z component.");
		protected virtual float W => throw new ArgumentException($"{typeof(S)} has no W component.");

		protected virtual float GetMagnitudeSq(S vector) => throw new NotImplementedException();
		protected virtual float GetDiffMagnitudeSq(S other) => throw new NotImplementedException();

		[CustomAssertion]
		public AndConstraint<VectorAssertions<S, A>> HaveX(
			float x,
			float toleranceFactor = 0,
			string because = "",
			params object[] becauseArgs
		) =>
			HaveComponent("X", X, x, toleranceFactor, because, becauseArgs);

		[CustomAssertion]
		public AndConstraint<VectorAssertions<S, A>> HaveY(
			float y,
			float toleranceFactor = 0,
			string because = "",
			params object[] becauseArgs
		) =>
			HaveComponent("Y", Y, y, toleranceFactor, because, becauseArgs);

		[CustomAssertion]
		public AndConstraint<VectorAssertions<S, A>> HaveZ(
			float z,
			float toleranceFactor = 0,
			string because = "",
			params object[] becauseArgs
		) =>
			HaveComponent("Z", Z, z, toleranceFactor, because, becauseArgs);

		[CustomAssertion]
		public AndConstraint<VectorAssertions<S, A>> HaveW(
			float w,
			float toleranceFactor = 0,
			string because = "",
			params object[] becauseArgs
		) =>
			HaveComponent("W", W, w, toleranceFactor, because, becauseArgs);

		private AndConstraint<VectorAssertions<S, A>> HaveComponent(
			string componentName,
			float componentValue,
			float expectedValue,
			float toleranceFactor,
			string because,
			object[] becauseArgs
		) {
			var xTolerance = Math.Abs(expectedValue * toleranceFactor);
			var xCond = Math.Abs(componentValue - expectedValue) <= xTolerance;
			_chain
				.BecauseOf(because, becauseArgs)
				.ForCondition(xCond)
				.WithDefaultIdentifier(Identifier)
				.FailWith(
					"Expected {context} to have an {2} component of {0}{reason}, but found {1}.",
					expectedValue,
					componentValue,
					componentName
				);

			return new AndConstraint<VectorAssertions<S, A>>(this);
		}

		[CustomAssertion]
		public AndConstraint<VectorAssertions<S, A>> Equal(
			S expected,
			float toleranceFactor = 0,
			string because = "",
			params object[] becauseArgs
		) {
			var tolerance = GetMagnitudeSq(expected) * toleranceFactor;
			var diff = GetDiffMagnitudeSq(expected);
			_chain
				.BecauseOf(because, becauseArgs)
				.ForCondition(diff <= tolerance)
				.WithDefaultIdentifier(Identifier)
				.FailWith("Expected {context} to equal {0}{reason}, but found {1}.", expected, Subject);

			return new AndConstraint<VectorAssertions<S, A>>(this);
		}
	}

	public class Vector2Assertions : VectorAssertions<Vector2, Vector2Assertions> {
		public Vector2Assertions(Vector2 instance, AssertionChain chain) : base(instance, chain) { }
		protected override string Identifier => "vector2";
		protected override float X => Subject.X;
		protected override float Y => Subject.Y;
		protected override float GetMagnitudeSq(Vector2 vector) => vector.LengthSquared();
		protected override float GetDiffMagnitudeSq(Vector2 other) => (Subject - other).LengthSquared();
	}

	public class Vector3Assertions : VectorAssertions<Vector3, Vector3Assertions> {
		public Vector3Assertions(Vector3 instance, AssertionChain chain) : base(instance, chain) { }
		protected override string Identifier => "vector3";
		protected override float X => Subject.X;
		protected override float Y => Subject.Y;
		protected override float GetMagnitudeSq(Vector3 vector) => vector.LengthSquared();
		protected override float GetDiffMagnitudeSq(Vector3 other) => (Subject - other).LengthSquared();
	}

	public class Vector4Assertions : VectorAssertions<Vector4, Vector4Assertions> {
		public Vector4Assertions(Vector4 instance, AssertionChain chain) : base(instance, chain) { }
		protected override string Identifier => "vector4";
		protected override float X => Subject.X;
		protected override float Y => Subject.Y;
		protected override float GetMagnitudeSq(Vector4 vector) => vector.LengthSquared();
		protected override float GetDiffMagnitudeSq(Vector4 other) => (Subject - other).LengthSquared();
	}
}
