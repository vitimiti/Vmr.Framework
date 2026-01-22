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

using Vmr.Framework.CommandLine;
using Vmr.Framework.Logging;
using Vmr.Framework.Time;

namespace Vmr.Framework;

/// <summary>
/// Provides a base class for games built on Vmr.Framework, enforcing a standard lifecycle.
/// </summary>
public abstract class GameBase : IDisposable
{
    private readonly GameClock _clock = new();
    private bool _exitRequested;
    private bool _disposed;

    /// <summary>
    /// Finalizes an instance of the <see cref="GameBase"/> class.
    /// </summary>
    ~GameBase() => Dispose(disposing: false);

    /// <summary>
    /// Runs the game loop using the provided command-line arguments.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    public void Run(string[] args)
    {
        ConfigureLogging(args);
        Logger.Info($"{nameof(GameBase)}:{nameof(Run)}", "Starting game loop.");

        try
        {
            Logger.Debug($"{nameof(GameBase)}:{nameof(Initialize)}", "Initializing game.");
            Initialize();
            while (!_exitRequested)
            {
                GameTime time = _clock.Tick();
                Logger.Trace($"{nameof(GameBase)}:{nameof(Update)}", "Update frame start.");
                Update(time);
                Logger.Trace($"{nameof(GameBase)}:{nameof(Draw)}", "Draw frame start.");
                Draw(time);
            }

            Logger.Info($"{nameof(GameBase)}:{nameof(Run)}", "Exit requested. Ending game loop.");
        }
        finally
        {
            Logger.Debug($"{nameof(GameBase)}:{nameof(Dispose)}", "Disposing game resources.");
            Logger.Shutdown();
            Dispose();
        }
    }

    /// <summary>
    /// Requests that the game loop exits after the current frame.
    /// </summary>
    protected void RequestExit()
    {
        Logger.Info($"{nameof(GameBase)}:{nameof(RequestExit)}", "Exit requested by game.");
        _exitRequested = true;
    }

    /// <summary>
    /// Initializes game state and systems.
    /// </summary>
    protected virtual void Initialize() { }

    /// <summary>
    /// Updates game logic.
    /// </summary>
    /// <param name="time">Timing information for the current frame.</param>
    protected abstract void Update(GameTime time);

    /// <summary>
    /// Draws the current frame.
    /// </summary>
    /// <param name="time">Timing information for the current frame.</param>
    protected virtual void Draw(GameTime time) { }

    /// <summary>
    /// Releases resources used by the game.
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases resources used by the game.
    /// </summary>
    /// <param name="disposing">Whether the method is called from <see cref="Dispose()"/>.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            // Managed cleanup hooks for derived classes.
        }

        _disposed = true;
    }

    private static void ConfigureLogging(string[] args)
    {
        CommandLineResult options = CommandLineParser.Parse(args, CommandLineDefaults.LoggingOptions);

        LogLevel level = options.GetEnum("log-level", LogSettings.Default.Level);
        var logToFile = options.GetFlag("log-file");

        Logger.Configure(new LogSettings(level, logToFile));
        Logger.Info(
            $"{nameof(GameBase)}:{nameof(ConfigureLogging)}",
            $"Logging configured. Level={level}, File={logToFile}."
        );
    }
}
