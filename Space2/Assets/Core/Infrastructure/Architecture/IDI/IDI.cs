/// <summary>
/// Dependency Injection
/// </summary>
namespace Assets.Infrastructure.Architecture.IDI
{
    /// <summary>
    /// Interface for injection mechanisms
    /// </summary>
    public interface IDI
    {
        /// <summary>
        /// Binds from class to class
        /// </summary>
        /// <typeparam name="TFrom">Key class, normally an Interface</typeparam>
        /// <typeparam name="TTo">Value class</typeparam>
        /// <param name="isSingleton">if true, IoC will return TTo as singleton. Else as new instance for each request</param>
        /// <param name="filter">string filter to identify this binding</param>
        void Bind<TFrom, TTo>(bool isSingleton = true, string filter = null);

        /// <summary>
        /// Binds from object instance to object instance
        /// </summary>
        /// <param name="from">Key object</param>
        /// <param name="to">Value obejct</param>
        /// <param name="filter">string filter to identify this binding</param>
        void Bind(object from, object to, string filter = null);

        /// <summary>
        /// Gets class instance by type key
        /// </summary>
        /// <typeparam name="T">type key</typeparam>
        /// <param name="filter">optional string filter</param>
        /// <returns>Instance of class bound to T</returns>
        T Get<T>(string filter = null);
    }
}