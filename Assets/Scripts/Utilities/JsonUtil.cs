using System.IO;
using UnityEngine;

namespace Utilities
{
    public static class JsonUtil
    {
        /// <summary>
        /// Generic method which checks the if the file exists,
        /// if it doesn't then we create a new one
        /// </summary>
        public static T GetOrCreateJsonFile<T>(string filePath) where T : new()
        {
            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
            }

            if (!File.Exists(filePath))
            {
                var binding = new T();

                var newJson = JsonUtility.ToJson(binding);

                File.WriteAllText(filePath, newJson);

                return binding;
            }

            var existingJson = File.ReadAllText(filePath);

            return JsonUtility.FromJson<T>(existingJson);
        }

        /// <summary>
        /// Converts the object to Json, then writes it to file.
        /// Which can be deserialized later
        /// </summary>
        public static void SaveJson(object obj, string filePath)
        {
            var json = JsonUtility.ToJson(obj);

            File.WriteAllText(filePath, json);
        }
    }
}
