using Assets.Infrastructure.Architecture.Redux;
using System;
using UniRx;
using Assets.Infrastructure.Architecture.Logger;
using JsonUtility = UnityEngine.JsonUtility;

namespace Assets.Infrastructure.Architecture.Redux.Middlewares
{
    /// <summary>
    /// Middleware for loggin actions and state
    /// </summary>
	public class LoggerMiddleware
	{
		public ILogger Logger;
		public LoggerMiddleware(ILogger logger)
		{
			this.Logger = logger;
		}

		public Func<Dispatcher, Dispatcher> Middleware<TState>(IStore<TState> store)
		{
			return (Dispatcher next) => (IAction action) =>
			{
				var jsonState = JsonUtility.ToJson(store.GetState());
				var actionType = action.GetType();
				Logger.Log("actionType: " + actionType + "\ncurrentState: " + jsonState + "\n====");

				return next(action);
			};
		}
	}
}

