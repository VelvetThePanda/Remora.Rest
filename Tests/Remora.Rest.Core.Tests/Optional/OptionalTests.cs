//
//  OptionalTests.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2017 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using Xunit;

// ReSharper disable SA1600
#pragma warning disable 1591, SA1600

namespace Remora.Rest.Core.Tests;

/// <summary>
/// Tests the <see cref="Optional{TValue}"/> struct.
/// </summary>
public class OptionalTests
{
    /// <summary>
    /// Tests the <see cref="Optional{TValue}.HasValue"/> property.
    /// </summary>
    public class HasValue
    {
        [Fact]
        public void ReturnsTrueWhenOptionalValueTypeContainsValue()
        {
            var optional = new Optional<int>(0);

            Assert.True(optional.HasValue);
        }

        [Fact]
        public void ReturnsFalseWhenOptionalValueTypeDoesNotContainValue()
        {
            Optional<int> optional = default;

            Assert.False(optional.HasValue);
        }

        [Fact]
        public void ReturnsTrueWhenOptionalNullableValueTypeContainsValue()
        {
            var optional = new Optional<int?>(0);

            Assert.True(optional.HasValue);
        }

        [Fact]
        public void ReturnsTrueWhenOptionalNullableValueTypeContainsNull()
        {
            var optional = new Optional<int?>(null);

            Assert.True(optional.HasValue);
        }

        [Fact]
        public void ReturnsFalseWhenOptionalNullableValueTypeDoesNotContainValue()
        {
            Optional<int?> optional = default;

            Assert.False(optional.HasValue);
        }

        [Fact]
        public void ReturnsTrueWhenOptionalReferenceTypeContainsValue()
        {
            var optional = new Optional<string>("Hello world!");

            Assert.True(optional.HasValue);
        }

        [Fact]
        public void ReturnsFalseWhenOptionalReferenceTypeDoesNotContainValue()
        {
            Optional<string> optional = default;

            Assert.False(optional.HasValue);
        }

        [Fact]
        public void ReturnsTrueWhenOptionalNullableReferenceTypeContainsValue()
        {
            var optional = new Optional<string?>(null);

            Assert.True(optional.HasValue);
        }

        [Fact]
        public void ReturnsFalseWhenOptionalNullableReferenceTypeDoesNotContainValue()
        {
            Optional<string?> optional = default;

            Assert.False(optional.HasValue);
        }
    }

    /// <summary>
    /// Tests the <see cref="Optional{TValue}.Value"/> property.
    /// </summary>
    public class Value
    {
        [Fact]
        public void ReturnsCorrectValueIfValueTypeOptionalContainsValue()
        {
            var optional = new Optional<int>(64);

            Assert.Equal(64, optional.Value);
        }

        [Fact]
        public void ThrowsIfValueTypeOptionalDoesNotContainValue()
        {
            Optional<int> optional = default;

            Assert.Throws<InvalidOperationException>(() => optional.Value);
        }

        [Fact]
        public void ReturnsCorrectValueIfNullableValueTypeOptionalContainsValue()
        {
            var optional = new Optional<int?>(64);

            Assert.Equal(64, optional.Value);
        }

        [Fact]
        public void ReturnsCorrectValueIfNullableValueTypeOptionalContainsNullValue()
        {
            var optional = new Optional<int?>(null);

            Assert.Null(optional.Value);
        }

        [Fact]
        public void ThrowsIfNullableValueTypeOptionalDoesNotContainValue()
        {
            Optional<int?> optional = default;

            Assert.Throws<InvalidOperationException>(() => optional.Value);
        }

        [Fact]
        public void ReturnsCorrectValueIfReferenceTypeOptionalContainsValue()
        {
            var optional = new Optional<string>("Hello world!");

            Assert.Equal("Hello world!", optional.Value);
        }

        [Fact]
        public void ThrowsIfReferenceTypeOptionalDoesNotContainValue()
        {
            Optional<string> optional = default;

            Assert.Throws<InvalidOperationException>(() => optional.Value);
        }

        [Fact]
        public void ReturnsCorrectValueIfNullableReferenceTypeOptionalContainsValue()
        {
            var optional = new Optional<string?>("Hello world!");

            Assert.Equal("Hello world!", optional.Value);
        }

        [Fact]
        public void ReturnsCorrectValueIfNullableReferenceTypeOptionalContainsNullValue()
        {
            var optional = new Optional<string?>(null);

            Assert.Null(optional.Value);
        }

        [Fact]
        public void ThrowsIfNullableReferenceTypeOptionalDoesNotContainValue()
        {
            Optional<string?> optional = default;

            Assert.Throws<InvalidOperationException>(() => optional.Value);
        }
    }

