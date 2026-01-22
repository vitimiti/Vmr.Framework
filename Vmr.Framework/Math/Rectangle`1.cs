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
/// Represents an axis-aligned rectangle using a top-left origin and inclusive bounds.
/// </summary>
/// <typeparam name="TNumber">The numeric type used for coordinates.</typeparam>
[SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "By design.")]
public record struct Rectangle<TNumber>(TNumber X, TNumber Y, TNumber Width, TNumber Height)
    where TNumber : INumber<TNumber>
{
    /// <summary>
    /// Gets the rectangle with all components set to zero.
    /// </summary>
    [Pure]
    public static Rectangle<TNumber> Zero => new(TNumber.Zero, TNumber.Zero, TNumber.Zero, TNumber.Zero);

    /// <summary>
    /// Gets the left edge (X).
    /// </summary>
    [Pure]
    public TNumber Left => X;

    /// <summary>
    /// Gets the top edge (Y).
    /// </summary>
    [Pure]
    public TNumber Top => Y;

    /// <summary>
    /// Gets the right edge (inclusive).
    /// </summary>
    [Pure]
    public TNumber Right => X + Width - TNumber.One;

    /// <summary>
    /// Gets the bottom edge (inclusive).
    /// </summary>
    [Pure]
    public TNumber Bottom => Y + Height - TNumber.One;

    /// <summary>
    /// Gets the top-left position of the rectangle.
    /// </summary>
    [Pure]
    public Point2D<TNumber> Position => new(X, Y);

    /// <summary>
    /// Gets the size of the rectangle.
    /// </summary>
    [Pure]
    public Vector2<TNumber> Size => new(Width, Height);

    /// <summary>
    /// Gets a value indicating whether the rectangle has a non-positive width or height.
    /// </summary>
    [Pure]
    public bool IsEmpty => Width <= TNumber.Zero || Height <= TNumber.Zero;

    /// <summary>
    /// Creates a rectangle from a center point and size.
    /// </summary>
    /// <param name="centerX">The center X coordinate.</param>
    /// <param name="centerY">The center Y coordinate.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns>The created rectangle.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle<TNumber> FromCenter(TNumber centerX, TNumber centerY, TNumber width, TNumber height)
    {
        var halfWidth = width / (TNumber.One + TNumber.One);
        var halfHeight = height / (TNumber.One + TNumber.One);
        return new Rectangle<TNumber>(centerX - halfWidth, centerY - halfHeight, width, height);
    }

    /// <summary>
    /// Creates a rectangle from a center point and size.
    /// </summary>
    /// <param name="center">The center point.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns>The created rectangle.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle<TNumber> FromCenter(Point2D<TNumber> center, TNumber width, TNumber height) =>
        FromCenter(center.X, center.Y, width, height);

    /// <summary>
    /// Creates a rectangle from a center point and size.
    /// </summary>
    /// <param name="center">The center point.</param>
    /// <param name="size">The size.</param>
    /// <returns>The created rectangle.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle<TNumber> FromCenter(Point2D<TNumber> center, Vector2<TNumber> size) =>
        FromCenter(center.X, center.Y, size.X, size.Y);

    /// <summary>
    /// Computes the area of the rectangle.
    /// </summary>
    /// <returns>The area.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TNumber Area() => Width * Height;

    /// <summary>
    /// Checks whether the rectangle contains a point specified by coordinates (inclusive).
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <returns><see langword="true"/> if the point is inside; otherwise <see langword="false"/>.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(TNumber x, TNumber y) => x >= Left && x <= Right && y >= Top && y <= Bottom;

    /// <summary>
    /// Checks whether the rectangle contains a point (inclusive).
    /// </summary>
    /// <param name="point">The point to test.</param>
    /// <returns><see langword="true"/> if the point is inside; otherwise <see langword="false"/>.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Point2D<TNumber> point) =>
        point.X >= Left && point.X <= Right && point.Y >= Top && point.Y <= Bottom;

    /// <summary>
    /// Checks whether the rectangle fully contains another rectangle (inclusive).
    /// </summary>
    /// <param name="other">The rectangle to test.</param>
    /// <returns><see langword="true"/> if fully contained; otherwise <see langword="false"/>.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Rectangle<TNumber> other) =>
        other.Left >= Left && other.Right <= Right && other.Top >= Top && other.Bottom <= Bottom;

    /// <summary>
    /// Checks whether the rectangle intersects another rectangle (inclusive).
    /// </summary>
    /// <param name="other">The rectangle to test.</param>
    /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Intersects(Rectangle<TNumber> other) =>
        Left <= other.Right && Right >= other.Left && Top <= other.Bottom && Bottom >= other.Top;

    /// <summary>
    /// Returns the intersection of two rectangles. If there is no intersection, returns <see cref="Zero"/>.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>The intersected rectangle.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle<TNumber> Intersection(Rectangle<TNumber> other)
    {
        var left = TNumber.Max(Left, other.Left);
        var top = TNumber.Max(Top, other.Top);
        var right = TNumber.Min(Right, other.Right);
        var bottom = TNumber.Min(Bottom, other.Bottom);

        if (right < left || bottom < top)
        {
            return Zero;
        }

        return new Rectangle<TNumber>(left, top, right - left + TNumber.One, bottom - top + TNumber.One);
    }

    /// <summary>
    /// Returns the union of two rectangles.
    /// </summary>
    /// <param name="other">The other rectangle.</param>
    /// <returns>The union rectangle.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle<TNumber> Union(Rectangle<TNumber> other)
    {
        var left = TNumber.Min(Left, other.Left);
        var top = TNumber.Min(Top, other.Top);
        var right = TNumber.Max(Right, other.Right);
        var bottom = TNumber.Max(Bottom, other.Bottom);

        return new Rectangle<TNumber>(left, top, right - left + TNumber.One, bottom - top + TNumber.One);
    }

    /// <summary>
    /// Returns a rectangle offset by the specified vector.
    /// </summary>
    /// <param name="offset">The offset vector.</param>
    /// <returns>The offset rectangle.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle<TNumber> Offset(Vector2<TNumber> offset) => new(X + offset.X, Y + offset.Y, Width, Height);

    /// <summary>
    /// Returns a rectangle inflated by the specified amounts on each axis.
    /// </summary>
    /// <param name="x">The horizontal inflation amount.</param>
    /// <param name="y">The vertical inflation amount.</param>
    /// <returns>The inflated rectangle.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle<TNumber> Inflate(TNumber x, TNumber y) => new(X - x, Y - y, Width + (x + x), Height + (y + y));

    /// <summary>
    /// Clamps a point to the rectangle bounds (inclusive).
    /// </summary>
    /// <param name="point">The point to clamp.</param>
    /// <returns>The clamped point.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Point2D<TNumber> Clamp(Point2D<TNumber> point)
    {
        var x = TNumber.Min(TNumber.Max(point.X, Left), Right);
        var y = TNumber.Min(TNumber.Max(point.Y, Top), Bottom);
        return new Point2D<TNumber>(x, y);
    }
}
