using DocumentFormat.OpenXml.Math;
using MyDoctor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.Helper
{
    public static class Extention 
    {
        public static IEnumerable<T> AppendData<T>(this IEnumerable<T> data, IEnumerable<T> appendData){
           
            appendData.ToList().ForEach(a=>data = data.Append(a));
            return data;

        }
    }
}
