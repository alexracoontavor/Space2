using System;
using System.Collections.Generic;
using Assets.Infrastructure.Architecture.Modulux;
using Assets.Infrastructure.Architecture.Redux;

namespace Assets.Scripts.Space2Module.Integration.ObjectsSandbox.Setup
{
    public static class Setup
    {
        public static void Reset()
        {
            ModuluxRoot.Initialize(new []
            {
                typeof(Space2Module)
            },
            new Dictionary<Type, Action<IAction>>());
        }
    }
}
