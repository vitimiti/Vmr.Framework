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

using System.Runtime.InteropServices;

namespace Vmr.Framework.NativeHandles;

/// <summary>
/// Represents a base safe handle for native window resources.
/// </summary>
/// <param name="invalidHandleValue">The handle value that represents an invalid handle.</param>
/// <param name="ownsHandle">Whether this instance owns the handle and should release it.</param>
public abstract class WindowSafeHandle(nint invalidHandleValue, bool ownsHandle)
    : SafeHandle(invalidHandleValue, ownsHandle);
