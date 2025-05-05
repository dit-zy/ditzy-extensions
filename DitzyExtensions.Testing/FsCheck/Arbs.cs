using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using CSharpFunctionalExtensions;
using DitzyExtensions.Collection;
using FsCheck;
using FsCheck.Fluent;
using static DitzyExtensions.EnumExtensions;

namespace DitzyExtensions.Testing.FsCheck {
	public static class Arbs {
		public static Arbitrary<string> String() =>
			GenFor<UnicodeString>()
				.Select(s => s.ToString())
				.ToArbitrary();

		public static Arbitrary<string> NonEmptyString() =>
			String()
				.Generator
				.Where(s => !string.IsNullOrEmpty(s))
				.ToArbitrary();

		public static Arbitrary<float> Float() =>
			GenFor<NormalFloat>()
				.Select(f => (float)f.Get)
				.ToArbitrary();

		public static Arbitrary<double> Double() =>
			GenFor<double>()
				.Where(d => !double.IsNaN(d) && !double.IsInfinity(d))
				.ToArbitrary();

		public static Arbitrary<byte> Byte() => ArbFor<byte>();

		public static Arbitrary<short> Short() => ArbFor<short>();

		public static Arbitrary<ushort> UShort() => ArbFor<ushort>();

		public static Arbitrary<int> Int() => ArbFor<int>();

		public static Arbitrary<uint> UInt() => ArbFor<uint>();

		public static Arbitrary<long> Long() => ArbFor<long>();

		public static Arbitrary<ulong> ULong() => ArbFor<ulong>();

		public static Arbitrary<Vector2> Vector2() =>
			Float()
				.Choose2()
				.Select(vals => new Vector2(vals.a, vals.b))
				.ToArbitrary();

		public static Arbitrary<Vector3> Vector3() =>
			Float()
				.Choose3()
				.Select(vals => new Vector3(vals.a, vals.b, vals.c))
				.ToArbitrary();

		public static Arbitrary<Vector4> Vector4() =>
			Float()
				.Choose4()
				.Select(vals => new Vector4(vals.a, vals.b, vals.c, vals.d))
				.ToArbitrary();

#if !N48_S2
		public static Arbitrary<IDictionary<K, V>> DictOf<K, V>(Gen<K> keyGen, Gen<V> valueGen) where K : notnull =>
#else
		public static Arbitrary<IDictionary<K, V>> DictOf<K, V>(Gen<K> keyGen, Gen<V> valueGen) =>
#endif
			FsCheckUtils.Zip(keyGen, valueGen)
				.ListOf()
				.Select(entryList => entryList.AsDict())
				.ToArbitrary();

#if !N48_S2
		public static Arbitrary<T?> WithNulls<T>(Gen<T> gen) =>
			RandomFreq(
				gen.Select(value => (T?)value),
				Gen.Constant((T?)default)
			);
#else
		public static Arbitrary<T> WithNulls<T>(Gen<T> gen) =>
			RandomFreq(
				gen,
				Gen.Constant<T>(default)
			);
#endif

		public static Arbitrary<T> OfEnum<T>() where T : struct, Enum =>
			Gen.Elements(GetEnumValues<T>()).ToArbitrary();

#if !N48_S2
		public static Arbitrary<IDictionary<K, V>> EnumDict<K, V>() where K : struct, Enum where V : notnull {
#else
		public static Arbitrary<IDictionary<K, V>> EnumDict<K, V>() where K : struct, Enum {
#endif
			var enumValues = GetEnumValues<K>();
			return GenFor<V>()
				.ListOf(enumValues.Length)
				.Select(valueList => enumValues.Zip(valueList))
				.Select(entries => entries.AsDict())
				.ToArbitrary();
		}

#if !N48_S2
		public static Arbitrary<IDictionary<K, V>> PartialEnumDict<K, V>() where K : struct, Enum where V : notnull =>
#else
		public static Arbitrary<IDictionary<K, V>> PartialEnumDict<K, V>() where K : struct, Enum =>
#endif
			EnumDict<K, V>().Generator
				.Select(dict => dict.ToList())
				.SelectMany(Gen.Shuffle)
				.SelectMany(entries => Gen
					.Choose(1, entries.Length - 1)
					.Select(entries.Skip)
				)
				.Select(entries => entries.AsDict())
				.ToArbitrary();

		public static Arbitrary<Maybe<T>> MaybeArb<T>(Gen<T> gen, bool includeNulls = false) =>
#if !N48_S2
			(includeNulls ? WithNulls(gen).Generator : gen.Select(value => (T?)value))
#else
			(includeNulls ? WithNulls(gen).Generator : gen)
#endif
			.Select(value =>
					Maybe
						.From(value)
#if !N48_S2
						.Select(maybeValue => (T)maybeValue!)
#endif
			)
			.ToArbitrary();

		public static Arbitrary<T> RandomFreq<T>(params Gen<T>[] gens) =>
			Gen.Choose(0, 100)
				.ListOf(gens.Length)
				.SelectMany(freqs =>
					Gen.Choose(0, freqs.Count - 1)
						.Select(index => freqs.With((index, freqs[index] + 1)))
				)
				.SelectMany(freqs =>
#if !N48_S2
						Gen.Frequency(freqs.Zip(gens).Select(f => (f.First, f.Second)))
#else
						Gen.Frequency(freqs.Zip(gens).Select(f => (f.t, f.u)))
#endif
				)
				.ToArbitrary();

		public static Arbitrary<T> ArbFor<T>() => ArbMap.Default.ArbFor<T>();

		public static Gen<T> GenFor<T>() => ArbMap.Default.GeneratorFor<T>();
	}

