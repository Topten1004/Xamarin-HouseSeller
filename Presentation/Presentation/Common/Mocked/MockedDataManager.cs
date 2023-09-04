using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace Immowert4You.Presentation.Common.Mocked
{
    public class MockedDataManager
    {
        public static T GetData<T>(string source)
        {
            var assembly = typeof(MockedDataManager).GetTypeInfo().Assembly;
            string jsonFileName = $"Immowert4You.Presentation.Common.Mocked.Data.{source}.json";
            var test = assembly.GetManifestResourceNames();
            Stream stream = assembly.GetManifestResourceStream(jsonFileName);
            using (var reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }
        }
    }
}