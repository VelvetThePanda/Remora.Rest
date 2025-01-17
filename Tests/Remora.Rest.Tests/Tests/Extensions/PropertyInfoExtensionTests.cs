//
//  PropertyInfoExtensionTests.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) Jarl Gullberg
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

using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Remora.Rest.Extensions;
using Xunit;

// ReSharper disable SA1600
#pragma warning disable 1591, SA1600

namespace Remora.Rest.Tests.Extensions;

/// <summary>
/// Tests the <see cref="Rest.Extensions.PropertyInfoExtensions"/> class.
/// </summary>
public static class PropertyInfoExtensionTests
{
    public class AllowsNull
    {
        /// <summary>
        /// Gets nothing. This field is used to get an annotated value type.
        /// </summary>
        [PublicAPI]
        private int ValueType { get; }

        /// <summary>
        /// Gets nothing. This field is used to get an annotated nullable value type.
        /// </summary>
        [PublicAPI]
        private int? NullableValueType { get; }

        /// <summary>
        /// Gets nothing. This field is used to get an annotated reference type.
        /// </summary>
        [PublicAPI]
        private string ReferenceType { get; } = string.Empty;

        /// <summary>
        /// Gets nothing. This field is used to get an annotated nullable reference type.
        /// </summary>
        [PublicAPI]
        private string? NullableReferenceType { get; }

        /// <summary>
        /// Gets nothing. This field is used to get an annotated generic value type-containing reference type.
        /// </summary>
        [PublicAPI]
        private List<int> ValueTypeList { get; } = new();

        /// <summary>
        /// Gets nothing. This field is used to get an annotated nullable generic value type-containing
        /// reference type.
        /// </summary>
        [PublicAPI]
        private List<int>? NullableValueTypeList { get; }

        /// <summary>
        /// Gets nothing. This field is used to get an annotated generic nullable value type-containing
        /// reference type.
        /// </summary>
        [PublicAPI]
        private List<int?> NonNullableNullableValueTypeList { get; } = new();

        /// <summary>
        /// Gets nothing. This field is used to get an annotated generic reference type-containing reference
        /// type.
        /// </summary>
        [PublicAPI]
        private List<string> ReferenceTypeList { get; } = new();

        /// <summary>
        /// Gets nothing. This field is used to get an annotated nullable generic reference type-containing
        /// reference type.
        /// </summary>
        [PublicAPI]
        private List<string>? NullableReferenceTypeList { get; }

        /// <summary>
        /// Gets nothing. This field is used to get an annotated generic nullable reference type-containing
        /// reference type.
        /// </summary>
        [PublicAPI]
        private List<string?> NonNullableNullableReferenceTypeList { get; } = new();

        [Fact]
        public void ReturnsFalseForValueType()
        {
            var property = GetType().GetProperty
            (
                nameof(this.ValueType),
                BindingFlags.Instance | BindingFlags.NonPublic
            );

            Assert.NotNull(property);
            Assert.False(property!.AllowsNull());
        }

        [Fact]
        public void ReturnsTrueForNullableValueType()
        {
            var property = GetType().GetProperty
            (
                nameof(this.NullableValueType),
                BindingFlags.Instance | BindingFlags.NonPublic
            );

            Assert.NotNull(property);
            Assert.True(property!.AllowsNull());
        }

        [Fact]
        public void ReturnsFalseForReferenceType()
        {
            var property = GetType().GetProperty
            (
                nameof(this.ReferenceType),
                BindingFlags.Instance | BindingFlags.NonPublic
            );

            Assert.NotNull(property);
            Assert.False(property!.AllowsNull());
        }

        [Fact]
        public void ReturnsTrueForNullableReferenceType()
        {
            var property = GetType().GetProperty
            (
                nameof(this.NullableReferenceType),
                BindingFlags.Instance | BindingFlags.NonPublic
            );

            Assert.NotNull(property);
            Assert.True(property!.AllowsNull());
        }

        [Fact]
        public void ReturnsFalseForGenericTypeWithValueTypeArgument()
        {
            var property = GetType().GetProperty
            (
                nameof(this.ValueTypeList),
                BindingFlags.Instance | BindingFlags.NonPublic
            );

            Assert.NotNull(property);
            Assert.False(property!.AllowsNull());
        }

        [Fact]
        public void ReturnsTrueForNullableGenericTypeWithValueTypeArgument()
        {
            var property = GetType().GetProperty
            (
                nameof(this.NullableValueTypeList),
                BindingFlags.Instance | BindingFlags.NonPublic
            );

            Assert.NotNull(property);
            Assert.True(property!.AllowsNull());
        }

        [Fact]
        public void ReturnsFalseForNonNullableGenericTypeWithNullableValueTypeArgument()
        {
            var property = GetType().GetProperty
            (
                nameof(this.NonNullableNullableValueTypeList),
                BindingFlags.Instance | BindingFlags.NonPublic
            );

            Assert.NotNull(property);
            Assert.False(property!.AllowsNull());
        }

        [Fact]
        public void ReturnsFalseForGenericTypeWithReferenceTypeArgument()
        {
            var property = GetType().GetProperty
            (
                nameof(this.ReferenceTypeList),
                BindingFlags.Instance | BindingFlags.NonPublic
            );

            Assert.NotNull(property);
            Assert.False(property!.AllowsNull());
        }

        [Fact]
        public void ReturnsTrueForNullableGenericTypeWithReferenceTypeArgument()
        {
            var property = GetType().GetProperty
            (
                nameof(this.NullableReferenceTypeList),
                BindingFlags.Instance | BindingFlags.NonPublic
            );

            Assert.NotNull(property);
            Assert.True(property!.AllowsNull());
        }

        [Fact]
        public void ReturnsFalseForNonNullableGenericTypeWithNullableReferenceTypeArgument()
        {
            var property = GetType().GetProperty
            (
                nameof(this.NonNullableNullableReferenceTypeList),
                BindingFlags.Instance | BindingFlags.NonPublic
            );

            Assert.NotNull(property);
            Assert.False(property!.AllowsNull());
        }
    }
}
