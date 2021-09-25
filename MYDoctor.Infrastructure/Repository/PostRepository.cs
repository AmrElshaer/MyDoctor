using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;
using MYDoctor.Infrastructure.Identity;
using System.Linq;
using System.Threading.Tasks;
using MYDoctor.Core.Application.IHelper;
using MYDoctor.Core.Application;
using MYDoctor.Core.Application.Common;

namespace MYDoctor.Infrastructure.Repository
{
    internal class PostRepository:BaseRepository<Post>,IPostRepository
    {
       
        private readonly IServiceBuilder serviceBuilder;

        public PostRepository(ApplicationDbContext context,IServiceBuilder serviceBuilder) : base(context)
        {
            
            this.serviceBuilder = serviceBuilder;
           
        }
        public async Task<BaseViewModel<Post>> GetPostAsync(int id, int numberRelated)
        {
            var post = await GetFirstAsync(p => p.Id == id, p => p.Category,
                p=>p.Category.Doctors,p=>p.Category.Medicins,p=>p.Category.Diseases,p => p.User);
            return serviceBuilder.BuildViewModel(new PostViewModel(post, numberRelated, post.CategoryId));
             
        }

    }
}
