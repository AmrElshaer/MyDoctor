using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDoctor.ViewModels;

namespace MyDoctor.ISerivce
{
    public interface IDashBoardSerivce
    {
       Task<IBaseViewModel> GetBoardViewModel(int pageSize);
    }
}
