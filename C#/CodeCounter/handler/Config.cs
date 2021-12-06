using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using CodeCounter.languages;

namespace CodeCounter.handler
{
    public class Config
    {
        private static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "CodeCounter");

        private const string FileName = "languages.config";

        public static void Serialize(List<Language> languages)
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            var x = new XmlSerializer(languages.GetType());
            var writer = new StreamWriter(Path.Combine(FilePath, FileName));
            x.Serialize(writer, languages);
            writer.Close();
        }

        public static List<Language> Deserialize()
        {
            if (!File.Exists(Path.Combine(FilePath, FileName)))
            {
                Serialize(Languages.DefaultLanguages());
            }

            var x = new XmlSerializer(typeof(List<Language>));
            var stream = new FileStream(Path.Combine(FilePath, FileName), FileMode.OpenOrCreate);
            var reader = new StreamReader(stream);
            var languages = (List<Language>)x.Deserialize(reader);
            stream.Close();
            reader.Close();
            return languages;
        }
    }
}
