using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Infrastructure.CoreTools.Instanced.FileSystemAccessor
{
    public class SerializedDataImporter
    {
        private static readonly string _dirName;

        static SerializedDataImporter()
        {
            _dirName = Application.persistentDataPath + "Data";
        }

        public static string LoadStringData(string filename)
        {
            string loaded;

            if (Directory.Exists(_dirName) && File.Exists(_dirName + "/" + filename))
            {
                loaded = File.ReadAllText(_dirName + "/" + filename);
            }
            else
            {
                loaded = LoadString(filename);
            }

            return loaded;
        }

        public static T LoadData<T>(string filename)
        {
            T loaded;

            if (Directory.Exists(_dirName) && File.Exists(_dirName + "/" + filename))
            {
                loaded = LoadFromHd<T>(filename);
            }
            else
            {
                loaded = Load<T>(filename);
            }

            return loaded;
        }

        public static void SaveData<T>(T data, string filename)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + filename);
            bf.Serialize(file, data);
            file.Close();
        }

        private static T LoadFromHd<T>(string filename)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(_dirName + "/" + filename, FileMode.Open);
            T loadedData = (T) bf.Deserialize(file);
            file.Close();
            return loadedData;
        }

        private static T Load<T>(string fileName)
        {
            if (!File.Exists(Application.persistentDataPath + "/" + fileName)) UnpackMobileFile(fileName);
            //check again in case it didn't work
            if (File.Exists(Application.persistentDataPath + "/" + fileName))
            {
                BinaryFormatter bf = new BinaryFormatter(); //engine that will interface with file
                FileStream file = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
                T loadedData = (T) bf.Deserialize(file);
                file.Close();
                return loadedData;
            }

            return default(T);
        }

        private static string LoadString(string fileName)
        {
            if (!File.Exists(Application.persistentDataPath + "/" + fileName)) UnpackMobileFile(fileName);
            //check again in case it didn't work
            if (File.Exists(Application.persistentDataPath + "/" + fileName))
            {
                Debug.Log("Loading string file from " + Application.persistentDataPath + "/" + fileName);
                return File.ReadAllText(Application.persistentDataPath + "/" + fileName);
            }

            return null;
        }

        private static void UnpackMobileFile(string fileName)
        {
            //copies and unpacks file from apk to persistentDataPath where it can be accessed
            string destinationPath = Path.Combine(Application.persistentDataPath, fileName);
            string sourcePath = Path.Combine(Application.streamingAssetsPath, fileName);

            //if DB does not exist in persistent data folder (folder "Documents" on iOS) or source DB is newer then copy it
            if (!File.Exists(destinationPath) ||
                (File.GetLastWriteTimeUtc(sourcePath) > File.GetLastWriteTimeUtc(destinationPath)))
            {
                if (sourcePath.Contains("://"))
                {
// Android  
                    WWW www = new WWW(sourcePath);
                    while (!www.isDone)
                    {
                        ;
                    } // Wait for download to complete - not pretty at all but easy hack for now 
                    if (string.IsNullOrEmpty(www.error))
                    {
                        File.WriteAllBytes(destinationPath, www.bytes);
                    }
                    else
                    {
                        Debug.Log("ERROR: the file DB named " + fileName +
                                  " doesn't exist in the StreamingAssets Folder, please copy it there.");
                    }
                }
                else
                {
                    // Mac, Windows, Iphone                
                    //validate the existence of the DB in the original folder (folder "StreamingAssets")
                    if (File.Exists(sourcePath))
                    {
                        //copy file - alle systems except Android
                        File.Copy(sourcePath, destinationPath, true);
                    }
                    else
                    {
                        Debug.Log("ERROR: the file DB named " + fileName +
                                  " doesn't exist in the StreamingAssets Folder, please copy it there.");
                    }
                }
            }
        }

        public class WrappedString
        {
            public string Data;
        }
    }
}