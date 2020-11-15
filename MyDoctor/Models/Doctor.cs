
using MyDoctor.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyDoctor.Models
{
    public class Doctor:BaseEntity
    {
        public Doctor()
        {
            Posts=new HashSet<Posts>();
        }
       
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public BeatyandHealthy Category { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Others { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public Kind Kind { get; set; }
        public string ImagePath { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and confirmpassword didnot match")]
        public string ConfirmPassword { get; set; }
        public ICollection<Posts> Posts { get; set; }


    }

    public enum Kind
    {
        Female,
        Male
    }
}
