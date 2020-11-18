using MyDoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDoctor.ViewModels
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
