using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Data;
using MyDoctor.Helper;
using MyDoctor.Infrastructure;
using MyDoctor.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Repository
{
    public class CommentRepository:BaseRepository<Comments>,ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task InsertCommentAsync(DiseaseHelper diseaseHelper,string userName)
        {
            CutomPropertiy user = await this._context.Users.FirstOrDefaultAsync(a => a.UserName == userName);
            Comments obj = new Comments()
            {
                Commment = diseaseHelper.Comment,
                UserName =userName,
                DiseaseName = diseaseHelper.DiseaseName,
                ImagePath = user.ImagePath?? "Defulat.jpg"

            };
            await InsertAsync(obj);
            
           
        }
    }
}
