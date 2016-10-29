namespace Assets.Infrastructure.CoreTools.Instanced.FileSystemAccessor
{
    /// <summary>
    /// Default implementation of IFileSystemAccessor
    /// </summary>
    public class FileAccessWrapper : IFileSystemAccessor
    {
        public string LoadStringData(string filename)
        {
            return SerializedDataImporter.LoadStringData(filename);
        }

        public T LoadData<T>(string filename)
        {
            return SerializedDataImporter.LoadData<T>(filename);
        }

        public void SaveData<T>(T data, string filename)
        {
            SerializedDataImporter.SaveData(data, filename);
        }
    }
}