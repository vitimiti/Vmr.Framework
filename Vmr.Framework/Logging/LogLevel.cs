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
/// Defines the severity of a log message.
/// </summary>
/// <remarks>If the global log level is set to one of these, any level higher will also be logged.</remarks>
public enum LogLevel
{
    /// <summary>
    /// This is the most verbose level.
    /// </summary>
    Trace,

    /// <summary>
    /// This level will show debug logs.
    /// </summary>
    /// <remarks>This is the default in debug builds.</remarks>
    Debug,

    /// <summary>
    /// This level will show informational logs.
    /// </summary>
    /// <remarks>This is the default in release builds.</remarks>
    Info,

    /// <summary>
    /// This level will show warnings.
    /// </summary>
    Warn,

    /// <summary>
    /// This level will show errors.
    /// </summary>
    Error,

    /// <summary>
    /// This level will show fatal errors.
    /// </summary>
    Fatal,

    /// <summary>
    /// Disables logging.
    /// </summary>
    None,
}
