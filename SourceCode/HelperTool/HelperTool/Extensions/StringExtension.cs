using Newtonsoft.Json;

namespace HelperTool.Extensions
{
    public static class StringExtension
    {
        public static T ToModel<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }


        public static string ToJson<T>(this T model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
