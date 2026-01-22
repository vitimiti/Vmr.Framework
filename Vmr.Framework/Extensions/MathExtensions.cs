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
using Vmr.Framework.Math;

namespace Vmr.Framework.Extensions;

/// <summary>
/// Provides extension math operations for point and vector types.
/// </summary>
public static class MathExtensions
{
    /// <summary>
    /// Distance-related extensions for <see cref="Point2D{TNumber}"/> that require square-root support.
    /// </summary>
    extension<TNumber>(Point2D<TNumber> point)
        where TNumber : INumber<TNumber>, IRootFunctions<TNumber>
    {
        /// <summary>
        /// Computes the distance between two points.
        /// </summary>
        /// <param name="other">The other point.</param>
        /// <returns>The distance between <paramref name="point"/> and <paramref name="other"/>.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TNumber Distance(Point2D<TNumber> other) => (point - other).Length();
    }

    /// <summary>
    /// Vector extensions that require square-root support.
    /// </summary>
    extension<TNumber>(Vector2<TNumber> vector)
        where TNumber : INumber<TNumber>, IRootFunctions<TNumber>
    {
        /// <summary>
        /// Returns the length of the vector.
        /// </summary>
        /// <returns>The vector length.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TNumber Length() => TNumber.Sqrt(Vector2<TNumber>.LengthSquared(vector));

        /// <summary>
        /// Returns a normalized copy of the vector.
        /// </summary>
        /// <returns>The normalized vector.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2<TNumber> Normalize() => vector / vector.Length();

        /// <summary>
        /// Attempts to normalize the vector.
        /// </summary>
        /// <param name="result">
        /// When this method returns <see langword="true"/>, contains the normalized vector;
        /// otherwise contains <see cref="Vector2{TNumber}.Zero"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the vector has non-zero length; otherwise <see langword="false"/>.
        /// </returns>
        public bool TryNormalize(out Vector2<TNumber> result)
        {
            TNumber length = vector.Length();
            if (length == TNumber.Zero)
            {
                result = Vector2<TNumber>.Zero;
                return false;
            }

            result = vector / length;
            return true;
        }
    }

    /// <summary>
    /// Vector extensions that require IEEE 754 floating-point support.
    /// </summary>
    extension<TNumber>(Vector2<TNumber> vector)
        where TNumber : IFloatingPointIeee754<TNumber>
    {
        /// <summary>
        /// Returns the signed angle to another vector, in radians.
        /// </summary>
        /// <param name="other">The other vector.</param>
        /// <returns>The angle from <paramref name="vector"/> to <paramref name="other"/> in radians.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TNumber AngleTo(Vector2<TNumber> other) =>
            TNumber.Atan2(Vector2<TNumber>.Cross(vector, other), Vector2<TNumber>.Dot(vector, other));

        /// <summary>
        /// Rotates the vector by the specified angle in radians.
        /// </summary>
        /// <param name="radians">The rotation angle in radians.</param>
        /// <returns>The rotated vector.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2<TNumber> Rotate(TNumber radians)
        {
            TNumber cos = TNumber.Cos(radians);
            TNumber sin = TNumber.Sin(radians);
            return new Vector2<TNumber>((vector.X * cos) - (vector.Y * sin), (vector.X * sin) + (vector.Y * cos));
        }
    }

    /// <summary>
    /// Circle extensions that require square-root support.
    /// </summary>
    extension<TNumber>(Circle2D<TNumber> circle)
        where TNumber : INumber<TNumber>, IRootFunctions<TNumber>
    {
        /// <summary>
        /// Gets the circle's radius.
        /// </summary>
        [Pure]
        public TNumber Radius => TNumber.Sqrt(circle.RadiusSquared);
    }

