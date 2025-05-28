using System.Collections.Generic;
using System.Linq;
using DitzyExtensions.Collection;
using DitzyExtensions.Testing.Assertions.Collections;
using DitzyExtensions.Testing.FsCheck;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using JetBrains.Annotations;
using Xunit;
using FCU = DitzyExtensions.Testing.FsCheck.FsCheckUtils;

namespace DitzyExtensions.Tests.Collections {
	[TestSubject(typeof(TupleExtensions))]
	public class TupleExtensionsTests {
		#region select element

		[Property]
		public Property SelectFirst_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.a);
				ls.SelectFirst().Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectFirst_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.a);
				ls.SelectFirst().Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectFirst_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.a);
				ls.SelectFirst().Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectSecond_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.b);
				ls.SelectSecond().Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectSecond_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.b);
				ls.SelectSecond().Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectSecond_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.b);
				ls.SelectSecond().Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectThird_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.c);
				ls.SelectThird().Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectThird_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.c);
				ls.SelectThird().Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectFourth_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.d);
				ls.SelectFourth().Should().BeEquivalentTo(expected);
			}
		);

		#endregion

		#region select element with transform

		[Property]
		public Property SelectFirst_Transform_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.a).Select(TestTransform);
				ls.SelectFirst(TestTransform).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectFirst_Transform_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.a).Select(TestTransform);
				ls.SelectFirst(TestTransform).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectFirst_Transform_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.a).Select(TestTransform);
				ls.SelectFirst(TestTransform).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectSecond_Transform_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.b).Select(TestTransform);
				ls.SelectSecond(TestTransform).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectSecond_Transform_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.b).Select(TestTransform);
				ls.SelectSecond(TestTransform).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectSecond_Transform_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.b).Select(TestTransform);
				ls.SelectSecond(TestTransform).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectThird_Transform_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.c).Select(TestTransform);
				ls.SelectThird(TestTransform).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectThird_Transform_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.c).Select(TestTransform);
				ls.SelectThird(TestTransform).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectFourth_Transform_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expected = ls.Select(pair => pair.d).Select(TestTransform);
				ls.SelectFourth(TestTransform).Should().BeEquivalentTo(expected);
			}
		);

		#endregion

		#region zipwith

		[Property]
		public Property ZipWith_2() => FCU.ForAll(
			Arbs.Int().ListOf(),
			Arbs.Int().ListOf(),
			(ts, us) => {
				var expected = new List<(int, int)>();
				for (var i = 0; i < ts.Count && i < us.Count; i++) {
					expected.Add((ts[i], us[i]));
				}
				ts.ZipWith(us).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property ZipWith_3() => FCU.ForAll(
			Arbs.Int().ListOf(),
			Arbs.Int().ListOf(),
			Arbs.Int().ListOf(),
			(ts, us, vs) => {
				var expected = new List<(int, int, int)>();
				for (var i = 0; i < ts.Count && i < us.Count && i < vs.Count; i++) {
					expected.Add((ts[i], us[i], vs[i]));
				}
				ts.ZipWith(us, vs).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property ZipWith_4() => FCU.ForAll(
			Arbs.Int().ListOf(),
			Arbs.Int().ListOf(),
			Arbs.Int().ListOf(),
			Arbs.Int().ListOf(),
			(ts, us, vs, ws) => {
				var expected = new List<(int, int, int, int)>();
				for (var i = 0; i < ts.Count && i < us.Count && i < vs.Count && i < ws.Count; i++) {
					expected.Add((ts[i], us[i], vs[i], ws[i]));
				}
				ts.ZipWith(us, vs, ws).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property ZipWith_Transform_2() => FCU.ForAll(
			Arbs.Int().ListOf(),
			Arbs.Int().ListOf(),
			(ts, us) => {
				var expected = new List<int>();
				for (var i = 0; i < ts.Count && i < us.Count; i++) {
					expected.Add(TestTransform2(ts[i], us[i]));
				}
				ts.ZipWith(us, TestTransform2).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property ZipWith_Transform_3() => FCU.ForAll(
			Arbs.Int().ListOf(),
			Arbs.Int().ListOf(),
			Arbs.Int().ListOf(),
			(ts, us, vs) => {
				var expected = new List<int>();
				for (var i = 0; i < ts.Count && i < us.Count && i < vs.Count; i++) {
					expected.Add(TestTransform3(ts[i], us[i], vs[i]));
				}
				ts.ZipWith(us, vs, TestTransform3).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property ZipWith_Transform_4() => FCU.ForAll(
			Arbs.Int().ListOf(),
			Arbs.Int().ListOf(),
			Arbs.Int().ListOf(),
			Arbs.Int().ListOf(),
			(ts, us, vs, ws) => {
				var expected = new List<int>();
				for (var i = 0; i < ts.Count && i < us.Count && i < vs.Count && i < ws.Count; i++) {
					expected.Add(TestTransform4(ts[i], us[i], vs[i], ws[i]));
				}
				ts.ZipWith(us, vs, ws, TestTransform4).Should().BeEquivalentTo(expected);
			}
		);

		#endregion

		#region unzip

		[Property]
		public Property Unzip_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var ts = new List<int>();
				var us = new List<int>();
				for (var i = 0; i < ls.Count; i++) {
					ts.Add(ls[i].a);
					us.Add(ls[i].b);
				}
				ls.Unzip().Should().BeEquivalentTo((ts, us));
			}
		);

		[Property]
		public Property Unzip_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var ts = new List<int>();
				var us = new List<int>();
				var vs = new List<int>();
				for (var i = 0; i < ls.Count; i++) {
					ts.Add(ls[i].a);
					us.Add(ls[i].b);
					vs.Add(ls[i].c);
				}
				ls.Unzip().Should().BeEquivalentTo((ts, us, vs));
			}
		);

		[Property]
		public Property Unzip_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var ts = new List<int>();
				var us = new List<int>();
				var vs = new List<int>();
				var ws = new List<int>();
				for (var i = 0; i < ls.Count; i++) {
					ts.Add(ls[i].a);
					us.Add(ls[i].b);
					vs.Add(ls[i].c);
					ws.Add(ls[i].d);
				}
				ls.Unzip().Should().BeEquivalentTo((ts, us, vs, ws));
			}
		);

		[Property]
		public Property Unzip_Transform_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var ts = new List<int>();
				var us = new List<int>();
				for (var i = 0; i < ls.Count; i++) {
					ts.Add(ls[i].a);
					us.Add(ls[i].b);
				}
				ls.Unzip(TestTransform2List).Should().Be(TestTransform2List(ts, us));
			}
		);

		[Property]
		public Property Unzip_Transform_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var ts = new List<int>();
				var us = new List<int>();
				var vs = new List<int>();
				for (var i = 0; i < ls.Count; i++) {
					ts.Add(ls[i].a);
					us.Add(ls[i].b);
					vs.Add(ls[i].c);
				}
				ls.Unzip(TestTransform3List).Should().Be(TestTransform3List(ts, us, vs));
			}
		);

		[Property]
		public Property Unzip_Transform_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var ts = new List<int>();
				var us = new List<int>();
				var vs = new List<int>();
				var ws = new List<int>();
				for (var i = 0; i < ls.Count; i++) {
					ts.Add(ls[i].a);
					us.Add(ls[i].b);
					vs.Add(ls[i].c);
					ws.Add(ls[i].d);
				}
				ls.Unzip(TestTransform4List).Should().Be(TestTransform4List(ts, us, vs, ws));
			}
		);

		#endregion

		// [Property]
		public Property AsPairs() => FCU.ForAll(
			Arbs.Int().DictWith(Arbs.Int()),
			dict => {
				var expected = dict.SelectEntries((k, v) => (k, v));
				dict.AsPairs().Should().ContainExactlyInAnyOrder(expected);
			}
		);

		#region test utils

		private static int TestTransform(int x) => x + 10;
		private static int TestTransform2(int x, int y) => x + y + 10;
		private static int TestTransform3(int x, int y, int z) => x + y + z + 10;
		private static int TestTransform4(int x, int y, int z, int w) => x + y + z + w + 10;

		private static int TestTransform2List(
			IEnumerable<int> xs,
			IEnumerable<int> ys
		) => xs.Sum() + ys.Sum() + 10;

		private static int TestTransform3List(
			IEnumerable<int> xs,
			IEnumerable<int> ys,
			IEnumerable<int> zs
		) => xs.Sum() + ys.Sum() + zs.Sum() + 10;

		private static int TestTransform4List(
			IEnumerable<int> xs,
			IEnumerable<int> ys,
			IEnumerable<int> zs,
			IEnumerable<int> ws
		) => xs.Sum() + ys.Sum() + zs.Sum() + ws.Sum() + 10;

		#endregion
	}
}
