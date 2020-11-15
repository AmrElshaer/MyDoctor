using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDoctor.ViewModels;

namespace MyDoctor.ISerivce
{
    public interface ICategorySerivce
    {
        Task<IBaseViewModel> GetCategory(int categoryId, int numberRelated);
    }
}
