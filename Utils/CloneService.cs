using Newtonsoft.Json;

namespace SteamAPI.Utils
{
    [Serializable]
    public static class CloneService
    {
        public static T Clone<T>(this T source)
        {
            if(Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            var deserializeSetting = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };
            var serializeSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source, serializeSettings), deserializeSetting);
        }
    }
}
