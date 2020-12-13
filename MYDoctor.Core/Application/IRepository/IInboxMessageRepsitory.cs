using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MYDoctor.Core.Application.IRepository
{
    public interface IInboxMessageRepsitory : IRepository<InboxMessage>
    {
        Task MakeMessagesSeeAsync(string name);
        Task InsertAsync(InboxMessageViewModel inboxMessageView);
        Task<IEnumerable<InboxMessageViewModel>> GetMissMessages(string toName);
        Task<IEnumerable<InboxMessageViewModel>> MessagesDetails(string fromName, string name);
        Task<IEnumerable<InboxMessageViewModel>> GetALLMessagesAsync(string name);
    }
}
