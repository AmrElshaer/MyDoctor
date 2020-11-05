using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDoctor.Models;

namespace MyDoctor.Areas.Admin.Models
{
    public class MedicineComparer:IEqualityComparer<DiseaseMedicin>
    {
        public bool Equals(DiseaseMedicin x, DiseaseMedicin y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }
            //If one of the object refernce is null then return false
            if (x is null || y is null)
            {
                return false;
            }
            return x.DiseaseId == y.DiseaseId;

        }

        public int GetHashCode(DiseaseMedicin obj)
        {
            throw new NotImplementedException();
        }
    }
}
