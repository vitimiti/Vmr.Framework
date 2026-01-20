// A small utility tool to update and generate badges for the roadmap in our README.md file
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

using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

const string readmeFile = "README.md";
const string badgesDir = "badges";

var repoSlug = TryGetRepoSlug() ?? "<OWNER>/<REPO>";
var rawBaseUrl = $"https://raw.githubusercontent.com/{repoSlug}/main";

var jsonOptions = new JsonSerializerOptions { WriteIndented = true };

if (!File.Exists(readmeFile))
{
    await Console.Error.WriteLineAsync($"{readmeFile} not found.");
    return;
}

Directory.CreateDirectory(badgesDir);
var linesWaited = await File.ReadAllLinesAsync(readmeFile);
List<string> lines = linesWaited.ToList();
var roadmapStart = lines.FindIndex(l =>
    l.StartsWith("## ") && l.Contains("Roadmap", StringComparison.OrdinalIgnoreCase)
);

if (roadmapStart < 0)
{
    await Console.Error.WriteLineAsync("Roadmap section not found.");
    return;
}

var roadmapEnd = lines.FindIndex(roadmapStart + 1, l => l.StartsWith("## "));
if (roadmapEnd < 0)
{
    roadmapEnd = lines.Count;
}

for (var i = roadmapStart + 1; i < roadmapEnd; i++)
{
    if (!lines[i].StartsWith("### "))
    {
        continue;
    }

    var headerLine = lines[i];
    var headerText = headerLine[4..].Trim();
    var sectionName = headerText.Split(" - ")[0].Trim();

    var sectionEnd = lines.FindIndex(i + 1, l => l.StartsWith("### ") || l.StartsWith("## "));
    if (sectionEnd < 0 || sectionEnd > roadmapEnd)
    {
        sectionEnd = roadmapEnd;
    }

    var total = 0;
    var done = 0;
    for (var j = i + 1; j < sectionEnd; j++)
    {
        Match match = StateRegex().Match(lines[j]);
        if (!match.Success)
        {
            continue;
        }

        total++;
        if (match.Groups["state"].Value.Equals("x", StringComparison.InvariantCultureIgnoreCase))
        {
            done++;
        }
    }

    var percent = total == 0 ? 0 : (int)float.Round(done * 100F / total, MidpointRounding.AwayFromZero);
    percent = int.Clamp(percent, 0, 100);

    var updateHeader = PercentRegex().Replace(headerLine, "");
    lines[i] = $"{updateHeader} - `{percent}%`";

    var badgeIndex = i + 1;
    var badgeJsonUrl = $"{rawBaseUrl}/badges/{Slugify(sectionName)}.json";
    var expectedBadgeLine =
        $"![{sectionName}](https://img.shields.io/endpoint?url={Uri.EscapeDataString(badgeJsonUrl)})";

    if (badgeIndex >= sectionEnd || !lines[badgeIndex].StartsWith("!["))
    {
        lines.Insert(badgeIndex, expectedBadgeLine);
        roadmapEnd++;
        sectionEnd++;
    }
    else
    {
        lines[badgeIndex] = expectedBadgeLine;
    }

    var barIndex = badgeIndex + 1;

    var bar = BuildBar(percent);
    var barLine = $"`[{bar}] {percent}%`";

    if (barIndex < sectionEnd && ProgressBarRegex().IsMatch(lines[barIndex]))
    {
        lines[barIndex] = barLine;
    }
    else
    {
        lines.Insert(barIndex, barLine);
        roadmapEnd++;
    }

    WriteBadgeFile(sectionName, percent);
}

await File.WriteAllLinesAsync(readmeFile, lines);

return;

string BuildBar(int percent)
{
    var filled = (int)Math.Round(percent / 10.0, MidpointRounding.AwayFromZero);
    filled = Math.Clamp(filled, 0, 10);
    return new string('█', filled) + new string('░', 10 - filled);
}

string Slugify(string input)
{
    var lower = input.ToLowerInvariant();
    var slug = SlugRegex().Replace(lower, "-");
    return FinalSlugRegex().Replace(slug, "");
}

string? TryGetRepoSlug()
{
    try
    {
        var info = new ProcessStartInfo("git", "config --get remote.origin.url")
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        using var process = Process.Start(info);
        if (process is null)
        {
            return null;
        }

        var output = process.StandardOutput.ReadToEnd().Trim();
        process.WaitForExit();

        if (string.IsNullOrWhiteSpace(output))
        {
            return null;
        }

        // Supports: https://github.com/owner/repo(.git) and git@github.com:owner/repo(.git)
        var match = RepoSlugRegex().Match(output);
        return match.Success ? match.Groups["slug"].Value : null;
    }
    catch
    {
        return null;
    }
}

void WriteBadgeFile(string sectionName, int percent)
{
    var slug = Slugify(sectionName);
    var color = percent switch
    {
        0 => "lightgrey",
        < 50 => "yellow",
        < 100 => "orange",
        _ => "brightgreen",
    };

    var badge = new
    {
        schemaVersion = 1,
        label = sectionName,
        message = $"{percent}%",
        color,
    };

    var json = JsonSerializer.Serialize(badge, jsonOptions);
    File.WriteAllText(Path.Combine(badgesDir, $"{slug}.json"), json, Encoding.UTF8);
}

internal static partial class Program
{
    [GeneratedRegex(@"^- \[(?<state>[xX ])\]")]
    private static partial Regex StateRegex();

    [GeneratedRegex(@" - `\d+%`")]
    private static partial Regex PercentRegex();

    [GeneratedRegex(@"^`\[[█░]+\] \d+%`$")]
    private static partial Regex ProgressBarRegex();

    [GeneratedRegex(@"[^a-z0-9]+")]
    private static partial Regex SlugRegex();

    [GeneratedRegex(@"^-+|-+$")]
    private static partial Regex FinalSlugRegex();

    [GeneratedRegex(@"(?:github\.com[:/])(?<slug>[^/\s]+/[^/\s]+?)(?:\.git)?$")]
    private static partial Regex RepoSlugRegex();
}
