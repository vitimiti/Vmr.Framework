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
    }
}
