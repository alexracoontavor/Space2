using System;
using System.Collections.Generic;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.Architecture.Modulux.State;
using Assets.Infrastructure.Architecture.Redux;
using NUnit.Framework;
using UniRx;

namespace Assets.Editor.Tests.Space2Module.Redux.Actions
{
    public class ActionsCollectingMiddleware
    {
        private readonly List<IAction> actions;
        private Subject<IAction> _subject = new Subject<IAction>();
        public IObservable<IAction> ActionsStream {get { return _subject.AsObservable(); } }

        public ActionsCollectingMiddleware(List<IAction> actions)
        {
            this.actions = actions;
        }

        public Func<Dispatcher, Dispatcher> Middleware<TState>(IStore<TState> store)
        {
            return (Dispatcher next) => (IAction action) =>
            {
                actions.Add(action);
                _subject.OnNext(action);
                return next(action);
            };
        }
    }

    public abstract class ActionsTestBase
    {
        protected ActionsCollectingMiddleware ActionsCollectingMiddleware;
        protected List<IAction> Actions = new List<IAction>();

        [SetUp]
        public void Setup()
        {
            Actions.Clear();
            ActionsCollectingMiddleware = new ActionsCollectingMiddleware(Actions);
            ModuluxRoot.Store = new Store<BaseState>((baseState, action)=>baseState, null, ActionsCollectingMiddleware.Middleware);
        }
    }

    /// <summary>
    /// This is an action representing an unhandled action in any reducer
    /// </summary>
    public class OtherAction : IAction
    {
        
    }
}