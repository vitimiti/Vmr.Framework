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
/// Parses command-line arguments with case-insensitive option names.
/// </summary>
public static class CommandLineParser
{
    /// <summary>
    /// Parses command-line arguments using the provided option definitions.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    /// <param name="options">The recognized options.</param>
    /// <returns>The parsed results.</returns>
    public static CommandLineResult Parse(string[] args, IEnumerable<CommandLineOption> options)
    {
        var optionMap = new Dictionary<string, CommandLineOption>(StringComparer.OrdinalIgnoreCase);
        foreach (CommandLineOption option in options)
        {
            optionMap[option.Name] = option;
            if (!string.IsNullOrWhiteSpace(option.ShortName))
            {
                optionMap[option.ShortName] = option;
            }
        }

        var values = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        foreach (CommandLineOption option in optionMap.Values.Where(o => o.DefaultValue is not null))
        {
            values[option.Name] = option.DefaultValue;
        }

        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i];

            if (arg.StartsWith("--", StringComparison.Ordinal))
            {
                ParseLongOption(arg[2..], args, ref i, optionMap, values);
            }
            else if (arg.StartsWith('-') && arg.Length > 1)
            {
                ParseShortOption(arg[1..], args, ref i, optionMap, values);
            }
        }

        return new CommandLineResult(values);
    }

    private static void ParseLongOption(
        string token,
        string[] args,
        ref int index,
        Dictionary<string, CommandLineOption> optionMap,
        Dictionary<string, string?> values
    )
    {
        var name = token;
        string? value = null;

        var equalsIndex = token.IndexOf('=', StringComparison.Ordinal);
        if (equalsIndex >= 0)
        {
            name = token[..equalsIndex];
            value = token[(equalsIndex + 1)..];
        }

        if (!optionMap.TryGetValue(name, out CommandLineOption? option))
        {
            return;
        }

        if (option.ExpectsValue && value is null && index + 1 < args.Length)
        {
            value = args[++index];
        }

        values[option.Name] = option.ExpectsValue ? value ?? string.Empty : string.Empty;
    }

    private static void ParseShortOption(
        string token,
        string[] args,
        ref int index,
        Dictionary<string, CommandLineOption> optionMap,
        Dictionary<string, string?> values
    )
    {
        if (!optionMap.TryGetValue(token, out CommandLineOption? option))
        {
            return;
        }

        string? value = null;
        if (option.ExpectsValue && index + 1 < args.Length)
        {
            value = args[++index];
        }

        values[option.Name] = option.ExpectsValue ? value ?? string.Empty : string.Empty;
    }
}
