using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using DitzyExtensions.Collection;
using DitzyExtensions.Functional;
using DitzyExtensions.Testing.FsCheck;
using FluentAssertions;
using FsCheck;
using FsCheck.Fluent;
using FsCheck.Xunit;
using JetBrains.Annotations;
using Xunit;
using static DitzyExtensions.MathUtils;
using FCU = DitzyExtensions.Testing.FsCheck.FsCheckUtils;

namespace DitzyExtensions.Tests {
	[TestSubject(typeof(MathUtils))]
	public class MathUtilsTests {
		#region vector constructors

		[Property]
		public Property Prop_V2_a() => FCU.ForAll(
			Arbs.Float(),
			x => V2(x).Should().Be(new Vector2(x, x))
		);

		[Property]
		public Property Prop_V2_b() => FCU.ForAll(
			Arbs.Vector2(),
			v => V2(v.X, v.Y).Should().Be(v)
		);

		[Property]
		public Property Prop_V3_a() => FCU.ForAll(
			Arbs.Float(),
			x => V3(x).Should().Be(new Vector3(x, x, x))
		);

		[Property]
		public Property Prop_V3_b() => FCU.ForAll(
			Arbs.Vector2(),
			Arbs.Float(),
			(v, z) => V3(v, z).Should().Be(new Vector3(v.X, v.Y, z))
		);

		[Property]
		public Property Prop_V3_c() => FCU.ForAll(
			Arbs.Vector3(),
			v => V3(v.X, v.Y, v.Z).Should().Be(v)
		);

		[Property]
		public Property Prop_V4_a() => FCU.ForAll(
			Arbs.Float(),
			x => V4(x).Should().Be(new Vector4(x, x, x, x))
		);

		[Property]
		public Property Prop_V4_b() => FCU.ForAll(
			Arbs.Vector2(),
			Arbs.Float(),
			Arbs.Float(),
			(v, z, w) => V4(v, z, w).Should().Be(new Vector4(v.X, v.Y, z, w))
		);

		[Property]
		public Property Prop_V4_c() => FCU.ForAll(
			Arbs.Vector2(),
			Arbs.Vector2(),
			(v, u) => V4(v, u).Should().Be(new Vector4(v.X, v.Y, u.X, u.Y))
		);

		[Property]
		public Property Prop_V4_d() => FCU.ForAll(
			Arbs.Vector3(),
			Arbs.Float(),
			(v, w) => V4(v, w).Should().Be(new Vector4(v.X, v.Y, v.Z, w))
		);

		[Property]
		public Property Prop_V4_e() => FCU.ForAll(
			Arbs.Vector4(),
			v => V4(v.X, v.Y, v.Z, v.W).Should().Be(v)
		);

		#endregion

		#region bit handling

		[Property]
		public Property Prop_GetBit_byte() => FCU.ForAll(
			Arbs.Byte(),
			n => 0.SequenceTo(8)
				.ForEach(i => n.GetBit(i).Should().Be((byte)((n >> i) & 1)))
		);

		[Property]
		public Property Prop_GetBit_short() => FCU.ForAll(
			Arbs.Short(),
			n => 0.SequenceTo(16)
				.ForEach(i => n.GetBit(i).Should().Be((short)((n >> i) & 1)))
		);

		[Property]
		public Property Prop_GetBit_ushort() => FCU.ForAll(
			Arbs.UShort(),
			n => 0.SequenceTo(16)
				.ForEach(i => n.GetBit(i).Should().Be((ushort)((n >> i) & 1)))
		);

		[Property]
		public Property Prop_GetBit_int() => FCU.ForAll(
			Arbs.Int(),
			n => 0.SequenceTo(32)
				.ForEach(i => n.GetBit(i).Should().Be((n >> i) & 1))
		);

		[Property]
		public Property Prop_GetBit_uint() => FCU.ForAll(
			Arbs.UInt(),
			n => 0.SequenceTo(32)
				.ForEach(i => n.GetBit(i).Should().Be((n >> i) & 1U))
		);

		[Property]
		public Property Prop_GetBit_long() => FCU.ForAll(
			Arbs.Long(),
			n => 0.SequenceTo(64)
				.ForEach(i => n.GetBit(i).Should().Be((n >> i) & 1L))
		);

		[Property]
		public Property Prop_GetBit_ulong() => FCU.ForAll(
			Arbs.ULong(),
			n => 0.SequenceTo(64)
				.ForEach(i => n.GetBit(i).Should().Be((n >> i) & 1UL))
		);

		[Property]
		public Property Prop_IsBitSet_byte() => FCU.ForAll(
			Arbs.Byte(),
			n => 0.SequenceTo(8)
				.ForEach(i => n.IsBitSet(i).Should().Be(((n >> i) & 1) == 1))
		);

		[Property]
		public Property Prop_IsBitSet_short() => FCU.ForAll(
			Arbs.Short(),
			n => 0.SequenceTo(16)
				.ForEach(i => n.IsBitSet(i).Should().Be(((n >> i) & 1) == 1))
		);

