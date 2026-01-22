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

using System.Diagnostics.Contracts;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Vmr.Framework.Math;

/// <summary>
/// Represents a 2D ray defined by an origin point and a direction vector.
/// </summary>
/// <typeparam name="TNumber">The numeric type used for coordinates.</typeparam>
public record struct Ray2D<TNumber>(Point2D<TNumber> Origin, Vector2<TNumber> Direction)
    where TNumber : INumber<TNumber>
{
    /// <summary>
    /// Returns a point along the ray at the specified distance.
    /// </summary>
    /// <param name="distance">The distance from the origin.</param>
    /// <returns>The point on the ray.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Point2D<TNumber> PointAt(TNumber distance) => Origin + (Direction * distance);
}