    /// <summary>
    /// Tests the <see cref="Optional{TValue}.IsDefined()"/> method and its overloads.
    /// </summary>
    public class IsDefined
    {
        [Fact]
        public void ReturnsFalseIfNullableOptionalHasNoValue()
        {
            Optional<int?> noValue = default;
            Assert.False(noValue.IsDefined());

            Assert.False(noValue.IsDefined(out var value));
            Assert.Null(value);
        }

        [Fact]
        public void ReturnsFalseIfNullableOptionalHasNullValue()
        {
            Optional<int?> noValue = null;
            Assert.False(noValue.IsDefined());

            Assert.False(noValue.IsDefined(out var value));
            Assert.Null(value);
        }

        [Fact]
        public void ReturnsTrueIfNullableOptionalHasNonNullValue()
        {
            Optional<int?> noValue = 1;
            Assert.True(noValue.IsDefined());

            Assert.True(noValue.IsDefined(out var value));
            Assert.NotNull(value);
        }

        [Fact]
        public void ReturnsFalseIfOptionalHasNoValue()
        {
            Optional<int> noValue = default;
            Assert.False(noValue.IsDefined());
            Assert.False(noValue.IsDefined(out _));
        }

        [Fact]
        public void ReturnsTrueIfOptionalHasNonNullValue()
        {
            Optional<int> noValue = 1;
            Assert.True(noValue.IsDefined());

            Assert.True(noValue.IsDefined(out var value));
            Assert.Equal(1, value);
        }
    }

    /// <summary>
    /// Tests the <see cref="Optional{TValue}.ToString"/> method.
    /// </summary>
    public new class ToString
    {
        [Fact]
        public void ResultContainsValueIfValueTypeOptionalContainsValue()
        {
            var optional = new Optional<int>(64);

            Assert.Contains(64.ToString(), optional.ToString());
        }

        [Fact]
        public void ResultIsEmptyIfValueTypeOptionalDoesNotContainValue()
        {
            var optional = default(Optional<int>);

            Assert.Equal("Empty", optional.ToString());
        }

        [Fact]
        public void ResultContainsValueIfNullableValueTypeOptionalContainsValue()
        {
            var optional = new Optional<int?>(64);

            Assert.Contains(64.ToString(), optional.ToString());
        }

        [Fact]
        public void ResultContainsNullIfNullableValueTypeOptionalContainsNullValue()
        {
            var optional = new Optional<int?>(null);

            Assert.Contains("null", optional.ToString());
        }

        [Fact]
        public void ResultIsEmptyIfNullableValueTypeOptionalDoesNotContainValue()
        {
            var optional = default(Optional<int?>);

            Assert.Equal("Empty", optional.ToString());
        }

        [Fact]
        public void ResultContainsValueIfReferenceTypeOptionalContainsValue()
        {
            var optional = new Optional<string>("Hello world!");

            Assert.Contains("Hello world!", optional.ToString());
        }

        [Fact]
        public void ResultIsEmptyIfReferenceTypeOptionalDoesNotContainValue()
        {
            var optional = default(Optional<string>);

            Assert.Equal("Empty", optional.ToString());
        }

        [Fact]
        public void ResultContainsValueIfNullableReferenceTypeOptionalContainsValue()
        {
            var optional = new Optional<string?>("Hello world!");

            Assert.Contains("Hello world!", optional.ToString());
        }

        [Fact]
        public void ResultContainsNullIfNullableReferenceTypeOptionalContainsNullValue()
        {
            var optional = new Optional<string?>(null);

            Assert.Contains("null", optional.ToString());
        }

        [Fact]
        public void ResultIsEmptyIfNullableReferenceTypeOptionalDoesNotContainValue()
        {
            var optional = default(Optional<string?>);

            Assert.Equal("Empty", optional.ToString());
        }
    }

    /// <summary>
    /// Tests the implicit operator.
    /// </summary>
    public class ImplicitOperator
    {
        [Fact]
        public void CanCreateValueTypeOptionalImplicitly()
        {
            Optional<int> optional = 64;

            Assert.True(optional.HasValue);
            Assert.Equal(64, optional.Value);
        }

        [Fact]
        public void CanCreateNullableValueTypeOptionalImplicitly()
        {
            Optional<int?> optional = 64;

            Assert.True(optional.HasValue);
            Assert.Equal(64, optional.Value);

            Optional<int?> nullOptional = null;

            Assert.True(nullOptional.HasValue);
            Assert.Null(nullOptional.Value);
        }

        [Fact]
        public void CanCreateReferenceTypeOptionalImplicitly()
        {
            Optional<string> optional = "Hello world!";

            Assert.True(optional.HasValue);
            Assert.Equal("Hello world!", optional.Value);
        }

