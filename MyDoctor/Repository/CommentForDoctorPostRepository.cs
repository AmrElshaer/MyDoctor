using MyDoctor.Infrastructure;
using MyDoctor.IRepository;
using MyDoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyDoctor.Data;

namespace MyDoctor.Repository
{
    public class CommentForDoctorPostRepository:BaseRepository<Commentfordoctorpost>,ICommentForDoctorPostRepository
    {
        public CommentForDoctorPostRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
