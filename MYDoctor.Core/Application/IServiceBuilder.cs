using MYDoctor.Core.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYDoctor.Core.Application
{
    /// <summary>
    /// Factory Method to build view model
    /// </summary>
    public interface IServiceBuilder
    {
        BaseViewModel<T> BuildViewModel<T>(ViewModel<T> viewModel) where T : class;
    }
}
