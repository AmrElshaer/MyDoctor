using System.Collections.Generic;
using System.Linq;
namespace MYDoctor.Core.Application.Common
{
    public static class Extention 
    {
        public static IEnumerable<T> AppendData<T>(this IEnumerable<T> data, IEnumerable<T> appendData){
           
            appendData.ToList().ForEach(a=>data = data.Append(a));
            return data;

        }
       
       
    }
}
