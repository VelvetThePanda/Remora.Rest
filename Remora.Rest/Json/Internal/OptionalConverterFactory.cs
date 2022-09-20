//
//  OptionalConverterFactory.cs
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

using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Remora.Rest.Core;

namespace Remora.Rest.Json.Internal;

/// <summary>
/// Creates converters for <see cref="Optional{TValue}"/>.
/// </summary>
internal class OptionalConverterFactory : JsonConverterFactory
{
    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
    {
        var typeInfo = typeToConvert.GetTypeInfo();
        if (!typeInfo.IsGenericType || typeInfo.IsGenericTypeDefinition)
        {
            return false;
        }

        var genericType = typeInfo.GetGenericTypeDefinition();
        return genericType == typeof(Optional<>);
    }

    /// <inheritdoc />
    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var typeInfo = typeToConvert.GetTypeInfo();

        var optionalType = typeof(OptionalConverter<>).MakeGenericType(typeInfo.GenericTypeArguments);

        if (Activator.CreateInstance(optionalType) is not JsonConverter createdConverter)
        {
            throw new JsonException();
        }

        return createdConverter;
    }
}
