﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MYDoctor.Infrastructure.Repository
{
    public class InboxMessageRepsitory : BaseRepository<InboxMessage>, IInboxMessageRepsitory
    {
        public InboxMessageRepsitory(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<InboxMessageViewModel>> MessagesDetails(string fromName, string name)
        {
            var messages = GetAll(ApplySearch(fromName, name)).Include(m => m.ToUserProfile).Include(m => m.UserProfile).OrderBy(a => a.CreateDate);
            return await ConvertToInboxMessageVM(messages);

        }

        private static Expression<Func<InboxMessage, bool>> ApplySearch(string fromName, string name)
        {
            return
                            m => (m.UserProfile.Email == fromName && m.ToUserProfile.Email == name) ||
                            (m.UserProfile.Email == name && m.ToUserProfile.Email == fromName);
        }

        public async Task<IEnumerable<InboxMessageViewModel>> GetMissMessages(string toName)
        {
            var misMessage =GetAll(m => !m.IsSee && m.ToUserProfile.Email == toName)
                .Include(m => m.ToUserProfile).Include( m => m.UserProfile).OrderByDescending(o => o.Id);
            return await ConvertToInboxMessageVM(misMessage);
        }



        public async Task InsertAsync(InboxMessageViewModel inboxMessageView)
        {
            try
            {
                var toUserProfile = await _context.UserProfiles.FirstOrDefaultAsync(a => a.Email == inboxMessageView.ToName);
                var fromUserProfile = await _context.UserProfiles.FirstOrDefaultAsync(a => a.Email == inboxMessageView.FromName);
                await InsertAsync(new InboxMessage()
                {
                    ToUserProfileId = toUserProfile.Id,
                    UserProfileId = fromUserProfile.Id,
                    Content = inboxMessageView.Content,
                    IsSee = false
                });

            }
            catch (Exception)
            {

                throw;
            }


        }
        public async Task<IEnumerable<InboxMessageViewModel>> GetALLMessagesAsync(string name) {
                var messages =await GetAll(m=> m.ToUserProfile.Email == name).Include(m => m.ToUserProfile)
                .Include(m => m.UserProfile).GroupBy(m=>m.UserProfileId).Select(m=>new InboxMessageViewModel() { 
                    FromName=m.FirstOrDefault().UserProfile.Email,
                    FromImage = $"/images/{m.FirstOrDefault().UserProfile.ImagePath ?? "Defulat.jpg"}",
                }).ToListAsync();
            return messages;
        }
        public async Task MakeMessagesSeeAsync(string name)
        {
            await GetAll(m => !m.IsSee && m.ToUserProfile.Email == name).Include(m => m.ToUserProfile).ForEachAsync(m => m.IsSee = true);
            await _context.SaveChangesAsync();
        }
         private async Task<IEnumerable<InboxMessageViewModel>> ConvertToInboxMessageVM(IQueryable<InboxMessage> messages) { 
           var resuilt=await messages.Select(
                 m => new InboxMessageViewModel()
                 {
                     FromName = m.UserProfile.Email,
                     FromImage = $"/images/{m.UserProfile.ImagePath ?? "Defulat.jpg"}",
                     Content = m.Content,
                     CreateDate = m.CreateDate.ToShortDateString()
                 }
                ).ToListAsync();
            return resuilt;
        }

    }
}
