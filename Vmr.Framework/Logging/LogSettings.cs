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

namespace Vmr.Framework.Logging;

/// <summary>
/// Represents logger configuration options.
/// </summary>
/// <param name="Level">The minimum level that will be logged.</param>
/// <param name="EnableFileLogging">Whether to buffer logs to a file.</param>
public readonly record struct LogSettings(LogLevel Level, bool EnableFileLogging)
{
    /// <summary>
    /// Gets the default settings (Debug in Debug builds, Info in Release builds).
    /// </summary>
    public static LogSettings Default => new(GetDefaultLevel(), EnableFileLogging: false);

    private static LogLevel GetDefaultLevel()
    {
#if DEBUG
        return LogLevel.Debug;
#else
        return LogLevel.Info;
#endif
    }
}
