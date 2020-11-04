﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDoctor.Areas.Admin.Models;
using MyDoctor.Infrastructure;
using MyDoctor.Models;

namespace MyDoctor.IRepository
{
    public interface IDiseasesRepository:IRepository<Disease>
    {
        Task CreateEdit(Disease disease);
        SearchResult<Disease> GetSearchResult(string query, int pageNumber, int pageSize, DateTime? createFrom, DateTime? createTo);
    }
}
