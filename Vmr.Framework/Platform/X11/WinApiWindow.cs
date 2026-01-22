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
using Vmr.Framework.NativeHandles;
using Vmr.Framework.Platform.NativeHandles;

namespace Vmr.Framework.Platform.X11;

internal sealed class X11Window : IPlatformWindow
{
    private readonly X11WindowSafeHandle _handle = new();

    private bool _disposed;

    ~X11Window() => Dispose(disposing: false);

    public X11Window(string title, Rectangle<int> bounds, bool fullScreen)
    {
        Title = title;
        Bounds = bounds;
        IsFullscreen = fullScreen;
        IsOpen = true;
    }

    public string Title { get; set; }

    public Rectangle<int> Bounds { get; set; }

    public bool IsFullscreen { get; set; }

    public bool IsOpen { get; private set; }

    public WindowSafeHandle Handle => _handle;

    public void Show() { }

    public void Close() => IsOpen = false;

    public void ProcessEvents() { }

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
            IsOpen = false;
            _handle.Dispose();
        }

        _disposed = true;
    }
}
