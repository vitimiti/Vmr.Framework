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

using Vmr.Framework.Math;
using Vmr.Framework.Platform;
using Vmr.Framework.Platform.Wayland;
using Vmr.Framework.Platform.WinApi;
using Vmr.Framework.Platform.X11;

namespace Vmr.Framework.Video;

/// <summary>
/// Provides factory methods for creating platform-specific windows.
/// </summary>
public static class WindowFactory
{
    private const string PlatformNotSupported =
        "No supported windowing backend found for the current platform. Only Win32, Wayland and X11 are supported.";

    /// <summary>
    /// Creates a new window using the best available platform backend.
    /// </summary>
    /// <param name="title">The window title.</param>
    /// <param name="bounds">The window bounds in screen coordinates.</param>
    /// <param name="fullScreen">Whether the window should be created in fullscreen mode.</param>
    /// <returns>The created <see cref="Window"/> instance.</returns>
    /// <exception cref="PlatformNotSupportedException">
    /// Thrown when no supported windowing backend is available.
    /// </exception>
    public static Window Create(string title, Rectangle<int> bounds, bool fullScreen) => new(title, bounds, fullScreen);

    internal static IPlatformWindow CreatePlatformWindow(string title, Rectangle<int> bounds, bool fullScreen)
    {
        if (OperatingSystem.IsWindows())
        {
            return CreateWindowsWindow(title, bounds, fullScreen);
        }

        if (OperatingSystem.IsLinux())
        {
            return CreateLinuxWindow(title, bounds, fullScreen);
        }

        throw new PlatformNotSupportedException(PlatformNotSupported);
    }

    private static WinApiWindow CreateWindowsWindow(string title, Rectangle<int> bounds, bool fullScreen) =>
        new(title, bounds, fullScreen);

    private static IPlatformWindow CreateLinuxWindow(string title, Rectangle<int> bounds, bool fullScreen)
    {
        var sessionType = Environment.GetEnvironmentVariable("XDG_SESSION_TYPE");
        var waylandDisplay = Environment.GetEnvironmentVariable("WAYLAND_DISPLAY");

        if (
            string.Equals(sessionType, "wayland", StringComparison.OrdinalIgnoreCase)
            || !string.IsNullOrEmpty(waylandDisplay)
        )
        {
            return new WaylandWindow(title, bounds, fullScreen);
        }

        var display = Environment.GetEnvironmentVariable("DISPLAY");
        if (!string.IsNullOrEmpty(display))
        {
            return new X11Window(title, bounds, fullScreen);
        }

        throw new PlatformNotSupportedException(PlatformNotSupported);
    }
}
