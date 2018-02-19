using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarmCallApplication.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }        
        public string Designation { get; set; }
        public int DesignationRoleLevel { get; set; }
        public int Price { get; set; }
        public string UserTimezone { get; set; }
        //public List<UserSchedule> AvailableSchedules { get; set; }
        public virtual ICollection<UserSchedule> BuyerAvailableSchedules { get; set; }
        public virtual ICollection<Requirement> MyRequirements { get; set; }
        public virtual ICollection<CallSchedule> SellerCallSchedules { get; set; }
    }

    public class UserSchedule{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ScheduleDate { get; set; }
        public string UntilDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool Repeat { get; set; }
    }

    public class Requirement
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	    public int Id{get; set;}
	    public string Title{get; set;}
        public string Description{get; set;}
        public int Cost{get; set;}
        public int CallPrice{get; set;}
        public bool Status { get; set; }
        public virtual ICollection<CallSchedule> SellerCallSchedules { get; set; }
    }

    public class CallSchedule
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ScheduleDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public virtual ApplicationUser Seller { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}