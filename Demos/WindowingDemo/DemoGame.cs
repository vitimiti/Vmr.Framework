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

using Vmr.Framework;
using Vmr.Framework.Logging;
using Vmr.Framework.Math;
using Vmr.Framework.Video;

namespace WindowingDemo;

internal sealed class DemoGame : GameBase
{
    private Window? _window;

    protected override void Initialize()
    {
        Logger.Info($"{nameof(DemoGame)}:{nameof(Initialize)}", "Windowing demo starting.");

        _window = new Window(
            title: "Vmr.Framework - Windowing Demo",
            size: new Size2D<int>(1270, 720),
            fullScreen: false
        );

        _window.Show();
    }

    protected override void Update(Vmr.Framework.Time.GameTime time)
    {
        _window?.ProcessEvents();

        if (time.TotalSeconds >= 2.0)
        {
            RequestExit();
        }
    }

    protected override void Draw(Vmr.Framework.Time.GameTime time)
    {
        // No rendering yet.
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _window?.Dispose();
        }

        base.Dispose(disposing);
    }
}