    extension<TNumber>(Circle2D<TNumber> circle)
        where TNumber : IFloatingPointIeee754<TNumber>
    {
        /// <summary>
        /// Checks whether the circle intersects a ray.
        /// </summary>
        /// <param name="ray">The ray to test.</param>
        /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
        [Pure]
        public bool Intersects(Ray2D<TNumber> ray) => circle.TryIntersect(ray, out _, out _);

        /// <summary>
        /// Checks whether the circle intersects a line segment.
        /// </summary>
        /// <param name="segment">The segment to test.</param>
        /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
        [Pure]
        public bool Intersects(LineSegment2D<TNumber> segment) => circle.TryIntersect(segment, out _, out _);

        /// <summary>
        /// Attempts to intersect a ray with the circle.
        /// </summary>
        /// <param name="ray">The ray to test.</param>
        /// <param name="t">The distance along the ray to the intersection point.</param>
        /// <param name="point">The intersection point.</param>
        /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
        [Pure]
        public bool TryIntersect(Ray2D<TNumber> ray, out TNumber t, out Point2D<TNumber> point)
        {
            Vector2<TNumber> oc = ray.Origin - circle.Center;
            TNumber a = Vector2<TNumber>.Dot(ray.Direction, ray.Direction);
            TNumber b = (TNumber.One + TNumber.One) * Vector2<TNumber>.Dot(oc, ray.Direction);
            TNumber c = Vector2<TNumber>.Dot(oc, oc) - (circle.Radius * circle.Radius);
            TNumber discriminant = (b * b) - ((TNumber.One + TNumber.One) * a * c);

            if (discriminant < TNumber.Zero)
            {
                t = TNumber.Zero;
                point = ray.Origin;
                return false;
            }

            TNumber sqrt = TNumber.Sqrt(discriminant);
            TNumber twoA = (TNumber.One + TNumber.One) * a;

            TNumber t0 = (-b - sqrt) / twoA;
            TNumber t1 = (-b + sqrt) / twoA;

            t = t0 >= TNumber.Zero ? t0 : t1;
            if (t < TNumber.Zero)
            {
                point = ray.Origin;
                return false;
            }

            point = ray.PointAt(t);
            return true;
        }

        /// <summary>
        /// Attempts to intersect a line segment with the circle.
        /// </summary>
        /// <param name="segment">The segment to test.</param>
        /// <param name="t">The normalized distance along the segment to the intersection point.</param>
        /// <param name="point">The intersection point.</param>
        /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
        [Pure]
        public bool TryIntersect(LineSegment2D<TNumber> segment, out TNumber t, out Point2D<TNumber> point)
        {
            Vector2<TNumber> direction = segment.End - segment.Start;
            Vector2<TNumber> oc = segment.Start - circle.Center;

            TNumber a = Vector2<TNumber>.Dot(direction, direction);
            TNumber b = (TNumber.One + TNumber.One) * Vector2<TNumber>.Dot(oc, direction);
            TNumber c = Vector2<TNumber>.Dot(oc, oc) - (circle.Radius * circle.Radius);
            TNumber discriminant = (b * b) - ((TNumber.One + TNumber.One) * a * c);

            if (discriminant < TNumber.Zero)
            {
                t = TNumber.Zero;
                point = segment.Start;
                return false;
            }

            TNumber sqrt = TNumber.Sqrt(discriminant);
            TNumber twoA = (TNumber.One + TNumber.One) * a;

            TNumber t0 = (-b - sqrt) / twoA;
            TNumber t1 = (-b + sqrt) / twoA;

            t = t0;
            if (t < TNumber.Zero || t > TNumber.One)
            {
                t = t1;
                if (t < TNumber.Zero || t > TNumber.One)
                {
                    point = segment.Start;
                    return false;
                }
            }

            point = segment.Start + (direction * t);
            return true;
        }
    }

    /// <summary>
    /// Line segment extensions that require square-root support.
    /// </summary>
    extension<TNumber>(LineSegment2D<TNumber> line)
        where TNumber : INumber<TNumber>, IRootFunctions<TNumber>
    {
        /// <summary>
        /// Gets the length of the line segment.
        /// </summary>
        /// <returns>The line segment length.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TNumber Length() => TNumber.Sqrt(line.LengthSquared());
    }