		[Property]
		public Property Prop_IsBitSet_ushort() => FCU.ForAll(
			Arbs.UShort(),
			n => 0.SequenceTo(16)
				.ForEach(i => n.IsBitSet(i).Should().Be(((n >> i) & 1) == 1))
		);

		[Property]
		public Property Prop_IsBitSet_int() => FCU.ForAll(
			Arbs.Int(),
			n => 0.SequenceTo(32)
				.ForEach(i => n.IsBitSet(i).Should().Be(((n >> i) & 1) == 1))
		);

		[Property]
		public Property Prop_IsBitSet_uint() => FCU.ForAll(
			Arbs.UInt(),
			n => 0.SequenceTo(32)
				.ForEach(i => n.IsBitSet(i).Should().Be(((n >> i) & 1U) == 1U))
		);

		[Property]
		public Property Prop_IsBitSet_long() => FCU.ForAll(
			Arbs.Long(),
			n => 0.SequenceTo(64)
				.ForEach(i => n.IsBitSet(i).Should().Be(((n >> i) & 1L) == 1L))
		);

		[Property]
		public Property Prop_IsBitSet_ulong() => FCU.ForAll(
			Arbs.ULong(),
			n => 0.SequenceTo(64)
				.ForEach(i => n.IsBitSet(i).Should().Be(((n >> i) & 1UL) == 1UL))
		);

		#endregion

		#region floats

		[Property]
		public Property Prop_Clamp01_float() => FCU.ForAll(
			Arbs.Float(),
			f => {
				var actual = f.Clamp01();
				if (actual < 0) {
					actual.Should().Be(0);
				} else if (1 < actual) {
					actual.Should().Be(1);
				} else {
					actual.Should().Be(actual);
				}
			}
		);

		[Property]
		public Property Prop_Clamp01_double() => FCU.ForAll(
			Arbs.Double(),
			d => {
				var actual = d.Clamp01();
				if (actual < 0) {
					actual.Should().Be(0);
				} else if (1 < actual) {
					actual.Should().Be(1);
				} else {
					actual.Should().Be(actual);
				}
			}
		);

		[Property]
		public Property Prop_Frac_float() => FCU.ForAll(
			Arbs.Float(),
			f => f.Frac().Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(1)
		);

		[Property]
		public Property Prop_Frac_double() => FCU.ForAll(
			Arbs.Double(),
			d => d.Frac().Should().BeGreaterThanOrEqualTo(0).And.BeLessThanOrEqualTo(1)
		);

		#endregion

		#region pure math

		[Fact]
		public void Fixed_Factorial() {
			var matches = new Dictionary<int, long> {
				{ -1, 1 },
				{ 0, 1 },
				{ 1, 1 },
				{ 2, 2 },
				{ 3, 6 },
				{ 5, 120 },
				{ 8, 40_320 },
				{ 12, 479_001_600 },
			};
			matches.ForEachEntry((n, fac) => n.Factorial().Should().Be(fac));
		}

		#endregion

		private const string ToStringChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ+/";

		[Property(Replay = "(17974118265581679621,15076827306960606809,4)")]
		public Property Prop_ToString_Radix_byte() =>
			Prop_ToString_Radix(byte.MaxValue, (n, radix) => ((byte)n).ToString(radix), false);

		[Property]
		public Property Prop_ToString_Radix_short() =>
			Prop_ToString_Radix(short.MaxValue, (n, radix) => ((short)n).ToString(radix));

		[Property]
		public Property Prop_ToString_Radix_int() =>
			Prop_ToString_Radix(int.MaxValue, (n, radix) => ((int)n).ToString(radix));

		[Property]
		public Property Prop_ToString_Radix_long() =>
			Prop_ToString_Radix(long.MaxValue, (n, radix) => n.ToString(radix));

		public Property Prop_ToString_Radix(double typeMax, Func<long, int, string> stringToTest, bool allowNegative = true) => FCU.ForAll(
			ToString_Radix_Arb(),
			ArbMap.Default.ArbFor<bool>(),
			(numStr, negative) => {
				// GIVEN
				var radix = numStr.n;
				var maxStrLen = (int)Math.Log(typeMax, radix);
				var str = numStr.str.Substring(0, Math.Min(maxStrLen, numStr.str.Length));
				var num = str.Reduce(
					(n, nextChar) => n * radix + ToStringChars.IndexOf(nextChar),
					0L
				);
				if (allowNegative && negative) {
					num = -num;
					str = "-" + str;
				}

				// WHEN
				var numToString = stringToTest(num, radix);

				// THEN
				numToString.Should().Be(str);
			}
		);

		private static Arbitrary<(int n, string str)> ToString_Radix_Arb() {
			return Gen.Choose(2, 64)
				.Select(n => (n, ToStringChars.Substring(0, n).ToCharArray()))
				.SelectMany(p =>
					Gen.Elements(p.Item2)
						.NonEmptyListOf()
						.Select(s => (p.n, s: s.Join(null).TrimStart('0')))
						.Where(q => 0 < q.s.Length)
				)
				.ToArbitrary();
		}
	}
}
