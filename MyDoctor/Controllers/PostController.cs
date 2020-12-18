using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MYDoctor.Core.Application.IRepository;
using MYDoctor.Core.Application.ViewModel;
using MYDoctor.Core.Domain.Entities;

namespace MyDoctor.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IDisLikeRepository _disLikeRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public PostController(IPostRepository postRepository,IUserProfileRepository userProfileRepository,ILikeRepository likeRepository,IDisLikeRepository disLikeRepository)
        {
            _postRepository = postRepository;
            _likeRepository = likeRepository;
            _disLikeRepository = disLikeRepository;
            _userProfileRepository = userProfileRepository;

        }
        public async Task<IActionResult> Details(int? id) {
            if (id==null)
            {
                return NotFound();
            }
            var post =await _postRepository.GetPostAsync(id.Value,4);
            return View(post);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            if (post!=null)
            {
                await _postRepository.InsertAsync(post);
               return RedirectToAction(nameof(Details),new {id=post.Id});
            }
            throw new ArgumentNullException(); 
        }
        public async Task<LikeDisLikeNumViewModel> AddLike(int postId) {
            var userProfile =await _userProfileRepository.GetFirstAsync(u => u.Email == User.Identity.Name);
            return await _likeRepository.AddLike(postId,userProfile.Id);
        }
        public async Task<LikeDisLikeNumViewModel> AddDisLike(int postId)
        {
            var userProfile =await _userProfileRepository.GetFirstAsync(u => u.Email == User.Identity.Name);
            return await _disLikeRepository.AddDisLike(postId, userProfile.Id);
        }
    }
}