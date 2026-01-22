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

using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Vmr.Framework.Time;

/// <summary>
/// Provides high-resolution timing and produces <see cref="GameTime"/> snapshots each frame.
/// </summary>
public sealed class GameClock
{
    private readonly Stopwatch _stopwatch = new();

    private TimeSpan _lastElapsed;

    /// <summary>
    /// Gets whether the underlying stopwatch is running.
    /// </summary>
    [Pure]
    public bool IsRunning => _stopwatch.IsRunning;

    /// <summary>
    /// Gets the raw elapsed time measured by the stopwatch.
    /// </summary>
    [Pure]
    public TimeSpan Elapsed => _stopwatch.Elapsed;

    /// <summary>
    /// Gets the index of the next frame snapshot to be produced.
    /// </summary>
    [Pure]
    public long FrameIndex { get; private set; }

    /// <summary>
    /// Starts (or restarts) the clock and resets accumulated timing state.
    /// </summary>
    public void Start()
    {
        _stopwatch.Restart();
        _lastElapsed = TimeSpan.Zero;
        FrameIndex = 0;
    }

    /// <summary>
    /// Stops the clock without resetting accumulated state.
    /// </summary>
    public void Stop() => _stopwatch.Stop();

    /// <summary>
    /// Resets the clock and clears accumulated timing state.
    /// </summary>
    public void Reset()
    {
        _stopwatch.Reset();
        _lastElapsed = TimeSpan.Zero;
        FrameIndex = 0;
    }

    /// <summary>
    /// Advances the clock and returns a new <see cref="GameTime"/> snapshot.
    /// </summary>
    /// <returns>The timing snapshot for the current frame.</returns>
    [Pure]
    public GameTime Tick()
    {
        if (!_stopwatch.IsRunning)
        {
            _stopwatch.Start();
        }

        TimeSpan elapsed = _stopwatch.Elapsed;
        TimeSpan delta = elapsed - _lastElapsed;
        _lastElapsed = elapsed;

        var frame = FrameIndex++;
        return new GameTime(elapsed, delta, frame);
    }
}
