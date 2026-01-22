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

namespace Vmr.Framework.Video;

/// <summary>
/// Represents a cross-platform window that delegates to a platform-specific implementation.
/// </summary>
/// <param name="title">The window title.</param>
/// <param name="bounds">The window bounds in screen coordinates.</param>
/// <param name="fullScreen">Whether the window should be created in fullscreen mode.</param>
public sealed class Window(string title, Rectangle<int> bounds, bool fullScreen) : IDisposable
{
    private readonly IPlatformWindow _platformWindow = WindowFactory.CreatePlatformWindow(title, bounds, fullScreen);

    private bool _disposed;

    /// <summary>
    /// Initializes a new window with the specified title and size.
    /// </summary>
    /// <param name="title">The window title.</param>
    /// <param name="size">The window size.</param>
    /// <param name="fullScreen">Whether the window should be created in fullscreen mode.</param>
    public Window(string title, Size2D<int> size, bool fullScreen)
        : this(title, new Rectangle<int>(0, 0, size.Width, size.Height), fullScreen) { }

    /// <summary>
    /// Finalizes an instance of the <see cref="Window"/> class.
    /// </summary>
    ~Window() => Dispose(disposing: false);

    /// <summary>
    /// Gets or sets the window title.
    /// </summary>
    public string Title
    {
        get => _platformWindow.Title;
        set => _platformWindow.Title = value;
    }

    /// <summary>
    /// Gets or sets the window bounds.
    /// </summary>
    public Rectangle<int> Bounds
    {
        get => _platformWindow.Bounds;
        set => _platformWindow.Bounds = value;
    }

    /// <summary>
    /// Gets or sets whether the window is in fullscreen mode.
    /// </summary>
    public bool IsFullscreen
    {
        get => _platformWindow.IsFullscreen;
        set => _platformWindow.IsFullscreen = value;
    }

    /// <summary>
    /// Gets whether the window is currently open.
    /// </summary>
    public bool IsOpen => _platformWindow.IsOpen;

    /// <summary>
    /// Shows the window.
    /// </summary>
    public void Show() => _platformWindow.Show();

    /// <summary>
    /// Closes the window.
    /// </summary>
    public void Close() => _platformWindow.Close();

    /// <summary>
    /// Processes pending window events.
    /// </summary>
    public void ProcessEvents() => _platformWindow.ProcessEvents();

    /// <summary>
    /// Releases window resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _platformWindow.Dispose();
        }

        _disposed = true;
    }
}
