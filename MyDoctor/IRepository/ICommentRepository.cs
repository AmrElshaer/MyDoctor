using MyDoctor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDoctor.Helper;
using MyDoctor.Models;

namespace MyDoctor.IRepository
{
     public interface ICommentRepository:IRepository<Comments>
     {
        Task InsertCommentAsync(DiseaseHelper diseaseHelper, string userName);
     }
}
