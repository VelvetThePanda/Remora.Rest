//
//  SnakeCaseNamingPolicy.cs
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

// This code uses modified portion of MIT licensed Newtonsoft.Json source code
//
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
using System;
using System.Buffers;
using System.Text;
using System.Text.Json;
using JetBrains.Annotations;

namespace Remora.Rest.Json.Policies;

/// <summary>
/// Represents a snake_case naming policy.
/// </summary>
[PublicAPI]
public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    private readonly bool _upperCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="SnakeCaseNamingPolicy"/> class.
    /// </summary>
    /// <param name="upperCase">Whether the converted names should be in all upper case.</param>
    public SnakeCaseNamingPolicy(bool upperCase = false)
    {
        _upperCase = upperCase;
    }

    /// <inheritdoc />
    public override string ConvertName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        var buffer = ArrayPool<int>.Shared.Rent(name.Length / 2);

        var size = 0;

        for (int i = 1; i < name.Length; i++)
        {
            if (char.IsLower(name[i - 1]) && char.IsUpper(name[i]))
            {
                buffer[size++] = i;
            }
            else if (i > 1 && char.IsUpper(name[i - 1]) && char.IsUpper(name[i - 2]) && char.IsLower(name[i]))
            {
                buffer[size++] = i - 1;
            }
        }

        var output = string.Create(name.Length + size, (name, buffer, size), static (span, state) =>
        {
            (string name, int[] buffer, int size) = state;

            span.Fill(' ');

            var nameSpan = name.AsSpan();
            var last = buffer[0];

            nameSpan.Slice(0, buffer[0]).ToLowerInvariant(span.Slice(0, buffer[0]));

            for (int i = 1; i < size; i++)
            {
                var length = buffer[i] - last;
                var index = last + (i - 1);
                span[index] = '_';

                nameSpan.Slice(last, length).ToLowerInvariant(span.Slice(index + 1, length));

                last = buffer[i];
            }

            span[last + size - 1] = '_';
            nameSpan.Slice(last).ToLowerInvariant(span.Slice(last + size));
        });

        ArrayPool<int>.Shared.Return(buffer);

        return output;
    }

    private enum SeparatedCaseState
    {
        Start,
        Lower,
        Upper,
        NewWord
    }
}
