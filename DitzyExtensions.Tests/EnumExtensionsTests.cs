using System;
using System.Linq;
using CSharpFunctionalExtensions;
using DitzyExtensions.Collection;
using DitzyExtensions.Testing.Functional.Result;
using FluentAssertions;
using FluentAssertions.Execution;
using JetBrains.Annotations;
using Xunit;
using static DitzyExtensions.EnumExtensions;

namespace DitzyExtensions.Tests {
	[TestSubject(typeof(EnumExtensions))]
	public class EnumExtensionsTests {
		[Fact]
		public void GetEnumValues() {
			GetEnumValues<TestEnum>().Should().Equal(new[] { TestEnum.A, TestEnum.B, TestEnum.C });
		}

		[Fact]
		public void AsEnum() {
			using (new AssertionScope()) {
				new[] { TestEnum.A, TestEnum.B, TestEnum.C }
					.ForEach(value => {
						var enumName = value.ToString();
						enumName.AsEnum<TestEnum>().Should().Be(value);
					});
			}
		}

		[Fact]
		public void AsEnum_InvalidName() {
			Action f = () => "NotARealValue".AsEnum<TestEnum>();
			f.Should()
				.ThrowExactly<ArgumentException>()
				.WithMessage("Enum 'NotARealValue' does not exist.");
		}

		[Fact]
		public void TryAsEnum() {
			using (new AssertionScope()) {
				new[] { TestEnum.A, TestEnum.B, TestEnum.C }
					.ForEach(value => {
						var enumName = value.ToString();
						enumName
							.TryAsEnum<TestEnum>()
							.Should()
							.BeSuccess(value);
					});
			}
		}

		[Fact]
		public void TryAsEnum_InvalidName() {
			"NotARealValue"
				.TryAsEnum<TestEnum>()
				.Should()
				.BeFailure("Enum 'NotARealValue' not found for type 'TestEnum'.", "");
		}

		[Fact]
		public void TryAsEnum_Out() {
			using (new AssertionScope()) {
				new[] { TestEnum.A, TestEnum.B, TestEnum.C }
					.ForEach(value => {
						var enumName = value.ToString();
						enumName.TryAsEnum<TestEnum>(out var actual).Should().BeTrue();
						actual.Should().Be(value);
					});
			}
		}

		[Fact]
		public void TryAsEnum_Out_InvalidName() {
			"NotARealValue".TryAsEnum<TestEnum>(out _).Should().BeFalse();
		}

		private enum TestEnum {
			A,
			B,
			C,
		}
	}
}
