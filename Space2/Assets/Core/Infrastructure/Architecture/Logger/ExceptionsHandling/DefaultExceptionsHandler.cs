using System;
using Assets.Infrastructure.Architecture.Modulux;

namespace Assets.Infrastructure.Architecture.Logger.ExceptionsHandling
{
    /// <summary>
    /// Default catcher of unhandled exceptions. Logs to Modulux logger.
    /// </summary>
    public static class DefaultExceptionsHandler
    {
        private static bool _isInitialized;

        public static void Initialize()
        {
            if (_isInitialized)
                return;

            _isInitialized = true;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ModuluxRoot.Logger.LogException((Exception) e.ExceptionObject);
        }
    }
}