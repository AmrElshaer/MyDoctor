using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Areas.Admin.Models
{
    public static class StringMax
    {
        public static string MaxString(this string value,int maxNumber,string displayoption)
        {
            if (!string.IsNullOrEmpty(value))
                  return value.Length > maxNumber ? $"{value.Substring(0, maxNumber)}{displayoption}":value;
            
            return value;
        }
    }
}
