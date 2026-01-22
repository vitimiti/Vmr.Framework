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
/// Represents parsed command-line values.
/// </summary>
public sealed class CommandLineResult
{
    private readonly Dictionary<string, string?> _values;

    internal CommandLineResult(Dictionary<string, string?> values) => _values = values;

    /// <summary>
    /// Gets the raw value for the specified option name.
    /// </summary>
    /// <param name="name">The option name.</param>
    /// <returns>The raw value, or <see langword="null"/> if the option is missing.</returns>
    public string? GetValue(string name) => _values.GetValueOrDefault(name);

    /// <summary>
    /// Gets a value parsed as an enum, or a default when parsing fails or the option is missing.
    /// </summary>
    /// <param name="name">The option name.</param>
    /// <param name="defaultValue">The default value to return when parsing fails.</param>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <returns>The parsed enum value.</returns>
    public TEnum GetEnum<TEnum>(string name, TEnum defaultValue)
        where TEnum : struct, Enum
    {
        var value = GetValue(name);
        return value is null || !Enum.TryParse(value, ignoreCase: true, out TEnum parsed) ? defaultValue : parsed;
    }

    /// <summary>
    /// Gets a boolean flag value.
    /// </summary>
    /// <param name="name">The option name.</param>
    /// <returns><see langword="true"/> if the flag is present; otherwise <see langword="false"/>.</returns>
    public bool GetFlag(string name)
    {
        var value = GetValue(name);
        if (value is null)
        {
            return false;
        }

        if (value.Length == 0)
        {
            return true;
        }

        return bool.TryParse(value, out var parsed) && parsed;
    }
}
