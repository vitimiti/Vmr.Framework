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
/// Represents a 2D size with generic numeric dimensions.
/// </summary>
/// <typeparam name="TNumber">The numeric type used for dimensions.</typeparam>
[SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "By design.")]
public record struct Size2D<TNumber>(TNumber Width, TNumber Height)
    where TNumber : INumber<TNumber>
{
    /// <summary>
    /// Gets the size with both dimensions set to zero.
    /// </summary>
    [Pure]
    public static Size2D<TNumber> Zero => new(TNumber.Zero, TNumber.Zero);

    /// <summary>
    /// Gets a value indicating whether the size has a non-positive width or height.
    /// </summary>
    [Pure]
    public bool IsEmpty => Width <= TNumber.Zero || Height <= TNumber.Zero;

    /// <summary>
    /// Computes the area of the size.
    /// </summary>
    /// <returns>The area.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TNumber Area() => Width * Height;
}
