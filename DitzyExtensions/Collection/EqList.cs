using System;
using System.Collections.Generic;
using System.Linq;

namespace DitzyExtensions.Collection {
	/// <summary>
	/// A value equatable list wrapper.
	/// <br/>
	/// <br/>
	/// 2 instances will be equal so long as they have equal elements. This is not a full collection class, instead wrapping a <see cref="List{T}"/> with equality checking.
	/// </summary>
	public class EqList<T> : IEquatable<EqList<T>> {
		public static EqList<T> Empty { get; } = new EqList<T>(Array.Empty<T>());

		public int Count => Values.Count;

		public IList<T> Values { get; }

		/// <summary>
		/// Initialize the <see cref="EqList{T}"/> with the provided enumerable. This will make a shallow copy of <c>source</c>.
		/// </summary>
		/// <param name="source">The enumerable to initialize the <see cref="EqList{T}"/> with.</param>
		/// <param name="immutable">When <c>true</c>, the wrapper <see cref="List{T}"/> will be immutable. When <c>false</c>, it will be mutable. The default is <c>true</c>.</param>
		public EqList(IEnumerable<T> source, bool immutable = true) {
			Values = immutable ? source.AsList() : source.AsMutableList();
		}

#if N48_S2
		public bool Equals(EqList<T> other) {
#else
		public bool Equals(EqList<T>? other) {
#endif
			if (other is null) return false;
			if (ReferenceEquals(this, other)) return true;
			return Values
				.Select((value, i) => Equals(value, other.Values[i]))
				.All(b => b);
		}

#if N48_S2
		public override bool Equals(object obj) {
#else
		public override bool Equals(object? obj) {
#endif
			if (obj is null) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((EqList<T>)obj);
		}

		public override int GetHashCode() {
			return Values.GetHashCode();
		}
	}
}
