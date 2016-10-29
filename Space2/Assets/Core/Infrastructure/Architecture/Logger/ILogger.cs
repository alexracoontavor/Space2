using System;

/// <summary>
/// All things relating to logging
/// </summary>
namespace Assets.Infrastructure.Architecture.Logger
{
    /// <summary>
    /// Basic Logger interface
    /// </summary>
    public interface ILogger
    {
        void Log(string text, string[] tags = null);
        void LogError(string text, string[] tags = null);
        void LogWarning(string text, string[] tags = null);
        void LogException(Exception exception);
    }
}