        [Fact]
        public void CanCreateNullableReferenceTypeOptionalImplicitly()
        {
            Optional<string?> optional = "Hello world!";

            Assert.True(optional.HasValue);
            Assert.Equal("Hello world!", optional.Value);

            Optional<string?> nullOptional = null;

            Assert.True(nullOptional.HasValue);
            Assert.Null(nullOptional.Value);
        }
    }

    /// <summary>
    /// Tests the equality operator.
    /// </summary>
    public class EqualityOperator
    {
        [Fact]
        public void ReturnsTrueForDefaultValues()
        {
            var a = default(Optional<int>);
            var b = default(Optional<int>);

            Assert.True(a == b);
            Assert.True(b == a);
        }

        [Fact]
        public void ReturnsTrueForSameContainedValues()
        {
            Optional<int> a = 1;
            Optional<int> b = 1;

            Assert.True(a == b);
            Assert.True(b == a);
        }

        [Fact]
        public void ReturnsFalseForDefaultValueComparedToContainedValue()
        {
            var a = default(Optional<int>);
            Optional<int> b = 1;

            Assert.False(a == b);
            Assert.False(b == a);
        }

        [Fact]
        public void ReturnsFalseForDifferentContainedValues()
        {
            Optional<int> a = 1;
            Optional<int> b = 2;

            Assert.False(a == b);
            Assert.False(b == a);
        }
    }

    /// <summary>
    /// Tests the inequality operator.
    /// </summary>
    public class InequalityOperator
    {
        [Fact]
        public void ReturnsFalseForDefaultValues()
        {
            var a = default(Optional<int>);
            var b = default(Optional<int>);

            Assert.False(a != b);
            Assert.False(b != a);
        }

        [Fact]
        public void ReturnsFalseForSameContainedValues()
        {
            Optional<int> a = 1;
            Optional<int> b = 1;

            Assert.False(a != b);
            Assert.False(b != a);
        }

        [Fact]
        public void ReturnsTrueForDefaultValueComparedToContainedValue()
        {
            var a = default(Optional<int>);
            Optional<int> b = 1;

            Assert.True(a != b);
            Assert.True(b != a);
        }

        [Fact]
        public void ReturnsTrueForDifferentContainedValues()
        {
            Optional<int> a = 1;
            Optional<int> b = 2;

            Assert.True(a != b);
            Assert.True(b != a);
        }
    }

    /// <summary>
    /// Tests the <see cref="Optional{TValue}.Equals(Optional{TValue})"/> method and its overloads.
    /// </summary>
    public new class Equals
    {
        public class Typed
        {
            [Fact]
            public void ReturnsTrueForDefaultValues()
            {
                var a = default(Optional<int>);
                var b = default(Optional<int>);

                Assert.True(a.Equals(b));
            }

            [Fact]
            public void ReturnsTrueForSameContainedValues()
            {
                Optional<int> a = 1;
                Optional<int> b = 1;

                Assert.True(a.Equals(b));
            }

            [Fact]
            public void ReturnsFalseForDefaultValueComparedToContainedValue()
            {
                var a = default(Optional<int>);
                Optional<int> b = 1;

                Assert.False(a.Equals(b));
            }

            [Fact]
            public void ReturnsFalseForDifferentContainedValues()
            {
                Optional<int> a = 1;
                Optional<int> b = 2;

                Assert.False(a.Equals(b));
            }
        }

        public class Object
        {
            [Fact]
            public void ReturnsTrueForDefaultValues()
            {
                var a = default(Optional<int>);
                object b = default(Optional<int>);

                Assert.True(a.Equals(b));
            }

            [Fact]
            public void ReturnsTrueForSameContainedValues()
            {
                Optional<int> a = 1;
                object b = new Optional<int>(1);

                Assert.True(a.Equals(b));
            }

            [Fact]
            public void ReturnsFalseForDefaultValueComparedToContainedValue()
            {
                var a = default(Optional<int>);
                object b = new Optional<int>(1);

                Assert.False(a.Equals(b));
            }

            [Fact]
            public void ReturnsFalseForDifferentContainedValues()
            {
                Optional<int> a = 1;
                object b = new Optional<int>(2);

                Assert.False(a.Equals(b));
            }
        }
    }

    /// <summary>
    /// Tests the <see cref="Optional{TValue}.GetHashCode"/> method.
    /// </summary>
    public new class GetHashCode
    {
        [Fact]
        public static void ReturnsDefaultContainedCombinedWithFalseForDefault()
        {
            var a = default(Optional<int>);
            Assert.Equal(HashCode.Combine(default(int), false), a.GetHashCode());
        }

        [Fact]
        public static void ReturnsContainedCombinedWithTrueForContainedValue()
        {
            Optional<int> a = 1;
            Assert.Equal(HashCode.Combine(1, true), a.GetHashCode());
        }
    }
}
