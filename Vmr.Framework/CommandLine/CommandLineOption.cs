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

namespace Vmr.Framework.CommandLine;

/// <summary>
/// Describes a command-line option.
/// </summary>
/// <param name="Name">The long name of the option.</param>
/// <param name="ShortName">The short name of the option.</param>
/// <param name="ExpectsValue">Whether the option expects a value.</param>
/// <param name="DefaultValue">The default value of the option.</param>
public sealed record CommandLineOption(string Name, string? ShortName, bool ExpectsValue, string? DefaultValue = null);
