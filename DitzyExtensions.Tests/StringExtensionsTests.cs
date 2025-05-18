using System;
using System.Text;
using DitzyExtensions.Testing.FsCheck;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using JetBrains.Annotations;
using Xunit;
using FCU = DitzyExtensions.Testing.FsCheck.FsCheckUtils;

namespace DitzyExtensions.Tests {
	[TestSubject(typeof(StringExtensions))]
	public class StringExtensionsTests {

		[Property]
		public Property Prop_AsLower() => FCU.ForAll(
			Arbs.String(),
			s => s.AsLower().Should().Be(s.ToLowerInvariant())
		);

		[Property]
		public Property Prop_AsUpper() => FCU.ForAll(
			Arbs.String(),
			s => s.AsUpper().Should().Be(s.ToUpperInvariant())
		);

		[Property]
		public Property Prop_Join() => FCU.ForAll(
			Arbs.String().ListOf(),
			Arbs.String(),
			(ls, sep) => {
				var expected = new StringBuilder();
				for (var i = 0; i < ls.Count; i++) {
					expected.Append(ls[i]);
					if (i != ls.Count - 1) expected.Append(sep);
				}
				
				ls.Join(sep).Should().Be(expected.ToString());
			}
		);

		[Fact]
		public void Fixed_AsLower() {
			"".AsLower().Should().Be("");
			"simple TEST PhraSe".AsLower().Should().Be("simple test phrase");
		}

		[Fact]
		public void Fixed_AsUpper() {
			"".AsLower().Should().Be("");
			"simple TEST PhraSe".AsUpper().Should().Be("SIMPLE TEST PHRASE");
		}

		[Fact]
		public void Fixed_Join() {
			Array.Empty<string>().Join(null).Should().Be("");
			Array.Empty<string>().Join("|-").Should().Be("");
			new[] {"one", "tWo", "OwO"}.Join(null).Should().Be("onetWoOwO");
			new[] {"one", "tWo", "OwO"}.Join("|-").Should().Be("one|-tWo|-OwO");
		}
	}
}
