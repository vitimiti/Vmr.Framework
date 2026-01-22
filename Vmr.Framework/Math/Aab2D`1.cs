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
/// Represents a 2D axis-aligned bounding box defined by minimum and maximum points (inclusive).
/// </summary>
/// <typeparam name="TNumber">The numeric type used for coordinates.</typeparam>
[SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "By design.")]
public record struct Aabb2D<TNumber>(Point2D<TNumber> Min, Point2D<TNumber> Max)
    where TNumber : INumber<TNumber>
{
    /// <summary>
    /// Gets the size of the bounding box.
    /// </summary>
    [Pure]
    public Size2D<TNumber> Size => new((Max.X - Min.X) + TNumber.One, (Max.Y - Min.Y) + TNumber.One);

    /// <summary>
    /// Creates a bounding box from a rectangle.
    /// </summary>
    /// <param name="rectangle">The rectangle.</param>
    /// <returns>The bounding box.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Aabb2D<TNumber> FromRectangle(Rectangle<TNumber> rectangle) =>
        new(
            new Point2D<TNumber>(rectangle.Left, rectangle.Top),
            new Point2D<TNumber>(rectangle.Right, rectangle.Bottom)
        );

    /// <summary>
    /// Converts the bounding box to a rectangle.
    /// </summary>
    /// <returns>The rectangle.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle<TNumber> ToRectangle() =>
        new(Min.X, Min.Y, (Max.X - Min.X) + TNumber.One, (Max.Y - Min.Y) + TNumber.One);

    /// <summary>
    /// Checks whether the bounding box contains a point (inclusive).
    /// </summary>
    /// <param name="point">The point to test.</param>
    /// <returns><see langword="true"/> if the point is inside; otherwise <see langword="false"/>.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Point2D<TNumber> point) =>
        point.X >= Min.X && point.X <= Max.X && point.Y >= Min.Y && point.Y <= Max.Y;

    /// <summary>
    /// Checks whether the bounding box intersects another bounding box (inclusive).
    /// </summary>
    /// <param name="other">The other bounding box.</param>
    /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Intersects(Aabb2D<TNumber> other) =>
        Min.X <= other.Max.X && Max.X >= other.Min.X && Min.Y <= other.Max.Y && Max.Y >= other.Min.Y;

    /// <summary>
    /// Converts the bounding box to a rectangle.
    /// </summary>
    /// <param name="aabb">The bounding box.</param>
    /// <returns>The rectangle.</returns>
    public static explicit operator Rectangle<TNumber>(Aabb2D<TNumber> aabb) => aabb.ToRectangle();
}
