// A multipurpose, open source, cross‑platform framework for game development in modern .NET.
// Copyright (C) 2026  The Vmr.Framework Contributors
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Vmr.Framework.Math;

/// <summary>
/// Represents a 2D line segment defined by two points.
/// </summary>
/// <typeparam name="TNumber">The numeric type used for coordinates.</typeparam>
public record struct LineSegment2D<TNumber>(Point2D<TNumber> Start, Point2D<TNumber> End)
    where TNumber : INumber<TNumber>
{
    /// <summary>
    /// Gets the direction vector from start to end.
    /// </summary>
    [Pure]
    public Vector2<TNumber> Direction => End - Start;

    /// <summary>
    /// Returns the squared length of the segment.
    /// </summary>
    /// <returns>The squared length.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TNumber LengthSquared() => Vector2<TNumber>.LengthSquared(Direction);
}
