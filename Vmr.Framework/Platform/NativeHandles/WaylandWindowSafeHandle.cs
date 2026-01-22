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

using Vmr.Framework.NativeHandles;

namespace Vmr.Framework.Platform.NativeHandles;

internal class WaylandWindowSafeHandle : WindowSafeHandle
{
    public WaylandWindowSafeHandle()
        : base(invalidHandleValue: nint.Zero, ownsHandle: true) => SetHandle(nint.Zero);

    public override bool IsInvalid => handle == nint.Zero;

    // TODO: Release the Wayland resources
    protected override bool ReleaseHandle() => true;
}
