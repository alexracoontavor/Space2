using Assets.Infrastructure.Architecture.Modulux.State;

namespace Assets.Infrastructure.Architecture.Modulux
{
    /// <summary>
    /// Helpers for modules and Modulux state hierarchy
    /// </summary>
    public static class ModuleHelper
    {
        /// <summary>
        /// Finds and returns the required state in a state hierarchy
        /// </summary>
        /// <typeparam name="T">Type of state to find</typeparam>
        /// <param name="s">State to search in</param>
        /// <param name="path">Path to state with Type T</param>
        /// <param name="pathIndex">Starting index of search</param>
        /// <returns></returns>
        public static BaseState ExtractState<T>(BaseState s, int[] path, int pathIndex = 0) where T : BaseState
        {
            while (true)
            {
                if (pathIndex >= path.Length - 1)
                    return s.Modules[path[pathIndex]] as T;
                if (s.Modules[path[pathIndex]] == null)
                    return null;
                s = s.Modules[path[pathIndex]];
                pathIndex++;
            }
        }
    }
}