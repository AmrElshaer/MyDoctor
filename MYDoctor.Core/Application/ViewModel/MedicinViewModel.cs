using MYDoctor.Core.Application.Common;
using MYDoctor.Core.Application.Common.Search;
using MYDoctor.Core.Domain.Entities;
using System.Collections.Generic;
namespace MYDoctor.Core.Application.ViewModel
{
    public class MedicinViewModel:BaseViewModel
    {
        public MedicinViewModel()
        {

        }
        public MedicinViewModel(IEnumerable<Medicin> medicins, MedicinSearch medicinSearch)
        {
            this.Medicins = medicins;
            this.MedicinSearch = medicinSearch;
        }
        public Medicin Medicin { get; set; }
        public MedicinSearch MedicinSearch { get;set; }
    }
}
