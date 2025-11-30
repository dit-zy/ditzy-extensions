/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

using System;
using DitzyExtensions.Testing.Generators;
using FsCheck;
using FsCheck.Fluent;

namespace DitzyExtensions.Testing.FsCheck {

	[FsCheckOverloads]
	public static partial class FsCheckUtils {
		public static Gen<A> Zip<A>(Gen<A> a) => a;

		public static Gen<(A a, B b)> Zip<A, B>(Gen<A> a, Gen<B> b) =>
			Zip(a).SelectMany(_ => b, (x, y) => (x, y));

		public static Arbitrary<A> Zip<A>(Arbitrary<A> a) => a;

		public static Property ForAll<A>(
			Arbitrary<A> a,
			Action<A> body
		) => Prop.ForAll(a, body.Invoke);

		public static void Replay(this Property prop, ulong seed, ulong gamma) {
			var config = Config.Default.WithReplay(seed, gamma);
			prop.Check(config);
		}
	}
}