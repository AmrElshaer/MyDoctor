using MYDoctor.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MYDoctor.Core.Application.IRepository
{
    public interface IUserProfileRepository:IRepository<UserProfile>
    {
        Task InsertAsync(string email, string imagePath);
    }
}
