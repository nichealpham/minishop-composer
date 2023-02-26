using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AppGlobal.Extensions
{
    public static class ObjectExtension
    {
        public static Dictionary<string, string> ConvertToDictionary<TEntity>(this TEntity obj) where TEntity : class
        {
            return obj.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(obj, null) != null ? prop.GetValue(obj, null).ToString() : "");
        }

        public static string Serialize<TEntity>(this TEntity obj) where TEntity : class
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            });
        }

        public static TEntity Deserialize<TEntity>(this string str) where TEntity : class
        {
            return JsonConvert.DeserializeObject<TEntity>(str, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            });
        }
    }
}
