﻿using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MYDoctor.Core.Application.Common;
using MYDoctor.Infrastructure.Identity;
using DocumentFormat.OpenXml.Wordprocessing;

namespace MYDoctor.Infrastructure.Helper
{
    public class RelativeCategoryHelper : IRelativeCategoryHelper
    {
        private readonly ApplicationDbContext _context;

        public RelativeCategoryHelper(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RelativeofBeatyandhealthy>> GetRelativesCategory(ICollection<RelativeofBeatyandhealthy> relativeofBeatyandhealthies, int numberRelated, int? categoryId)
        {
            if ((relativeofBeatyandhealthies.Any(), relativeofBeatyandhealthies.Count() >= numberRelated) == (true, true))
                return relativeofBeatyandhealthies;
            var rels = await _context.RelativeofBeatyandhealthy.Include(d => d.BeatyandHealthy).Where(a=>a.BeatyandHealthId!=categoryId).OrderByDescending(c => c.Id).Take(numberRelated - relativeofBeatyandhealthies.Count()).ToListAsync(); ;
            return relativeofBeatyandhealthies.AppendData(rels);
        }
        public async Task<IEnumerable<RelativeofBeatyandhealthy>> GetRelativesCategory(int numberRelated)
        {
          return  await _context.RelativeofBeatyandhealthy.Include(rc => rc.BeatyandHealthy)
                  .Take(numberRelated).ToListAsync();
        }
    }
}
