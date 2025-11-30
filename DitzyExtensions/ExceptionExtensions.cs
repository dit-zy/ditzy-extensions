/*
 SPDX-License-Identifier: MIT

 Copyright 2025 ditzy

 Use of this source code is governed by an MIT-style
 license that can be found in the LICENSE file or at
 https://opensource.org/licenses/MIT.
 */

using System;
using CSharpFunctionalExtensions;

namespace DitzyExtensions {
	public class ExceptionExtensions {
		public static Result<T, U> Try<T, U, E>(Func<T> action, Func<E, U> catchAction) where E : Exception {
			try {
				return action();
			} catch (E e) {
				return catchAction(e);
			}
		}
	}
}
