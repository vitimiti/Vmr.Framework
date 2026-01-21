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
            var length = vector.Length();
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
            var cos = TNumber.Cos(radians);
            var sin = TNumber.Sin(radians);
            return new Vector2<TNumber>((vector.X * cos) - (vector.Y * sin), (vector.X * sin) + (vector.Y * cos));
        }
    }
}
