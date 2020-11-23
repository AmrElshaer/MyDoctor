using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyDoctor.Areas.Admin.Models
{
    public static class ConvertToJson
    {

        public static string ToJson<T>(this T item)
        {
            return JsonConvert.SerializeObject(item);
        }
    }
}
