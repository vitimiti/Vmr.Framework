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

namespace Vmr.Framework.Time;

/// <summary>
/// Represents a snapshot of game timing information for a single frame.
/// </summary>
/// <param name="Total">Total time elapsed since the clock started.</param>
/// <param name="Delta">Time elapsed since the previous frame.</param>
/// <param name="FrameIndex">Sequential frame number produced by the clock.</param>
public record struct GameTime(TimeSpan Total, TimeSpan Delta, long FrameIndex)
{
    /// <summary>
    /// Gets a zeroed time snapshot with no elapsed time.
    /// </summary>
    [Pure]
    public static GameTime Zero => new(TimeSpan.Zero, TimeSpan.Zero, 0);

    /// <summary>
    /// Gets the total elapsed time in seconds.
    /// </summary>
    [Pure]
    public double TotalSeconds => Total.TotalSeconds;

    /// <summary>
    /// Gets the elapsed time between frames in seconds.
    /// </summary>
    [Pure]
    public double DeltaSeconds => Delta.TotalSeconds;
}
