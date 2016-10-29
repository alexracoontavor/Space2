namespace Assets.Infrastructure.CoreTools.Instanced.FileSystemAccessor
{
    /// <summary>
    /// Files access interface
    /// </summary>
    public interface IFileSystemAccessor
    {
        /// <summary>
        /// Loads and returns string data
        /// </summary>
        /// <param name="filename">filename to load from</param>
        /// <returns>loaded string</returns>
        string LoadStringData(string filename);

        /// <summary>
        /// Loads and returns any serializable data
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="filename">filename to load from</param>
        /// <returns>loaded data</returns>
        T LoadData<T>(string filename);

        /// <summary>
        /// Saves data (string or any serializable)
        /// </summary>
        /// <typeparam name="T">Type of data to save</typeparam>
        /// <param name="data">data to save</param>
        /// <param name="filename">filename to save to</param>
        void SaveData<T>(T data, string filename);
    }
}