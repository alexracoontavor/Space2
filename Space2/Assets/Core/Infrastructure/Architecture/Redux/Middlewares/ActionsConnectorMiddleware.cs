using System;
using System.Collections.Generic;

namespace Assets.Infrastructure.Architecture.Redux.Middlewares
{
    /// <summary>
    /// Middleware for connecting actions from one module to another.
    /// </summary>
    public class ActionsConnectorMiddleware
    {
        private Dictionary<Type, Action<IAction>> _actionsMap;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="actionsMap">Dictionary that maps action Type to a function</param>
        public ActionsConnectorMiddleware(Dictionary<Type, Action<IAction>> actionsMap)
        {
            _actionsMap = actionsMap;
        }

        public Func<Dispatcher, Dispatcher> Middleware<TState>(IStore<TState> store)
        {
            return (Dispatcher next) => (IAction action) =>
            {
                if (_actionsMap.ContainsKey(action.GetType()))
                    _actionsMap[action.GetType()](action);

                return next(action);
            };
        }
    }
}