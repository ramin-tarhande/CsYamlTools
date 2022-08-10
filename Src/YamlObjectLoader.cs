using System.IO;
using YamlDotNet.Serialization;

namespace CsYamlTools
{
    public static class YamlObjectLoader
    {
        public static T Load<T>(string filePath)
        {
            new YamlTabChecker(filePath).Check();

            using (var streamReader = new StreamReader(filePath))
            {
                var deserializer = new Deserializer();
                return deserializer.Deserialize<T>(streamReader);
            }
        }
    }
}
