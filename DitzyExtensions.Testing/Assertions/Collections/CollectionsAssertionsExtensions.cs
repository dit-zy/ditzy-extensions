using System.Collections.Generic;
using System.Linq;
using DitzyExtensions.Collection;
using FluentAssertions;
using FluentAssertions.Collections;

namespace DitzyExtensions.Testing.Assertions.Collections {
	public static class CollectionsAssertionsExtensions {
		public static AndConstraint<GenericCollectionAssertions<T>> ContainExactlyInAnyOrder<T>(
			this GenericCollectionAssertions<T> context,
			IEnumerable<T> expectedContents,
			string because = "",
			params object[] becauseArgs
		) {
			// context
			// 	.Subject
			// 	.ForEach(value => {
			// 			var count = valueCount.GetValueOrSetDefault(value, 0);
			// 			count++;
			// 			valueCount.Put(value, count);
			// 		}
			// 	);
			
			// var notFound = new List<T>(expected);
			// var extras = new List<T>(actual);
			//
			// var equalityComparer = HashSet<T>.CreateSetComparer();
			// context.CurrentAssertionChain
			// 	.BecauseOf(because, becauseArgs)
			// 	.ForCondition(equalityComparer.Equals(actual, expected))
			// 	.FailWith(
			// 		"Mismatch in expected elements contained in collection:\n\nexpected but not found: {0}\n\nfound but not expected: {1}",
			// 		notFound.AsString(),
			// 		extras.AsString()
			// 		);

			return new AndConstraint<GenericCollectionAssertions<T>>(context);
		}
	}
}