	public static class ArbExtensions {
		public static Arbitrary<IList<T>> ListOf<T>(this Arbitrary<T> arb) =>
			arb.Generator
				.ListOf()
				.Select(ls => ls.AsList())
				.ToArbitrary();

		public static Arbitrary<IList<T>> NonEmptyListOf<T>(this Arbitrary<T> arb) =>
			arb.Generator
				.NonEmptyListOf()
				.Select(ls => ls.AsList())
				.ToArbitrary();

		public static Arbitrary<IList<T>> NonEmpty<T>(this Gen<IList<T>> gen) =>
			gen.Where(list => list.IsNotEmpty()).ToArbitrary();

		public static Arbitrary<IList<T>> NonEmpty<T>(this Arbitrary<IList<T>> arb) =>
			arb.Generator.Where(list => list.IsNotEmpty()).ToArbitrary();

		public static Arbitrary<IDictionary<K, V>> DictWith<K, V>(
			this Arbitrary<K> keyArb,
			Arbitrary<V> valueArb,
			params K[] keysToExclude
#if !N48_S2
		) where K : notnull =>
#else
		) =>
#endif
			Arbs.DictOf(keyArb.Generator, valueArb.Generator)
				.Excluding(keysToExclude);

		public static Arbitrary<IDictionary<K, V>> DistinctDictWith<K, V>(
			this Arbitrary<K> keyArb,
			Arbitrary<V> valueArb,
			params K[] keysToExclude
#if !N48_S2
		) where K : notnull where V : notnull =>
#else
		) =>
#endif
			Arbs.DictOf(keyArb.Generator, valueArb.Generator)
				// flipping forces the values (now keys) to be distinct. double flip, to force keys and values to be distinct.
				.Select(dict => dict.Flip().Flip())
				.Excluding(keysToExclude);

		public static Arbitrary<IDictionary<K, V>> Excluding<K, V>(
			this Arbitrary<IDictionary<K, V>> source,
			params K[] keysToExclude
#if !N48_S2
		) where K : notnull =>
#else
		) =>
#endif
			source
				.Generator
				.Excluding(keysToExclude);

		public static Arbitrary<IDictionary<K, V>> Excluding<K, V>(
			this Gen<IDictionary<K, V>> source,
			params K[] keysToExclude
#if !N48_S2
		) where K : notnull =>
#else
		) =>
#endif
			source
				.Select(dict => dict.AsMutableDict())
				.Select(dict => {
						keysToExclude.ForEach(key => dict.Remove(key));
						return dict.AsDict();
					}
				)
				.ToArbitrary();

		public static Arbitrary<(T, U)> ZipWith<T, U>(this Arbitrary<T> arb, Arbitrary<U> secondArb) =>
			FsCheckUtils.Zip(arb, secondArb);

		public static Arbitrary<(T, U)> ZipWith<T, U>(this Arbitrary<T> arb, Gen<U> gen) =>
			FsCheckUtils.Zip(arb.Generator, gen).ToArbitrary();

		public static Arbitrary<(T, U)> ZipWith<T, U>(this Gen<T> gen, Arbitrary<U> arb) =>
			FsCheckUtils.Zip(gen, arb.Generator).ToArbitrary();

		public static Arbitrary<(T, U)> ZipWith<T, U>(this Gen<T> gen, Gen<U> secondGen) =>
			FsCheckUtils.Zip(gen, secondGen).ToArbitrary();

		public static Arbitrary<IList<(T, U)>> DistinctListOfPairsWith<T, U>(this Arbitrary<T> arb, Arbitrary<U> secondArb)
#if !N48_S2
			where T : notnull where U : notnull =>
#else
			=>
#endif
			arb
				.DistinctDictWith(secondArb)
				.Select(dict => dict.Select(entry => (entry.Key, entry.Value)))
				.Select(entries => entries.AsList())
				.ToArbitrary();

		public static Arbitrary<Maybe<T>> ToMaybeArb<T>(this Arbitrary<T> arb, bool includeNulls = false) =>
			Arbs.MaybeArb(arb.Generator, includeNulls);

		public static Gen<U> Select<T, U>(this Arbitrary<T> arb, Func<T, U> selector) =>
			arb
				.Generator
				.Select(selector);

		public static Gen<U> SelectMany<T, U>(this Arbitrary<T> arb, Func<T, Gen<U>> selector) =>
			arb
				.Generator
				.SelectMany(selector);

		public static Gen<T> Where<T>(this Arbitrary<T> arb, Func<T, bool> predicate) => arb.Generator.Where(predicate);

		public static Gen<A> KeepFirst<A, B>(this Gen<(A, B)> source) => source.Select(pair => pair.Item1);

		public static Gen<B> KeepSecond<A, B>(this Gen<(A, B)> source) => source.Select(pair => pair.Item2);

		public static Arbitrary<(A a, A b)> Choose2<A>(this Arbitrary<A> source) =>
			source
				.Generator
				.Two()
				.Select(vals => (vals.Item1, vals.Item2))
				.ToArbitrary();

		public static Arbitrary<(A a, A b, A c)> Choose3<A>(this Arbitrary<A> source) =>
			source
				.Generator
				.Three()
				.Select(vals => (vals.Item1, vals.Item2, vals.Item3))
				.ToArbitrary();

		public static Arbitrary<(A a, A b, A c, A d)> Choose4<A>(this Arbitrary<A> source) =>
			source
				.Generator
				.Four()
				.Select(vals => (vals.Item1, vals.Item2, vals.Item3, vals.Item4))
				.ToArbitrary();
	}
}
