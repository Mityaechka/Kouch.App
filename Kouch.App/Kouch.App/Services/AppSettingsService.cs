using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Kouch.App.Models
{
    public class AppSettingsService
    {
        private static AppSettingsService _instance;
        private readonly JObject _secrets;

        private const string Namespace = "Kouch.App";
        private const string FileName = "appsettings.json";

        private AppSettingsService()
        {
            try
            {
                Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppSettingsService)).Assembly;
                Stream stream = assembly.GetManifestResourceStream($"{Namespace}.{FileName}");
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    _secrets = JObject.Parse(json);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Unable to load secrets file");
            }
        }

        public static AppSettingsService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppSettingsService();
                }

                return _instance;
            }
        }

        public string this[string name]
        {
            get
            {
                try
                {
                    string[] path = name.Split(':');

                    JToken node = _secrets[path[0]];
                    for (int index = 1; index < path.Length; index++)
                    {
                        node = node[path[index]];
                    }

                    return node.ToString();
                }
                catch (Exception)
                {
                    Debug.WriteLine($"Unable to retrieve secret '{name}'");
                    return string.Empty;
                }
            }
        }
        public T GetValue<T>(string name)
        {
            try
            {
                string[] path = name.Split(':');

                JToken node = _secrets[path[0]];
                for (int index = 1; index < path.Length; index++)
                {
                    node = node[path[index]];
                }

                return (T)Convert.ChangeType(node.ToString(), typeof(T));
            }
            catch (Exception)
            {
                Debug.WriteLine($"Unable to retrieve secret '{name}'");
                return default;
            }
        }
    }
}
