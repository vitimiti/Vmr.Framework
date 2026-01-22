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
    /// Gets the center point of the
    /// </summary>
    [Pure]
    public Point2D<TNumber> Center
    {
        get
        {
            TNumber halfWidth = (Width - TNumber.One) / (TNumber.One + TNumber.One);
            TNumber halfHeight = (Height - TNumber.One) / (TNumber.One + TNumber.One);
            return new Point2D<TNumber>(X + halfWidth, Y + halfHeight);
        }
    }

    /// <summary>
    /// Gets the top-left corner.
    /// </summary>
    [Pure]
    public Point2D<TNumber> TopLeft => new(Left, Top);

    /// <summary>
    /// Gets the top-right corner.
    /// </summary>
    [Pure]
    public Point2D<TNumber> TopRight => new(Right, Top);

    /// <summary>
    /// Gets the bottom-left corner.
    /// </summary>
    [Pure]
    public Point2D<TNumber> BottomLeft => new(Left, Bottom);

    /// <summary>
    /// Gets the bottom-right corner.
    /// </summary>
    [Pure]
    public Point2D<TNumber> BottomRight => new(Right, Bottom);

    /// <summary>
    /// Gets the top-left position of the
    /// </summary>
    [Pure]
    public Point2D<TNumber> Position => new(X, Y);

    /// <summary>
    /// Gets the size of the
    /// </summary>
    [Pure]
    public Size2D<TNumber> Size => new(Width, Height);

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
    /// <returns>The created </returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle<TNumber> FromCenter(TNumber centerX, TNumber centerY, TNumber width, TNumber height)
    {
        TNumber halfWidth = width / (TNumber.One + TNumber.One);
        TNumber halfHeight = height / (TNumber.One + TNumber.One);
        return new Rectangle<TNumber>(centerX - halfWidth, centerY - halfHeight, width, height);
    }

    /// <summary>
    /// Creates a rectangle from a center point and size.
    /// </summary>
    /// <param name="center">The center point.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns>The created </returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle<TNumber> FromCenter(Point2D<TNumber> center, TNumber width, TNumber height) =>
        FromCenter(center.X, center.Y, width, height);

    /// <summary>
    /// Creates a rectangle from a center point and size.
    /// </summary>
    /// <param name="center">The center point.</param>
    /// <param name="size">The size.</param>
    /// <returns>The created </returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle<TNumber> FromCenter(Point2D<TNumber> center, Size2D<TNumber> size) =>
        FromCenter(center.X, center.Y, size.Width, size.Height);

    /// <summary>
    /// Computes the area of the
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
    /// Checks whether the rectangle intersects a circle (inclusive).
    /// </summary>
    /// <param name="circle">The circle to test.</param>
    /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Intersects(Circle2D<TNumber> circle)
    {
        Point2D<TNumber> closest = Clamp(circle.Center);
        return Point2D<TNumber>.DistanceSquared(closest, circle.Center) <= (circle.Radius * circle.Radius);
    }

    /// <summary>
    /// Checks whether the rectangle intersects a line segment.
    /// </summary>
    /// <param name="segment">The segment to test.</param>
    /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
    [Pure]
    public bool Intersects(LineSegment2D<TNumber> segment)
    {
        Vector2<TNumber> direction = segment.End - segment.Start;
        TNumber tMin = TNumber.Zero;
        TNumber tMax = TNumber.One;

        if (direction.X == TNumber.Zero)
        {
            if (segment.Start.X < Left || segment.Start.X > Right)
            {
                return false;
            }
        }
        else
        {
            TNumber tx1 = (Left - segment.Start.X) / direction.X;
            TNumber tx2 = (Right - segment.Start.X) / direction.X;
            tMin = TNumber.Max(tMin, TNumber.Min(tx1, tx2));
            tMax = TNumber.Min(tMax, TNumber.Max(tx1, tx2));
        }

        if (direction.Y == TNumber.Zero)
        {
            if (segment.Start.Y < Top || segment.Start.Y > Bottom)
            {
                return false;
            }
        }
        else
        {
            TNumber ty1 = (Top - segment.Start.Y) / direction.Y;
            TNumber ty2 = (Bottom - segment.Start.Y) / direction.Y;
            tMin = TNumber.Max(tMin, TNumber.Min(ty1, ty2));
            tMax = TNumber.Min(tMax, TNumber.Max(ty1, ty2));
        }

        return tMax >= tMin;
    }

    /// <summary>
    /// Returns the intersection of two rectangles. If there is no intersection, returns <see cref="Zero"/>.
    /// </summary>
    /// <param name="other">The other </param>
    /// <returns>The intersected </returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle<TNumber> Intersection(Rectangle<TNumber> other)
    {
        TNumber left = TNumber.Max(Left, other.Left);
        TNumber top = TNumber.Max(Top, other.Top);
        TNumber right = TNumber.Min(Right, other.Right);
        TNumber bottom = TNumber.Min(Bottom, other.Bottom);

        if (right < left || bottom < top)
        {
            return Zero;
        }

        return new Rectangle<TNumber>(left, top, right - left + TNumber.One, bottom - top + TNumber.One);
    }

    /// <summary>
    /// Returns the union of two rectangles.
    /// </summary>
    /// <param name="other">The other </param>
    /// <returns>The union </returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle<TNumber> Union(Rectangle<TNumber> other)
    {
        TNumber left = TNumber.Min(Left, other.Left);
        TNumber top = TNumber.Min(Top, other.Top);
        TNumber right = TNumber.Max(Right, other.Right);
        TNumber bottom = TNumber.Max(Bottom, other.Bottom);

        return new Rectangle<TNumber>(left, top, right - left + TNumber.One, bottom - top + TNumber.One);
    }

    /// <summary>
    /// Returns a rectangle offset by the specified vector.
    /// </summary>
    /// <param name="offset">The offset vector.</param>
    /// <returns>The offset </returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle<TNumber> Offset(Vector2<TNumber> offset) => new(X + offset.X, Y + offset.Y, Width, Height);

    /// <summary>
    /// Returns a rectangle inflated by the specified amounts on each axis.
    /// </summary>
    /// <param name="x">The horizontal inflation amount.</param>
    /// <param name="y">The vertical inflation amount.</param>
    /// <returns>The inflated </returns>
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
        TNumber x = TNumber.Min(TNumber.Max(point.X, Left), Right);
        TNumber y = TNumber.Min(TNumber.Max(point.Y, Top), Bottom);
        return new Point2D<TNumber>(x, y);
    }

    /// <summary>
    /// Expands the rectangle to include a point (inclusive).
    /// </summary>
    /// <param name="point">The point to include.</param>
    /// <returns>The expanded </returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle<TNumber> ExpandToInclude(Point2D<TNumber> point)
    {
        if (IsEmpty)
        {
            return new Rectangle<TNumber>(point.X, point.Y, TNumber.One, TNumber.One);
        }

        TNumber left = TNumber.Min(Left, point.X);
        TNumber top = TNumber.Min(Top, point.Y);
        TNumber right = TNumber.Max(Right, point.X);
        TNumber bottom = TNumber.Max(Bottom, point.Y);

        return new Rectangle<TNumber>(left, top, (right - left) + TNumber.One, (bottom - top) + TNumber.One);
    }

    /// <summary>
    /// Attempts to intersect a line segment with the rectangle.
    /// </summary>
    /// <param name="segment">The segment to test.</param>
    /// <param name="t">The normalized distance along the segment to the intersection point.</param>
    /// <param name="point">The intersection point.</param>
    /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
    [Pure]
    public bool TryIntersect(LineSegment2D<TNumber> segment, out TNumber t, out Point2D<TNumber> point)
    {
        Vector2<TNumber> direction = segment.End - segment.Start;
        TNumber tMin = TNumber.Zero;
        TNumber tMax = TNumber.One;

        if (direction.X == TNumber.Zero)
        {
            if (segment.Start.X < Left || segment.Start.X > Right)
            {
                t = TNumber.Zero;
                point = segment.Start;
                return false;
            }
        }
        else
        {
            TNumber tx1 = (Left - segment.Start.X) / direction.X;
            TNumber tx2 = (Right - segment.Start.X) / direction.X;
            tMin = TNumber.Max(tMin, TNumber.Min(tx1, tx2));
            tMax = TNumber.Min(tMax, TNumber.Max(tx1, tx2));
        }

        if (direction.Y == TNumber.Zero)
        {
            if (segment.Start.Y < Top || segment.Start.Y > Bottom)
            {
                t = TNumber.Zero;
                point = segment.Start;
                return false;
            }
        }
        else
        {
            TNumber ty1 = (Top - segment.Start.Y) / direction.Y;
            TNumber ty2 = (Bottom - segment.Start.Y) / direction.Y;
            tMin = TNumber.Max(tMin, TNumber.Min(ty1, ty2));
            tMax = TNumber.Min(tMax, TNumber.Max(ty1, ty2));
        }

        if (tMax < tMin)
        {
            t = TNumber.Zero;
            point = segment.Start;
            return false;
        }

        t = tMin;
        point = segment.Start + (direction * t);
        return true;
    }
}
