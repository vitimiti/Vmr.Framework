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
/// Represents a 2D circle defined by a center point and radius.
/// </summary>
/// <typeparam name="TNumber">The numeric type used for coordinates.</typeparam>
[SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "By design.")]
public record struct Circle<TNumber>(Point2D<TNumber> Center, TNumber Radius)
    where TNumber : INumber<TNumber>
{
    /// <summary>
    /// Gets a circle centered at the origin with a radius of zero.
    /// </summary>
    [Pure]
    public static Circle<TNumber> Zero => new(Point2D<TNumber>.Zero, TNumber.Zero);

    /// <summary>
    /// Gets the squared radius.
    /// </summary>
    [Pure]
    public TNumber RadiusSquared => Radius * Radius;

    /// <summary>
    /// Checks whether the circle contains a point (inclusive).
    /// </summary>
    /// <param name="point">The point to test.</param>
    /// <returns><see langword="true"/> if the point is inside; otherwise <see langword="false"/>.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Point2D<TNumber> point) => Point2D<TNumber>.DistanceSquared(Center, point) <= RadiusSquared;

    /// <summary>
    /// Checks whether the circle intersects another circle (inclusive).
    /// </summary>
    /// <param name="other">The other circle.</param>
    /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Intersects(Circle<TNumber> other)
    {
        TNumber radiusSum = Radius + other.Radius;
        return Point2D<TNumber>.DistanceSquared(Center, other.Center) <= radiusSum * radiusSum;
    }

    /// <summary>
    /// Checks whether the circle intersects a rectangle (inclusive).
    /// </summary>
    /// <param name="rectangle">The rectangle to test.</param>
    /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Intersects(Rectangle<TNumber> rectangle) => rectangle.Intersects(this);
}
