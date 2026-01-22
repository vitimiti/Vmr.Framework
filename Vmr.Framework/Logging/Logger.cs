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

using System.Globalization;

namespace Vmr.Framework.Logging;

/// <summary>
/// Provides game-focused logging to console and optional buffered file output.
/// </summary>
public static class Logger
{
    private static readonly Lock Sync = new();
    private static readonly List<string> FileBuffer = [];

    private static LogSettings _settings = LogSettings.Default;
    private static string? _logFilePath;

    /// <summary>
    /// Configures the logger.
    /// </summary>
    /// <param name="settings">The logger settings.</param>
    public static void Configure(LogSettings settings)
    {
        lock (Sync)
        {
            _settings = settings;

            if (_settings.EnableFileLogging)
            {
                _logFilePath = EnsureLogFilePath();
            }
            else
            {
                _logFilePath = null;
                FileBuffer.Clear();
            }
        }
    }

    /// <summary>
    /// Logs a message with the specified level and category.
    /// </summary>
    /// <param name="level">The log level.</param>
    /// <param name="category">The message category.</param>
    /// <param name="message">The message text.</param>
    public static void Log(LogLevel level, string category, string message)
    {
        if (level < _settings.Level || _settings.Level == LogLevel.None)
        {
            return;
        }

        var line = FormatLine(level, category, message);
        Console.WriteLine(line);

        if (!_settings.EnableFileLogging)
        {
            return;
        }

        lock (Sync)
        {
            FileBuffer.Add(line);
        }
    }

    /// <summary>
    /// Flushes buffered file logs to disk.
    /// </summary>
    public static void Flush()
    {
        if (!_settings.EnableFileLogging || _logFilePath is null)
        {
            return;
        }

        lock (Sync)
        {
            if (FileBuffer.Count == 0)
            {
                return;
            }

            File.AppendAllLines(_logFilePath, FileBuffer);
            FileBuffer.Clear();
        }
    }

    /// <summary>
    /// Flushes buffered logs and releases file logging resources.
    /// </summary>
    public static void Shutdown()
    {
        Flush();
        _logFilePath = null;
    }

    /// <summary>
    /// Logs a trace message.
    /// </summary>
    public static void Trace(string category, string message) => Log(LogLevel.Trace, category, message);

    /// <summary>
    /// Logs a debug message.
    /// </summary>
    public static void Debug(string category, string message) => Log(LogLevel.Debug, category, message);

    /// <summary>
    /// Logs an informational message.
    /// </summary>
    public static void Info(string category, string message) => Log(LogLevel.Info, category, message);

    /// <summary>
    /// Logs a warning message.
    /// </summary>
    public static void Warn(string category, string message) => Log(LogLevel.Warn, category, message);

    /// <summary>
    /// Logs an error message.
    /// </summary>
    public static void Error(string category, string message) => Log(LogLevel.Error, category, message);

    /// <summary>
    /// Logs a fatal error message.
    /// </summary>
    public static void Fatal(string category, string message) => Log(LogLevel.Fatal, category, message);

    private static string FormatLine(LogLevel level, string category, string message)
    {
        var time = DateTime.Now.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture);
        var safeCategory = string.IsNullOrWhiteSpace(category) ? "General" : category;
        return $"[{time}] [{level}] [{safeCategory}] {message}";
    }

    private static string EnsureLogFilePath()
    {
        var dataFolder = Environment.GetFolderPath(
            OperatingSystem.IsWindows()
                ? Environment.SpecialFolder.LocalApplicationData
                : Environment.SpecialFolder.ApplicationData
        );

        var logsFolder = Path.Combine(dataFolder, "logs");
        Directory.CreateDirectory(logsFolder);

        TrimOldLogs(logsFolder);

        var fileName = $"{DateTime.Now:yyyy-MM-dd}.log";
        return Path.Combine(logsFolder, fileName);
    }

    private static void TrimOldLogs(string logsFolder)
    {
        var files = Directory.GetFiles(logsFolder, "*.log", SearchOption.TopDirectoryOnly);
        Array.Sort(files, StringComparer.OrdinalIgnoreCase);
        Array.Reverse(files);

        for (var i = 5; i < files.Length; i++)
        {
            try
            {
                File.Delete(files[i]);
            }
            catch
            {
                // Best-effort cleanup; ignore failures.
            }
        }
    }
}
