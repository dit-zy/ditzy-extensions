/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

using System.Collections.Generic;
using System.Linq;
using DitzyExtensions.Collection;
using DitzyExtensions.Testing.Assertions.Collections;
using DitzyExtensions.Testing.FsCheck;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using JetBrains.Annotations;
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

		#region select element with transform and index

		[Property]
		public Property SelectFirst_Transform_Index_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var expected = ls.Select((pair, i) => TestTransform2(pair.a, i));
				ls.SelectFirst(TestTransform2).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectFirst_Transform_Index_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expected = ls.Select((pair, i) => TestTransform2(pair.a, i));
				ls.SelectFirst(TestTransform2).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectFirst_Transform_Index_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expected = ls.Select((pair, i) => TestTransform2(pair.a, i));
				ls.SelectFirst(TestTransform2).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectSecond_Transform_Index_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var expected = ls.Select((pair, i) => TestTransform2(pair.b, i));
				ls.SelectSecond(TestTransform2).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectSecond_Transform_Index_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expected = ls.Select((pair, i) => TestTransform2(pair.b, i));
				ls.SelectSecond(TestTransform2).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectSecond_Transform_Index_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expected = ls.Select((pair, i) => TestTransform2(pair.b, i));
				ls.SelectSecond(TestTransform2).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectThird_Transform_Index_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expected = ls.Select((pair, i) => TestTransform2(pair.c, i));
				ls.SelectThird(TestTransform2).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectThird_Transform_Index_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expected = ls.Select((pair, i) => TestTransform2(pair.c, i));
				ls.SelectThird(TestTransform2).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectFourth_Transform_Index_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expected = ls.Select((pair, i) => TestTransform2(pair.d, i));
				ls.SelectFourth(TestTransform2).Should().BeEquivalentTo(expected);
			}
		);

		#endregion

		#region select entries

		[Property]
		public Property SelectEntries_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var expected = ls.Select(pair => TestTransform2(pair.a, pair.b));
				ls.SelectEntries(TestTransform2).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectEntries_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expected = ls.Select(pair => TestTransform3(pair.a, pair.b, pair.c));
				ls.SelectEntries(TestTransform3).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectEntries_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expected = ls.Select(pair => TestTransform4(pair.a, pair.b, pair.c, pair.d));
				ls.SelectEntries(TestTransform4).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectEntries_Index_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var expected = ls.Select((pair, i) => TestTransform3(pair.a, pair.b, i));
				ls.SelectEntries(TestTransform3).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectEntries_Index_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expected = ls.Select((pair, i) => TestTransform4(pair.a, pair.b, pair.c, i));
				ls.SelectEntries(TestTransform4).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property SelectEntries_Index_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expected = ls.Select((pair, i) => TestTransform5(pair.a, pair.b, pair.c, pair.d, i));
				ls.SelectEntries(TestTransform5).Should().BeEquivalentTo(expected);
			}
		);

		#endregion
		
		#region where element

		[Property]
		public Property WhereFirst_2() => FCU.ForAll(
			Arbs.Float().Choose2().ListOf(),
			ls => {
				var expected = ls.Where(pair => TestWhere(pair.a));
				ls.WhereFirst(TestWhere).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereFirst_3() => FCU.ForAll(
			Arbs.Float().Choose3().ListOf(),
			ls => {
				var expected = ls.Where(pair => TestWhere(pair.a));
				ls.WhereFirst(TestWhere).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereFirst_4() => FCU.ForAll(
			Arbs.Float().Choose4().ListOf(),
			ls => {
				var expected = ls.Where(pair => TestWhere(pair.a));
				ls.WhereFirst(TestWhere).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereSecond_2() => FCU.ForAll(
			Arbs.Float().Choose2().ListOf(),
			ls => {
				var expected = ls.Where(pair => TestWhere(pair.b));
				ls.WhereSecond(TestWhere).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereSecond_3() => FCU.ForAll(
			Arbs.Float().Choose3().ListOf(),
			ls => {
				var expected = ls.Where(pair => TestWhere(pair.b));
				ls.WhereSecond(TestWhere).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereSecond_4() => FCU.ForAll(
			Arbs.Float().Choose4().ListOf(),
			ls => {
				var expected = ls.Where(pair => TestWhere(pair.b));
				ls.WhereSecond(TestWhere).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereThird_3() => FCU.ForAll(
			Arbs.Float().Choose3().ListOf(),
			ls => {
				var expected = ls.Where(pair => TestWhere(pair.c));
				ls.WhereThird(TestWhere).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereThird_4() => FCU.ForAll(
			Arbs.Float().Choose4().ListOf(),
			ls => {
				var expected = ls.Where(pair => TestWhere(pair.c));
				ls.WhereThird(TestWhere).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereFourth_4() => FCU.ForAll(
			Arbs.Float().Choose4().ListOf(),
			ls => {
				var expected = ls.Where(pair => TestWhere(pair.d));
				ls.WhereFourth(TestWhere).Should().BeEquivalentTo(expected);
			}
		);

		#endregion

		#region where element with index

		[Property]
		public Property WhereFirst_Index_2() => FCU.ForAll(
			Arbs.Float().Choose2().ListOf(),
			ls => {
				var expected = ls.Where((pair, i) => TestWhere2(pair.a, i));
				ls.WhereFirst(TestWhereIx).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereFirst_Index_3() => FCU.ForAll(
			Arbs.Float().Choose3().ListOf(),
			ls => {
				var expected = ls.Where((pair, i) => TestWhereIx(pair.a, i));
				ls.WhereFirst(TestWhereIx).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereFirst_Index_4() => FCU.ForAll(
			Arbs.Float().Choose4().ListOf(),
			ls => {
				var expected = ls.Where((pair, i) => TestWhereIx(pair.a, i));
				ls.WhereFirst(TestWhereIx).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereSecond_Index_2() => FCU.ForAll(
			Arbs.Float().Choose2().ListOf(),
			ls => {
				var expected = ls.Where((pair, i) => TestWhereIx(pair.b, i));
				ls.WhereSecond(TestWhereIx).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereSecond_Index_3() => FCU.ForAll(
			Arbs.Float().Choose3().ListOf(),
			ls => {
				var expected = ls.Where((pair, i) => TestWhereIx(pair.b, i));
				ls.WhereSecond(TestWhereIx).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereSecond_Index_4() => FCU.ForAll(
			Arbs.Float().Choose4().ListOf(),
			ls => {
				var expected = ls.Where((pair, i) => TestWhereIx(pair.b, i));
				ls.WhereSecond(TestWhereIx).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereThird_Index_3() => FCU.ForAll(
			Arbs.Float().Choose3().ListOf(),
			ls => {
				var expected = ls.Where((pair, i) => TestWhereIx(pair.c, i));
				ls.WhereThird(TestWhereIx).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereThird_Index_4() => FCU.ForAll(
			Arbs.Float().Choose4().ListOf(),
			ls => {
				var expected = ls.Where((pair, i) => TestWhereIx(pair.c, i));
				ls.WhereThird(TestWhereIx).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereFourth_Index_4() => FCU.ForAll(
			Arbs.Float().Choose4().ListOf(),
			ls => {
				var expected = ls.Where((pair, i) => TestWhereIx(pair.d, i));
				ls.WhereFourth(TestWhereIx).Should().BeEquivalentTo(expected);
			}
		);

		#endregion

		#region where entries

		[Property]
		public Property WhereEntries_2() => FCU.ForAll(
			Arbs.Float().Choose2().ListOf(),
			ls => {
				var expected = ls.Where(pair => TestWhere2(pair.a, pair.b));
				ls.WhereEntries(TestWhere2).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereEntries_3() => FCU.ForAll(
			Arbs.Float().Choose3().ListOf(),
			ls => {
				var expected = ls.Where(pair => TestWhere3(pair.a, pair.b, pair.c));
				ls.WhereEntries(TestWhere3).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereEntries_4() => FCU.ForAll(
			Arbs.Float().Choose4().ListOf(),
			ls => {
				var expected = ls.Where(pair => TestWhere4(pair.a, pair.b, pair.c, pair.d));
				ls.WhereEntries(TestWhere4).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereEntries_Index_2() => FCU.ForAll(
			Arbs.Float().Choose2().ListOf(),
			ls => {
				var expected = ls.Where((pair, i) => TestWhere2Ix(pair.a, pair.b, i));
				ls.WhereEntries(TestWhere2Ix).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereEntries_Index_3() => FCU.ForAll(
			Arbs.Float().Choose3().ListOf(),
			ls => {
				var expected = ls.Where((pair, i) => TestWhere3Ix(pair.a, pair.b, pair.c, i));
				ls.WhereEntries(TestWhere3Ix).Should().BeEquivalentTo(expected);
			}
		);

		[Property]
		public Property WhereEntries_Index_4() => FCU.ForAll(
			Arbs.Float().Choose4().ListOf(),
			ls => {
				var expected = ls.Where((pair, i) => TestWhere4Ix(pair.a, pair.b, pair.c, pair.d, i));
				ls.WhereEntries(TestWhere4Ix).Should().BeEquivalentTo(expected);
			}
		);

		#endregion

		#region foreach element

		[Property]
		public Property ForEachFirst_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var expectedSum = ls.Select(pair => pair.a).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachFirst(x => actualSum += x).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachFirst_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expectedSum = ls.Select(pair => pair.a).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachFirst(x => actualSum += x).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachFirst_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expectedSum = ls.Select(pair => pair.a).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachFirst(x => actualSum += x).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachSecond_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var expectedSum = ls.Select(pair => pair.b).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachSecond(x => actualSum += x).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachSecond_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expectedSum = ls.Select(pair => pair.b).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachSecond(x => actualSum += x).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachSecond_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expectedSum = ls.Select(pair => pair.b).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachSecond(x => actualSum += x).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachThird_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expectedSum = ls.Select(pair => pair.c).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachThird(x => actualSum += x).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachThird_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expectedSum = ls.Select(pair => pair.c).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachThird(x => actualSum += x).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachFourth_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expectedSum = ls.Select(pair => pair.d).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachFourth(x => actualSum += x).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		#endregion

		#region foreach element with index

		[Property]
		public Property ForEachFirst_Index_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var expectedSum = ls.Select((pair, i) => pair.a + i).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachFirst((x, i) => actualSum += x + i).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachFirst_Index_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expectedSum = ls.Select((pair, i) => pair.a + i).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachFirst((x, i) => actualSum += x + i).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachFirst_Index_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expectedSum = ls.Select((pair, i) => pair.a + i).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachFirst((x, i) => actualSum += x + i).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachSecond_Index_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var expectedSum = ls.Select((pair, i) => pair.b + i).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachSecond((x, i) => actualSum += x + i).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachSecond_Index_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expectedSum = ls.Select((pair, i) => pair.b + i).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachSecond((x, i) => actualSum += x + i).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachSecond_Index_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expectedSum = ls.Select((pair, i) => pair.b + i).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachSecond((x, i) => actualSum += x + i).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachThird_Index_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expectedSum = ls.Select((pair, i) => pair.c + i).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachThird((x, i) => actualSum += x + i).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachThird_Index_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expectedSum = ls.Select((pair, i) => pair.c + i).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachThird((x, i) => actualSum += x + i).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachFourth_Index_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expectedSum = ls.Select((pair, i) => pair.d + i).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachFourth((x, i) => actualSum += x + i).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		#endregion

		#region foreach entry

		[Property]
		public Property ForEachEntry_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var expectedSum = ls.Select(t => t.a + t.b).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachEntry((a, b) => actualSum += a + b).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachEntries_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expectedSum = ls.Select(t => t.a + t.b + t.c).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachEntry((a, b, c) => actualSum += a + b + c).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachEntries_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expectedSum = ls.Select(t => t.a + t.b + t.c + t.d).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachEntry((a, b, c, d) => actualSum += a + b + c + d).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachEntries_Index_2() => FCU.ForAll(
			Arbs.Int().Choose2().ListOf(),
			ls => {
				var expectedSum = ls.Select((t, i) => t.a + t.b + i).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachEntry((a, b, i) => actualSum += a + b + i).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachEntries_Index_3() => FCU.ForAll(
			Arbs.Int().Choose3().ListOf(),
			ls => {
				var expectedSum = ls.Select((t, i) => t.a + t.b + t.c + i).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachEntry((a, b, c, i) => actualSum += a + b + c + i).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
			}
		);

		[Property]
		public Property ForEachEntries_Index_4() => FCU.ForAll(
			Arbs.Int().Choose4().ListOf(),
			ls => {
				var expectedSum = ls.Select((t, i) => t.a + t.b + t.c + t.d + i).Sum() + 7f;
				var actualSum = 7f;
				ls.ForEachEntry((a, b, c, d, i) => actualSum += a + b + c + d + i).Should().BeEquivalentTo(ls);
				actualSum.Should().Be(expectedSum);
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
		private static int TestTransform5(int x, int y, int z, int w, int v) => x + y + z + w + v + 10;

		private static bool TestWhere(float x) => 10 < x + 10;
		private static bool TestWhere2(float x, float y) => 10 < x + y + 10;
		private static bool TestWhere3(float x, float y, float z) => 10 < x + y + z + 10;
		private static bool TestWhere4(float x, float y, float z, float w) => 10 < x + y + z + w + 10;

		private static bool TestWhereIx(float x, int i) => 10 < x + i + 10;
		private static bool TestWhere2Ix(float x, float y, int i) => 10 < x + y + i + 10;
		private static bool TestWhere3Ix(float x, float y, float z, int i) => 10 < x + y + z + i + 10;
		private static bool TestWhere4Ix(float x, float y, float z, float w, int i) => 10 < x + y + z + w + i + 10;

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
