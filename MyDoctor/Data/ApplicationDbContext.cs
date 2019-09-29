using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Models;

namespace MyDoctor.Data
{
    public class ApplicationDbContext : IdentityDbContext<CutomPropertiy>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
                
     
                  
            base.OnModelCreating(builder);
            builder.Entity<LikeandDislikeclass>().HasData(
                new LikeandDislikeclass
                {
                    Id = 1,
                    DiseaseName = "Highpress",
                    Like = 0,
                    DisLike = 0,
                }
            ) ;
            builder.Entity<BeatyandHealthy>().HasData(
                new BeatyandHealthy
                {
                    Id = 1,
                    Catagory = "الرياضة والرشاقة",
                   Image= "https://static.webteb.net/images/content/slideshow_slideshow_1756_3807b64a6cc-17e2-4ea3-9785-abd3cdb05540.jpg"
                   
                }, new BeatyandHealthy
                {
                    Id = 2,
                    Catagory = "الريجيم وتخفيف الوزن",
                    Image = "https://static.webteb.net/images/content/tbl_articles_article_21104_50697899ac1-0b6d-454d-b882-aa5c535a839d.jpg"
                   
                   
                }
                ,new BeatyandHealthy
                 {
                Id = 3,
                    Catagory = "التغذية السليمة",
                    Image = "https://static.webteb.net/images/content/tbl_articles_article_21149_393ab96dbff-e233-4161-8335-2590a210376c.jpg"
                                  }, new BeatyandHealthy
                {
                    Id = 4,
                    Catagory = "الحياة الزوجية",
                    Image = "https://static.webteb.net/images/content/slideshow_slideshow_1855_938129447e6-33b4-4d1f-87e9-cb69c44f463f.jpg"
                 
                }, new BeatyandHealthy
                {
                    Id = 5,
                    Catagory = "العناية بالشعر",
                    Image = "https://static.webteb.net/images/content/video_video_574_19396f7560c-3586-42b6-8a33-c4242b762843.jpg"
                   
                }
                , new BeatyandHealthy
                {
                    Id = 6,
                    Catagory = "العناية بالبشرة والجمال",
                    Image = "https://static.webteb.net/images/content/tbl_articles_article_21115_350a54688d-6b44-49de-9132-c83db36c1e21.jpg"
                
                }
                , new BeatyandHealthy
                  {
                      Id = 7,
                      Catagory = "جودة الحياة",
                      Image = "https://static.webteb.net/images/content/tbl_articles_article_21091_224c1f7c9d9-1017-49f6-91ab-8338bf8f8bf5.jpg"
                  
                }, new BeatyandHealthy
                {
                    Id = 8,
                    Catagory = "الجيل الذهبي",
                    Image = "https://static.webteb.net/images/content/tbl_articles_article_17490_98.jpg"
                   
                }
                , new BeatyandHealthy
                {
                    Id = 9,
                    Catagory = "وصفات صحية",
                    Image = "https://static.webteb.net/images/content/ramadanrecipe_recipe_544_785238ad407-4bce-40df-a2e5-402e3fae7092.jpg"
                  
                }
                , new BeatyandHealthy
                {
                    Id = 10,
                    Catagory = "القيم الغذائية للمأكولات",
                    Image = "https://static.webteb.net/images/content/tbl_articles_article_21183_468062634f5-e43b-4772-bd68-daaf7436c10f.jpg"
                  
                }
            );
        }




        public DbSet<MyDoctor.Models.Disease> Disease { get; set; }
        public DbSet<MyDoctor.Models.Comments> Comments { get; set; }
        public DbSet<MyDoctor.Models.LikeandDislikeclass> LikeandDislikeclass { get; set; }
        public DbSet<MyDoctor.Models.RelativeDisease> RelativeDisease { get; set; }
        public DbSet<MyDoctor.Models.BeatyandHealthy> BeatyandHealthy { get; set; }
        public DbSet<MyDoctor.Models.RelativeofBeatyandhealthy> RelativeofBeatyandhealthy { get; set; }
        public DbSet<MyDoctor.Models.updateRelativeBeatyandHealthy> updateRelativeBeatyandHealthy { get; set; }
        public DbSet<MyDoctor.Models.Doctor> Doctor { get; set; }
        public DbSet<MyDoctor.Models.Posts> Posts { get; set; }
        public DbSet<MyDoctor.Models.Commentfordoctorpost> Commentfordoctorpost { get; set; }
        public DbSet<MyDoctor.Models.Medicin> Medicin { get; set; }
    }
}