    extension<TNumber>(Rectangle<TNumber> rectangle)
        where TNumber : IFloatingPointIeee754<TNumber>
    {
        /// <summary>
        /// Checks whether the rectangle intersects a ray.
        /// </summary>
        /// <param name="ray">The ray to test.</param>
        /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
        [Pure]
        public bool Intersects(Ray2D<TNumber> ray)
        {
            TNumber tMin = TNumber.NegativeInfinity;
            TNumber tMax = TNumber.PositiveInfinity;

            if (TNumber.Abs(ray.Direction.X) < TNumber.Epsilon)
            {
                if (ray.Origin.X < rectangle.Left || ray.Origin.X > rectangle.Right)
                {
                    return false;
                }
            }
            else
            {
                TNumber tx1 = (rectangle.Left - ray.Origin.X) / ray.Direction.X;
                TNumber tx2 = (rectangle.Right - ray.Origin.X) / ray.Direction.X;
                tMin = TNumber.Max(tMin, TNumber.Min(tx1, tx2));
                tMax = TNumber.Min(tMax, TNumber.Max(tx1, tx2));
            }

            if (TNumber.Abs(ray.Direction.Y) < TNumber.Epsilon)
            {
                if (ray.Origin.Y < rectangle.Top || ray.Origin.Y > rectangle.Bottom)
                {
                    return false;
                }
            }
            else
            {
                TNumber ty1 = (rectangle.Top - ray.Origin.Y) / ray.Direction.Y;
                TNumber ty2 = (rectangle.Bottom - ray.Origin.Y) / ray.Direction.Y;
                tMin = TNumber.Max(tMin, TNumber.Min(ty1, ty2));
                tMax = TNumber.Min(tMax, TNumber.Max(ty1, ty2));
            }

            return tMax >= TNumber.Max(tMin, TNumber.Zero);
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

            if (TNumber.Abs(direction.X) < TNumber.Epsilon)
            {
                if (segment.Start.X < rectangle.Left || segment.Start.X > rectangle.Right)
                {
                    return false;
                }
            }
            else
            {
                TNumber tx1 = (rectangle.Left - segment.Start.X) / direction.X;
                TNumber tx2 = (rectangle.Right - segment.Start.X) / direction.X;
                tMin = TNumber.Max(tMin, TNumber.Min(tx1, tx2));
                tMax = TNumber.Min(tMax, TNumber.Max(tx1, tx2));
            }

            if (TNumber.Abs(direction.Y) < TNumber.Epsilon)
            {
                if (segment.Start.Y < rectangle.Top || segment.Start.Y > rectangle.Bottom)
                {
                    return false;
                }
            }
            else
            {
                TNumber ty1 = (rectangle.Top - segment.Start.Y) / direction.Y;
                TNumber ty2 = (rectangle.Bottom - segment.Start.Y) / direction.Y;
                tMin = TNumber.Max(tMin, TNumber.Min(ty1, ty2));
                tMax = TNumber.Min(tMax, TNumber.Max(ty1, ty2));
            }

            return tMax >= tMin;
        }

        /// <summary>
        /// Attempts to intersect a ray with the rectangle.
        /// </summary>
        /// <param name="ray">The ray to test.</param>
        /// <param name="t">The distance along the ray to the intersection point.</param>
        /// <param name="point">The intersection point.</param>
        /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
        [Pure]
        public bool TryIntersect(Ray2D<TNumber> ray, out TNumber t, out Point2D<TNumber> point)
        {
            TNumber tMin = TNumber.NegativeInfinity;
            TNumber tMax = TNumber.PositiveInfinity;

            if (TNumber.Abs(ray.Direction.X) < TNumber.Epsilon)
            {
                if (ray.Origin.X < rectangle.Left || ray.Origin.X > rectangle.Right)
                {
                    t = TNumber.Zero;
                    point = ray.Origin;
                    return false;
                }
            }
            else
            {
                TNumber tx1 = (rectangle.Left - ray.Origin.X) / ray.Direction.X;
                TNumber tx2 = (rectangle.Right - ray.Origin.X) / ray.Direction.X;
                tMin = TNumber.Max(tMin, TNumber.Min(tx1, tx2));
                tMax = TNumber.Min(tMax, TNumber.Max(tx1, tx2));
            }

            if (TNumber.Abs(ray.Direction.Y) < TNumber.Epsilon)
            {
                if (ray.Origin.Y < rectangle.Top || ray.Origin.Y > rectangle.Bottom)
                {
                    t = TNumber.Zero;
                    point = ray.Origin;
                    return false;
                }
            }
            else
            {
                TNumber ty1 = (rectangle.Top - ray.Origin.Y) / ray.Direction.Y;
                TNumber ty2 = (rectangle.Bottom - ray.Origin.Y) / ray.Direction.Y;
                tMin = TNumber.Max(tMin, TNumber.Min(ty1, ty2));
                tMax = TNumber.Min(tMax, TNumber.Max(ty1, ty2));
            }

            if (tMax < TNumber.Max(tMin, TNumber.Zero))
            {
                t = TNumber.Zero;
                point = ray.Origin;
                return false;
            }

            t = TNumber.Max(tMin, TNumber.Zero);
            point = ray.PointAt(t);
            return true;
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

            if (TNumber.Abs(direction.X) < TNumber.Epsilon)
            {
                if (segment.Start.X < rectangle.Left || segment.Start.X > rectangle.Right)
                {
                    t = TNumber.Zero;
                    point = segment.Start;
                    return false;
                }
            }
            else
            {
                TNumber tx1 = (rectangle.Left - segment.Start.X) / direction.X;
                TNumber tx2 = (rectangle.Right - segment.Start.X) / direction.X;
                tMin = TNumber.Max(tMin, TNumber.Min(tx1, tx2));
                tMax = TNumber.Min(tMax, TNumber.Max(tx1, tx2));
            }

            if (TNumber.Abs(direction.Y) < TNumber.Epsilon)
            {
                if (segment.Start.Y < rectangle.Top || segment.Start.Y > rectangle.Bottom)
                {
                    t = TNumber.Zero;
                    point = segment.Start;
                    return false;
                }
            }
            else
            {
                TNumber ty1 = (rectangle.Top - segment.Start.Y) / direction.Y;
                TNumber ty2 = (rectangle.Bottom - segment.Start.Y) / direction.Y;
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

        /// <summary>
        /// Attempts to intersect a ray with the rectangle and returns the hit normal.
        /// </summary>
        /// <param name="ray">The ray to test.</param>
        /// <param name="t">The distance along the ray to the intersection point.</param>
        /// <param name="point">The intersection point.</param>
        /// <param name="normal">The surface normal at the intersection point.</param>
        /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
        [Pure]
        public bool TryIntersect(
            Ray2D<TNumber> ray,
            out TNumber t,
            out Point2D<TNumber> point,
            out Vector2<TNumber> normal
        )
        {
            if (!rectangle.TryIntersect(ray, out t, out point))
            {
                normal = Vector2<TNumber>.Zero;
                return false;
            }

            normal = Rectangle<TNumber>.GetRectangleNormal(rectangle, point);
            return true;
        }

        /// <summary>
        /// Attempts to intersect a line segment with the rectangle and returns the hit normal.
        /// </summary>
        /// <param name="segment">The segment to test.</param>
        /// <param name="t">The normalized distance along the segment to the intersection point.</param>
        /// <param name="point">The intersection point.</param>
        /// <param name="normal">The surface normal at the intersection point.</param>
        /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
        [Pure]
        public bool TryIntersect(
            LineSegment2D<TNumber> segment,
            out TNumber t,
            out Point2D<TNumber> point,
            out Vector2<TNumber> normal
        )
        {
            if (!rectangle.TryIntersect(segment, out t, out point))
            {
                normal = Vector2<TNumber>.Zero;
                return false;
            }

            normal = Rectangle<TNumber>.GetRectangleNormal(rectangle, point);
            return true;
        }

        private static Vector2<TNumber> GetRectangleNormal(Rectangle<TNumber> rect, Point2D<TNumber> point)
        {
            TNumber leftDist = TNumber.Abs(point.X - rect.Left);
            TNumber rightDist = TNumber.Abs(point.X - rect.Right);
            TNumber topDist = TNumber.Abs(point.Y - rect.Top);
            TNumber bottomDist = TNumber.Abs(point.Y - rect.Bottom);

            TNumber min = TNumber.Min(TNumber.Min(leftDist, rightDist), TNumber.Min(topDist, bottomDist));

            if (TNumber.Abs(min - leftDist) < TNumber.Epsilon)
            {
                return new Vector2<TNumber>(-TNumber.One, TNumber.Zero);
            }

            if (TNumber.Abs(min - rightDist) < TNumber.Epsilon)
            {
                return new Vector2<TNumber>(TNumber.One, TNumber.Zero);
            }

            if (TNumber.Abs(min - topDist) < TNumber.Epsilon)
            {
                return new Vector2<TNumber>(TNumber.Zero, -TNumber.One);
            }

            return new Vector2<TNumber>(TNumber.Zero, TNumber.One);
        }
    }

    extension<TNumber>(Aabb2D<TNumber> aabb)
        where TNumber : IFloatingPointIeee754<TNumber>
    {
        /// <summary>
        /// Checks whether the bounding box intersects a ray.
        /// </summary>
        /// <param name="ray">The ray to test.</param>
        /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
        [Pure]
        public bool Intersects(Ray2D<TNumber> ray)
        {
            Rectangle<TNumber> rectangle = aabb.ToRectangle();
            return rectangle.Intersects(ray);
        }

        /// <summary>
        /// Checks whether the bounding box intersects a line segment.
        /// </summary>
        /// <param name="segment">The segment to test.</param>
        /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
        [Pure]
        public bool Intersects(LineSegment2D<TNumber> segment)
        {
            Rectangle<TNumber> rectangle = aabb.ToRectangle();
            return rectangle.Intersects(segment);
        }

        /// <summary>
        /// Attempts to intersect a ray with the bounding box.
        /// </summary>
        /// <param name="ray">The ray to test.</param>
        /// <param name="t">The distance along the ray to the intersection point.</param>
        /// <param name="point">The intersection point.</param>
        /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
        [Pure]
        public bool TryIntersect(Ray2D<TNumber> ray, out TNumber t, out Point2D<TNumber> point)
        {
            Rectangle<TNumber> rectangle = aabb.ToRectangle();
            return rectangle.TryIntersect(ray, out t, out point);
        }

        /// <summary>
        /// Attempts to intersect a line segment with the bounding box.
        /// </summary>
        /// <param name="segment">The segment to test.</param>
        /// <param name="t">The normalized distance along the segment to the intersection point.</param>
        /// <param name="point">The intersection point.</param>
        /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
        [Pure]
        public bool TryIntersect(LineSegment2D<TNumber> segment, out TNumber t, out Point2D<TNumber> point)
        {
            Rectangle<TNumber> rectangle = aabb.ToRectangle();
            return rectangle.TryIntersect(segment, out t, out point);
        }

        /// <summary>
        /// Attempts to intersect a ray with the bounding box and returns the hit normal.
        /// </summary>
        /// <param name="ray">The ray to test.</param>
        /// <param name="t">The distance along the ray to the intersection point.</param>
        /// <param name="point">The intersection point.</param>
        /// <param name="normal">The surface normal at the intersection point.</param>
        /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
        [Pure]
        public bool TryIntersect(
            Ray2D<TNumber> ray,
            out TNumber t,
            out Point2D<TNumber> point,
            out Vector2<TNumber> normal
        )
        {
            Rectangle<TNumber> rectangle = aabb.ToRectangle();
            if (!rectangle.TryIntersect(ray, out t, out point))
            {
                normal = Vector2<TNumber>.Zero;
                return false;
            }

            normal = Aabb2D<TNumber>.GetAabbNormal(rectangle, point);
            return true;
        }

        /// <summary>
        /// Attempts to intersect a line segment with the bounding box and returns the hit normal.
        /// </summary>
        /// <param name="segment">The segment to test.</param>
        /// <param name="t">The normalized distance along the segment to the intersection point.</param>
        /// <param name="point">The intersection point.</param>
        /// <param name="normal">The surface normal at the intersection point.</param>
        /// <returns><see langword="true"/> if they intersect; otherwise <see langword="false"/>.</returns>
        [Pure]
        public bool TryIntersect(
            LineSegment2D<TNumber> segment,
            out TNumber t,
            out Point2D<TNumber> point,
            out Vector2<TNumber> normal
        )
        {
            Rectangle<TNumber> rectangle = aabb.ToRectangle();
            if (!rectangle.TryIntersect(segment, out t, out point))
            {
                normal = Vector2<TNumber>.Zero;
                return false;
            }

            normal = Aabb2D<TNumber>.GetAabbNormal(rectangle, point);
            return true;
        }

        private static Vector2<TNumber> GetAabbNormal(Rectangle<TNumber> rectangle, Point2D<TNumber> point)
        {
            TNumber leftDist = TNumber.Abs(point.X - rectangle.Left);
            TNumber rightDist = TNumber.Abs(point.X - rectangle.Right);
            TNumber topDist = TNumber.Abs(point.Y - rectangle.Top);
            TNumber bottomDist = TNumber.Abs(point.Y - rectangle.Bottom);

            TNumber min = TNumber.Min(TNumber.Min(leftDist, rightDist), TNumber.Min(topDist, bottomDist));

            if (TNumber.Abs(min - leftDist) < TNumber.Epsilon)
            {
                return new Vector2<TNumber>(-TNumber.One, TNumber.Zero);
            }

            if (TNumber.Abs(min - rightDist) < TNumber.Epsilon)
            {
                return new Vector2<TNumber>(TNumber.One, TNumber.Zero);
            }

            if (TNumber.Abs(min - topDist) < TNumber.Epsilon)
            {
                return new Vector2<TNumber>(TNumber.Zero, -TNumber.One);
            }

            return new Vector2<TNumber>(TNumber.Zero, TNumber.One);
        }
    }
}
