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
/// Represents a 2D point with generic numeric coordinates.
/// </summary>
/// <typeparam name="TNumber">The numeric type used for coordinates.</typeparam>
[SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "By design.")]
public record struct Point2D<TNumber>(TNumber X, TNumber Y)
    where TNumber : INumber<TNumber>
{
    /// <summary>
    /// Gets the point at the origin (0, 0).
    /// </summary>
    [Pure]
    public static Point2D<TNumber> Zero => new(TNumber.Zero, TNumber.Zero);

    /// <summary>
    /// Returns the squared distance between two points.
    /// </summary>
    /// <param name="left">The first point.</param>
    /// <param name="right">The second point.</param>
    /// <returns>The squared distance between <paramref name="left"/> and <paramref name="right"/>.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TNumber DistanceSquared(Point2D<TNumber> left, Point2D<TNumber> right) =>
        Vector2<TNumber>.LengthSquared(left - right);

    /// <summary>
    /// Linearly interpolates between two points.
    /// </summary>
    /// <param name="start">The start point.</param>
    /// <param name="end">The end point.</param>
    /// <param name="amount">The interpolation factor.</param>
    /// <returns>The interpolated point.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point2D<TNumber> Lerp(Point2D<TNumber> start, Point2D<TNumber> end, TNumber amount) =>
        new(start.X + ((end.X - start.X) * amount), start.Y + ((end.Y - start.Y) * amount));

    /// <summary>
    /// Translates a point by a vector.
    /// </summary>
    /// <param name="point">The point to translate.</param>
    /// <param name="vector">The translation vector.</param>
    /// <returns>The translated point.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point2D<TNumber> operator +(Point2D<TNumber> point, Vector2<TNumber> vector) =>
        new(point.X + vector.X, point.Y + vector.Y);

    /// <summary>
    /// Translates a point by the inverse of a vector.
    /// </summary>
    /// <param name="point">The point to translate.</param>
    /// <param name="vector">The vector to subtract.</param>
    /// <returns>The translated point.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Point2D<TNumber> operator -(Point2D<TNumber> point, Vector2<TNumber> vector) =>
        new(point.X - vector.X, point.Y - vector.Y);

    /// <summary>
    /// Computes the vector from one point to another.
    /// </summary>
    /// <param name="left">The origin point.</param>
    /// <param name="right">The destination point.</param>
    /// <returns>The vector from <paramref name="right"/> to <paramref name="left"/>.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<TNumber> operator -(Point2D<TNumber> left, Point2D<TNumber> right) =>
        new(left.X - right.X, left.Y - right.Y);
}
