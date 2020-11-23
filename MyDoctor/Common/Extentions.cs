using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Reflection;

namespace MyDoctor.Common
{
    public static class Extentions
    {
        public static string ToJson<T>(this T item)
        {
            return JsonConvert.SerializeObject(item);
        }
        public static string ToJson<T>(this T item,params string[] ignoreProperties)
        {
            return JsonConvert.SerializeObject(item, new JsonSerializerSettings()
            { ContractResolver = new IgnorePropertiesResolver(ignoreProperties) });
        }
        public static string MaxString(this string value, int maxNumber, string displayoption)
        {
            if (!string.IsNullOrEmpty(value))
                return value.Length > maxNumber ? $"{value.Substring(0, maxNumber)}{displayoption}" : value;

            return value;
        }
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
    public class IgnorePropertiesResolver : DefaultContractResolver
    {
        private readonly HashSet<string> ignoreProps;
        public IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore)
        {
            this.ignoreProps = new HashSet<string>(propNamesToIgnore);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            if (this.ignoreProps.Contains(property.PropertyName))
            {
                property.ShouldSerialize = _ => false;
            }
            return property;
        }
    }
}
