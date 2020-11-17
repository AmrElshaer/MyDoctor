using MyDoctor.Infrastructure;
using MyDoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.IRepository
{
    public interface ICountryRepository:IRepository<Country>
    {
    }
}
