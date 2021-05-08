using Kouch.App.Models;
using Newtonsoft.Json.Linq;
using Plugin.Settings;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Kouch.App.Services
{
    public class LocalizationService
    {
        private JObject _secrets;

        private string currentLocalization;
        private static LocalizationService _instance;

        private const string Namespace = "Kouch.App";
        private const string FileName = "appsettings.json";


        public static LocalizationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LocalizationService();
                }

                return _instance;
            }
        }
        private LocalizationService()
        {
            
            currentLocalization = CrossSettings.Current.GetValueOrDefault("Locale", "ru");
            LoadTranslation();
        }
        private void LoadTranslation()
        {
            try
            {
                var t = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppSettingsService)).Assembly;
                var path = $"{Namespace}.Localization.localization-{currentLocalization}.json";
                var stream = assembly.GetManifestResourceStream(path);

                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    _secrets = JObject.Parse(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to load secrets file");
            }
        }

        public string this[string name]
        {
            get
            {
                try
                {
                    if (name == null)
                    {
                        return "Translate Error";
                    }
                    var path = name.Split('_');

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
                    return "Translate Error";
                }
            }
        }

    }
}
