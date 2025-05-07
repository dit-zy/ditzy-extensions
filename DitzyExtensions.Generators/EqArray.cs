using System.Collections.Immutable;

namespace DitzyExtensions.Generators;

/**
 * value equatable array. 2 instances will be equal so long as they have equal elements.
 *
 * used with the incremental generator framework in order to cache correctly.
 */
public class EqArray<T> : IEquatable<EqArray<T>> {
	public static EqArray<T> Empty { get; } = new([]);
	
	public int Count => Values.Count;

	public IList<T> Values { get; }

	public EqArray(IEnumerable<T> source) {
		Values = source.ToImmutableArray();
	}

	public override bool Equals(object? obj) {
		return (obj is EqArray<T> other) && Equals(other);
	}

	public bool Equals(EqArray<T>? other) {
		if (ReferenceEquals(this, other)) return true;
		if (ReferenceEquals(Values, other?.Values)) return true;
		if (Values.Count != other!.Values.Count) return false;
		return Values
			.Select((t, i) => Equals(t, other.Values[i]))
			.All(b => b);
	}
	
	public override int GetHashCode() => Values.GetHashCode();
}
