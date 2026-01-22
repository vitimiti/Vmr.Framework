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
/// Represents a 2D vector with generic numeric components.
/// </summary>
/// <typeparam name="TNumber">The numeric type used for components.</typeparam>
[SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "By design.")]
public record struct Vector2<TNumber>(TNumber X, TNumber Y)
    where TNumber : INumber<TNumber>
{
    /// <summary>
    /// Gets the zero vector (0, 0).
    /// </summary>
    [Pure]
    public static Vector2<TNumber> Zero => new(TNumber.Zero, TNumber.Zero);

    /// <summary>
    /// Gets the vector (1, 1).
    /// </summary>
    [Pure]
    public static Vector2<TNumber> One => new(TNumber.One, TNumber.One);

    /// <summary>
    /// Gets the unit vector along the X axis.
    /// </summary>
    [Pure]
    public static Vector2<TNumber> UnitX => new(TNumber.One, TNumber.Zero);

    /// <summary>
    /// Gets the unit vector along the Y axis.
    /// </summary>
    [Pure]
    public static Vector2<TNumber> UnitY => new(TNumber.Zero, TNumber.One);

    /// <summary>
    /// Returns the squared length of a vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>The squared length.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TNumber LengthSquared(Vector2<TNumber> vector) => (vector.X * vector.X) + (vector.Y * vector.Y);

    /// <summary>
    /// Computes the dot product of two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The dot product.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TNumber Dot(Vector2<TNumber> left, Vector2<TNumber> right) => (left.X * right.X) + (left.Y * right.Y);

    /// <summary>
    /// Computes the 2D cross product (scalar) of two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The scalar cross product.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TNumber Cross(Vector2<TNumber> left, Vector2<TNumber> right) =>
        (left.X * right.Y) - (left.Y * right.X);

    /// <summary>
    /// Returns a vector perpendicular to the input vector, rotated 90 degrees counterclockwise.
    /// </summary>
    /// <param name="vector">The vector to rotate.</param>
    /// <returns>A perpendicular vector.</returns>
    [SuppressMessage(
        "Major Code Smell",
        "S2234:Arguments should be passed in the same order as the method parameters",
        Justification = "By design."
    )]
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<TNumber> Perpendicular(Vector2<TNumber> vector) => new(-vector.Y, vector.X);

    /// <summary>
    /// Linearly interpolates between two vectors.
    /// </summary>
    /// <param name="start">The start vector.</param>
    /// <param name="end">The end vector.</param>
    /// <param name="amount">The interpolation factor.</param>
    /// <returns>The interpolated vector.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<TNumber> Lerp(Vector2<TNumber> start, Vector2<TNumber> end, TNumber amount) =>
        new(start.X + ((end.X - start.X) * amount), start.Y + ((end.Y - start.Y) * amount));

    /// <summary>
    /// Adds two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The sum of the vectors.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<TNumber> operator +(Vector2<TNumber> left, Vector2<TNumber> right) =>
        new(left.X + right.X, left.Y + right.Y);

    /// <summary>
    /// Subtracts one vector from another.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The difference of the vectors.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<TNumber> operator -(Vector2<TNumber> left, Vector2<TNumber> right) =>
        new(left.X - right.X, left.Y - right.Y);

    /// <summary>
    /// Negates a vector.
    /// </summary>
    /// <param name="value">The vector to negate.</param>
    /// <returns>The negated vector.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<TNumber> operator -(Vector2<TNumber> value) => new(-value.X, -value.Y);

    /// <summary>
    /// Scales a vector by a scalar.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="scalar">The scale factor.</param>1
    /// <returns>The scaled vector.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<TNumber> operator *(Vector2<TNumber> vector, TNumber scalar) =>
        new(vector.X * scalar, vector.Y * scalar);

    /// <summary>
    /// Scales a vector by a scalar.
    /// </summary>
    /// <param name="scalar">The scale factor.</param>
    /// <param name="vector">The vector.</param>
    /// <returns>The scaled vector.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<TNumber> operator *(TNumber scalar, Vector2<TNumber> vector) =>
        new(vector.X * scalar, vector.Y * scalar);

    /// <summary>
    /// Divides a vector by a scalar.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="scalar">The divisor.</param>
    /// <returns>The scaled vector.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2<TNumber> operator /(Vector2<TNumber> vector, TNumber scalar) =>
        new(vector.X / scalar, vector.Y / scalar);

    /// <summary>
    /// Returns a vector with component-wise absolute values.
    /// </summary>
    /// <returns>The absolute vector.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2<TNumber> Abs() => new(TNumber.Abs(X), TNumber.Abs(Y));

    /// <summary>
    /// Clamps the vector between a minimum and maximum vector.
    /// </summary>
    /// <param name="min">The minimum vector.</param>
    /// <param name="max">The maximum vector.</param>
    /// <returns>The clamped vector.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2<TNumber> Clamp(Vector2<TNumber> min, Vector2<TNumber> max)
    {
        TNumber x = TNumber.Min(TNumber.Max(X, min.X), max.X);
        TNumber y = TNumber.Min(TNumber.Max(Y, min.Y), max.Y);
        return new Vector2<TNumber>(x, y);
    }
}
